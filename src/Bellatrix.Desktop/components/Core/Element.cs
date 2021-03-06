﻿// <copyright file="Element.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Drawing;
using System.Text;
using Bellatrix.Desktop.Controls.Core;
using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.Services;
using Bellatrix.Desktop.Untils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop
{
    [DebuggerDisplay("BELLATRIX Element")]
    public partial class Element
    {
        private readonly ElementWaitService _elementWait;
        private readonly List<WaitStrategy> _untils;
        private WindowsElement _wrappedElement;

        public Element()
        {
            _elementWait = new ElementWaitService();
            WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
            _untils = new List<WaitStrategy>();
        }

        public static event EventHandler<ElementActionEventArgs> ScrollingToVisible;
        public static event EventHandler<ElementActionEventArgs> ScrolledToVisible;
        public static event EventHandler<ElementActionEventArgs> CreatingElement;
        public static event EventHandler<ElementActionEventArgs> CreatedElement;
        public static event EventHandler<ElementActionEventArgs> CreatingElements;
        public static event EventHandler<ElementActionEventArgs> CreatedElements;
        public static event EventHandler<NativeElementActionEventArgs> ReturningWrappedElement;

        public WindowsDriver<WindowsElement> WrappedDriver { get; }

        public WindowsElement WrappedElement
        {
            get
            {
                ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(GetAndWaitWebDriverElement()));
                var element = GetWebDriverElement();
                return element;
            }
            internal set => _wrappedElement = value;
        }

        public WindowsElement ParentWrappedElement { get; set; }

        public WindowsElement FoundWrappedElement { get; set; }

        public int ElementIndex { get; set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public dynamic By { get; internal set; }

        public string GetAttribute(string name)
        {
            return WrappedElement.GetAttribute(name);
        }

        public TElement Create<TElement, TBy>(TBy by)
             where TBy : FindStrategy
             where TElement : Element
        {
            CreatingElement?.Invoke(this, new ElementActionEventArgs(this));

            var elementRepository = new ElementRepository();
            var element = elementRepository.CreateElementWithParent<TElement>(by, WrappedElement, null, 0);

            CreatedElement?.Invoke(this, new ElementActionEventArgs(this));

            return element;
        }

        public ElementsList<TElement> CreateAll<TElement, TBy>(TBy by)
            where TBy : FindStrategy
            where TElement : Element
        {
            CreatingElements?.Invoke(this, new ElementActionEventArgs(this));

            var elementsCollection = new ElementsList<TElement>(by, WrappedElement);

            CreatedElements?.Invoke(this, new ElementActionEventArgs(this));

            return elementsCollection;
        }

        public void WaitToBe()
        {
            GetAndWaitWebDriverElement();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsPresent
        {
            get
            {
                try
                {
                    if (WrappedElement != null)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return false;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsVisible
        {
            get
            {
                try
                {
                    return WrappedElement.Displayed;
                }
                catch (WebDriverException)
                {
                    return false;
                }

                return false;
            }
        }

        public void ScrollToVisible()
        {
            ScrollingToVisible?.Invoke(this, new ElementActionEventArgs(this));

            var touchActions = new RemoteTouchScreen(WrappedDriver);
            System.Threading.Thread.Sleep(2000);
            touchActions.Scroll(WrappedElement.Coordinates, 0, 0);
            this.ToBeVisible().ToExists().WaitToBe();
            ScrolledToVisible?.Invoke(this, new ElementActionEventArgs(this));
        }

        public string ElementName { get; internal set; }

        public string PageName { get; internal set; }

        public Point Location => WrappedElement.Location;

        public Size Size => WrappedElement.Size;

        public void EnsureState(WaitStrategy until)
        {
            _untils.Add(until);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{ElementName}");
            sb.AppendLine($"X = {Location.X}");
            sb.AppendLine($"Y = {Location.Y}");
            sb.AppendLine($"Height = {Size.Height}");
            sb.AppendLine($"Width = {Size.Width}");
            return sb.ToString();
        }

        protected WindowsElement GetAndWaitWebDriverElement()
        {
            if (_wrappedElement == null)
            {
                if (_untils.Count == 0 || _untils[0] == null)
                {
                    EnsureState(Wait.To.Exists());
                }

                try
                {
                    foreach (var until in _untils)
                    {
                        if (until != null)
                        {
                            _elementWait.Wait(this, until);
                        }

                        if (until.GetType().Equals(typeof(WaitNotExistStrategy)))
                        {
                            return _wrappedElement;
                        }
                    }

                    _wrappedElement = GetWebDriverElement();
                }
                catch (WebDriverTimeoutException ex)
                {
                    throw new TimeoutException($"The element with Name = {ElementName} Locator {By.Value} was not found on the page or didn't fulfill the specified conditions.", ex);
                }
            }

            _untils.Clear();

            return _wrappedElement;
        }

        private WindowsElement GetWebDriverElement()
        {
            WindowsElement result = _wrappedElement;
            if (FoundWrappedElement != null)
            {
                result = FoundWrappedElement;
            }

            if (_wrappedElement != null)
            {
                result = _wrappedElement;
            }

            if (ParentWrappedElement == null && _wrappedElement == null)
            {
                result = By.FindElement(WrappedDriver);
            }

            if (ParentWrappedElement != null)
            {
                result = By.FindElement(ParentWrappedElement);
            }

            return result;
        }
    }
}
