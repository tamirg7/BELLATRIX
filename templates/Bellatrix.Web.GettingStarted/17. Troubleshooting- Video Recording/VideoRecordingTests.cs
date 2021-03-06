﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    // If you open the testFrameworkSettings file, you find the videoRecordingSettings section that controls this lifecycle.
    //  "videoRecordingSettings": {
    //      "isEnabled": "true",
    //      "waitAfterFinishRecordingMilliseconds": "500",
    //      "filePath": "ApplicationData\\Troubleshooting\\Videos"
    //  }
    //
    // You can turn off the making of videos for all tests and specify where the videos to be saved.
    // waitAfterFinishRecordingMilliseconds adds some time to the end of the test, making the video not going black immediately.
    // In the extensibility chapters read more about how you can create custom video recorder or change the saving strategy.
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public class VideoRecordingTests : MSTest.WebTest
    {
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");
            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");
            promotionsLink.Click();
        }
    }
}