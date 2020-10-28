﻿// <copyright file="BaseTest.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Bellatrix.NUnit;
using Bellatrix.TestWorkflowPlugins;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace Bellatrix
{
    public class NUnitBaseTest
    {
        public IServicesCollection Container;
        protected static ThreadLocal<Exception> ThrownException;
        private TestWorkflowPluginProvider _currentTestExecutionProvider;
        protected NUnitExecutionContext ЕxecutionContext;

        public NUnitBaseTest()
        {
            ЕxecutionContext = new NUnitExecutionContext();
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                if (eventArgs.Exception.Source != "System.Private.CoreLib")
                {
                    if (ThrownException == null)
                    {
                        ThrownException = new ThreadLocal<Exception>(() => eventArgs.Exception);
                    }
                    else
                    {
                        ThrownException.Value = eventArgs.Exception;
                    }
                }
            };
        }

        [OneTimeSetUp]
        public void OneTimeArrangeAct()
        {
            try
            {
                var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
                ServicesCollection.Current.RegisterInstance(ЕxecutionContext.SharedData.ContainsKey("isParallelRun"), "isParallelRun");
                if (ЕxecutionContext.SharedData.ContainsKey("ExecutionDirectory"))
                {
                    ServicesCollection.Main.RegisterInstance((string)ЕxecutionContext.SharedData["ExecutionDirectory"], "ExecutionDirectory");
                }

                Container = ServicesCollection.Current.CreateChildServicesCollection(testClassType.FullName);
                Container.RegisterInstance(Container);
                Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
                _currentTestExecutionProvider = new TestWorkflowPluginProvider();
                Initialize();
                InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
                _currentTestExecutionProvider.PreTestsArrange(testClassType);
                TestsArrange();
                _currentTestExecutionProvider.PostTestsArrange(testClassType);
                _currentTestExecutionProvider.PreTestsAct(testClassType);
                TestsAct();
                _currentTestExecutionProvider.PostTestsAct(testClassType);
            }
            catch (Exception ex)
            {
                var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
                var categories = GetAllTestCategories();
                var authors = GetAllAuthors();
                var descriptions = GetAllDescriptions();
                _currentTestExecutionProvider.TestsArrangeFailed(ex);

                throw ex;
            }
        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            try
            {
                var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
                ServicesCollection.Current.RegisterInstance(ЕxecutionContext.SharedData.ContainsKey("isParallelRun"), "isParallelRun");
                if (ЕxecutionContext.SharedData.ContainsKey("ExecutionDirectory"))
                {
                    ServicesCollection.Main.RegisterInstance((string)ЕxecutionContext.SharedData["ExecutionDirectory"], "ExecutionDirectory");
                }

                Container = ServicesCollection.Current.CreateChildServicesCollection(testClassType.FullName);
                Container.RegisterInstance(Container);
                Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
                _currentTestExecutionProvider = new TestWorkflowPluginProvider();
                InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);

                _currentTestExecutionProvider.PreClassCleanup(testClassType);
                TestsCleanup();
                _currentTestExecutionProvider.PostClassCleanup(testClassType);
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestsCleanupFailed(ex);
                throw;
            }
        }

        [SetUp]
        public void CoreTestInit()
        {
            if (ThrownException?.Value != null)
            {
                ThrownException.Value = null;
            }

            var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
            ServicesCollection.Current.RegisterInstance(ЕxecutionContext.SharedData.ContainsKey("isParallelRun"), "isParallelRun");
            if (ЕxecutionContext.SharedData.ContainsKey("ExecutionDirectory"))
            {
                ServicesCollection.Main.RegisterInstance((string)ЕxecutionContext.SharedData["ExecutionDirectory"], "ExecutionDirectory");
            }

            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);

            var testMethodMemberInfo = GetCurrentExecutionMethodInfo(ЕxecutionContext);
            var categories = GetAllTestCategories();
            var authors = GetAllAuthors();
            var descriptions = GetAllDescriptions();

            _currentTestExecutionProvider = new TestWorkflowPluginProvider();
            InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
            try
            {
                _currentTestExecutionProvider.PreTestInit(ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                TestInit();
                _currentTestExecutionProvider.PostTestInit(ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestInitFailed(ex, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                throw;
            }
        }

        [TearDown]
        public void CoreTestCleanup()
        {
            var testClassType = GetCurrentExecutionTestClassType(ЕxecutionContext);
            Container = ServicesCollection.Current.FindCollection(testClassType.FullName);
            Container.RegisterInstance<ExecutionContext>(ЕxecutionContext);
            var testMethodMemberInfo = GetCurrentExecutionMethodInfo(ЕxecutionContext);
            var categories = GetAllTestCategories();
            var authors = GetAllAuthors();
            var descriptions = GetAllDescriptions();

            try
            {
                _currentTestExecutionProvider = new TestWorkflowPluginProvider();
                InitializeTestExecutionBehaviorObservers(_currentTestExecutionProvider);
                _currentTestExecutionProvider.PreTestCleanup(ЕxecutionContext.TestOutcome, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions, ЕxecutionContext.ExceptionMessage, ЕxecutionContext.ExceptionStackTrace, ThrownException?.Value);
                TestCleanup();
                _currentTestExecutionProvider.PostTestCleanup(ЕxecutionContext.TestOutcome, ЕxecutionContext.TestFullName, testMethodMemberInfo, testClassType, categories, authors, descriptions, ЕxecutionContext.ExceptionMessage, ЕxecutionContext.ExceptionStackTrace, ThrownException?.Value);
            }
            catch (Exception ex)
            {
                _currentTestExecutionProvider.TestCleanupFailed(ex, ЕxecutionContext.TestName, testMethodMemberInfo, testClassType, categories, authors, descriptions);
                throw;
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual void TestsArrange()
        {
        }

        public virtual void TestsAct()
        {
        }

        public virtual void TestsCleanup()
        {
        }

        public virtual void TestInit()
        {
        }

        public virtual void TestCleanup()
        {
        }

        protected static bool IsDebugRun()
        {
#if DEBUG
            var isDebug = true;
#else
            bool isDebug = false;
#endif

            return isDebug;
        }

        private List<string> GetAllTestCategories()
        {
            var categories = new List<string>();
            foreach (var property in GetTestProperties(PropertyNames.Category))
            {
                categories.Add(property);
            }

            return categories;
        }

        private List<string> GetAllAuthors()
        {
            var authors = new List<string>();
            foreach (var property in GetTestProperties(PropertyNames.Author))
            {
                authors.Add(property);
            }

            return authors;
        }

        private IEnumerable<string> GetTestProperties(string name)
        {
            var list = new List<string>();
            var currentTest = (ITest)TestExecutionContext.CurrentContext?.CurrentTest;
            while (currentTest?.GetType() != typeof(TestSuite) && currentTest.ClassName != "NUnit.Framework.Internal.TestExecutionContext+AdhocContext")
            {
                if (currentTest.Properties.ContainsKey(name))
                {
                    if (currentTest.Properties[name].Count > 0)
                    {
                        for (var i = 0; i < currentTest.Properties[name].Count; i++)
                        {
                            list.Add(currentTest.Properties[name][i].ToString());
                        }
                    }
                }

                currentTest = currentTest.Parent;
            }

            return list;
        }

        private List<string> GetAllDescriptions()
        {
            var descriptions = new List<string>();
            foreach (var property in GetTestProperties(PropertyNames.Description))
            {
                descriptions.Add(property);
            }

            return descriptions;
        }

        private void InitializeTestExecutionBehaviorObservers(TestWorkflowPluginProvider testExecutionProvider)
        {
            var observers = ServicesCollection.Current.ResolveAll<TestWorkflowPlugin>();
            foreach (var observer in observers)
            {
                observer.Subscribe(testExecutionProvider);
            }
        }

        private MethodInfo GetCurrentExecutionMethodInfo(NUnitExecutionContext executionContext)
        {
            var testMethodMemberInfo = GetCurrentExecutionTestClassType(executionContext).GetMethod(executionContext.TestName?.Split('(').First());
            return testMethodMemberInfo;
        }

        private Type GetCurrentExecutionTestClassType(NUnitExecutionContext executionContext)
        {
            var testClassType = GetType().Assembly.GetType(executionContext.TestClassName);

            return testClassType;
        }
    }
}