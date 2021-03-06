﻿// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
   public static class ElementCreateExtensions
    {
        ////public static TElement CreateByTag<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByTagName>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindIdStrategy>(new FindIdStrategy(id));

        public static TElement CreateByIdContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindIdContainingStrategy>(new FindIdContainingStrategy(id));

        public static TElement CreateByDescription<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindDescriptionStrategy>(new FindDescriptionStrategy(description));

        public static TElement CreateByDescriptionContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindDescriptionContainingStrategy>(new FindDescriptionContainingStrategy(description));

        public static TElement CreateByText<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindTextStrategy>(new FindTextStrategy(text));

        public static TElement CreateByTextContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindTextContainingStrategy>(new FindTextContainingStrategy(text));

        ////public static TElement CreateByName<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByName>(new ByName(name));

        public static TElement CreateByClass<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByAndroidUIAutomator<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindAndroidUIAutomatorStrategy>(new FindAndroidUIAutomatorStrategy(automationId));

        public static TElement CreateByXPath<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, FindXPathStrategy>(new FindXPathStrategy(xpath));

        ////public static ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), element.WrappedElement);

        public static ElementsList<TElement, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id), element.WrappedElement);

        public static ElementsList<TElement, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id), element.WrappedElement);

        public static ElementsList<TElement, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description), element.WrappedElement);

        public static ElementsList<TElement, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description), element.WrappedElement);

        public static ElementsList<TElement, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text), element.WrappedElement);

        public static ElementsList<TElement, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text), element.WrappedElement);

        ////public static ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), element.WrappedElement);

        public static ElementsList<TElement, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

        public static ElementsList<TElement, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId), element.WrappedElement);

        public static ElementsList<TElement, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath), element.WrappedElement);
    }
}