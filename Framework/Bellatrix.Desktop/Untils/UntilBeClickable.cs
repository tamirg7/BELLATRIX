﻿// <copyright file="UntilBeClickable.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Untils
{
    public class UntilBeClickable : BaseUntil
    {
        public UntilBeClickable(int? timeoutInterval = null, int? sleepInterval = null)
            : base(timeoutInterval, sleepInterval) => TimeoutInterval = timeoutInterval ?? ConfigurationService.Instance.GetDesktopSettings().ElementToBeClickableTimeout;

        public override void WaitUntil<TBy>(TBy by) => WaitUntil(ElementIsClickable(WrappedWebDriver, by), TimeoutInterval, SleepInterval);

        private Func<IWebDriver, bool> ElementIsClickable<TBy>(WindowsDriver<WindowsElement> searchContext, TBy by)
            where TBy : Locators.By => driver =>
        {
            var element = by.FindElement(searchContext);
            element = element.Displayed ? element : null;
            try
            {
                return element != null && element.Enabled;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        };
    }
}