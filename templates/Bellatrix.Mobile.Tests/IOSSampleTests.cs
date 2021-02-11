﻿using Bellatrix.Mobile.IOS;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Mobile.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    [IOS("pathToApk",
        "7.1",
        "yourTestDeviceName",
        Lifecycle.ReuseIfStarted)]
    public class IOSSampleTests : NUnit.IOSTest
    {
        ////[TestMethod]
        [Test]
        public void CorrectTextDisplayed_When_ClickSubscribeButton()
        {
            var button = App.ElementCreateService.CreateById<Button>("button");

            button.Click();
        }
    }
}