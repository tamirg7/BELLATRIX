﻿// <copyright file="AndroidApp.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.EventHandlers.Android;
using Bellatrix.Mobile.Services;
using Bellatrix.Mobile.Services.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile
{
    public class AndroidApp : App<AndroidDriver<AndroidElement>, AndroidElement>
    {
        public AndroidAppService AppService => ServicesCollection.Current.Resolve<AndroidAppService>();

        public AndroidFileSystemService FileSystemService => ServicesCollection.Current.Resolve<AndroidFileSystemService>();

        public AndroidDeviceService DeviceService => ServicesCollection.Current.Resolve<AndroidDeviceService>();

        public AndroidKeyboardService KeyboardService => ServicesCollection.Current.Resolve<AndroidKeyboardService>();

        public TouchActionsService<AndroidDriver<AndroidElement>, AndroidElement> TouchActionsService => ServicesCollection.Current.Resolve<TouchActionsService<AndroidDriver<AndroidElement>, AndroidElement>>();

        public override void Dispose()
        {
            DisposeDriverService.DisposeAllAndroid();
            GC.SuppressFinalize(this);
        }

        public void AddElementEventHandler<TElementsEventHandler>()
           where TElementsEventHandler : ElementEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
            elementEventHandler.SubscribeToAll();
        }

        public void RemoveElementEventHandler<TElementsEventHandler>()
            where TElementsEventHandler : ElementEventHandlers
        {
            var elementEventHandler = (TElementsEventHandler)Activator.CreateInstance(typeof(TElementsEventHandler));
            elementEventHandler.UnsubscribeToAll();
        }
    }
}
