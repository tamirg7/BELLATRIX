﻿using System;
////using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace Bellatrix.Web.Tests
{
    // uncomment to use MSTest
    ////[TestClass]
    [TestFixture]
    [Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
    public class LoginTests : NUnit.WebTest
    {
        public override void TestInit() => App.NavigationService.Navigate("http://demos.bellatrix.solutions/my-account/");

        ////[TestMethod]
        [Test]
        public void SuccessfullyLoginToMyAccount()
        {
            TextField userNameField = App.ElementCreateService.CreateById<TextField>("username");
            Password passwordField = App.ElementCreateService.CreateById<Password>("password");
            Button loginButton = App.ElementCreateService.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.ElementCreateService.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.ValidateInnerTextContains("Hello info1");

            Anchor logoutLink = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.ValidateIsVisible();
            logoutLink.Click();
        }

        ////[TestMethod]
        [Test]
        public void SuccessfullyLoginToMyAccount1()
        {
            TextField userNameField = App.ElementCreateService.CreateById<TextField>("username");
            Password passwordField = App.ElementCreateService.CreateById<Password>("password");
            Button loginButton = App.ElementCreateService.CreateByXpath<Button>("//button[@name='login']");

            userNameField.SetText("info@berlinspaceflowers.com");
            passwordField.SetPassword("@purISQzt%%DYBnLCIhaoG6$");
            loginButton.Click();

            Div myAccountContentDiv = App.ElementCreateService.CreateByClass<Div>("woocommerce-MyAccount-content");
            myAccountContentDiv.ValidateInnerTextContains("Hello info1");

            Anchor logoutLink = App.ElementCreateService.CreateByInnerTextContaining<Anchor>("Log out");

            logoutLink.ValidateIsVisible();
            logoutLink.Click();
        }
    }
}