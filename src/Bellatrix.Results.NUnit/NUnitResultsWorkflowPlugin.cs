﻿// <copyright file="NUnitResultsWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Plugins;
using Bellatrix.TestExecutionExtensions.Video.Plugins;
using Bellatrix.TestWorkflowPlugins;
using NUnit.Framework;

namespace Bellatrix.Results.NUnit
{
    public class NUnitResultsWorkflowPlugin : TestWorkflowPlugin, IScreenshotPlugin, IVideoPlugin
    {
        public NUnitResultsWorkflowPlugin()
        {
        }

        public TestContext TestContext { get; set; }

        public void SubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
        {
            provider.ScreenshotGeneratedEvent += ScreenshotGenerated;
        }

        public void UnsubscribeScreenshotPlugin(IScreenshotPluginProvider provider)
        {
            provider.ScreenshotGeneratedEvent -= ScreenshotGenerated;
        }

        public void ScreenshotGenerated(object sender, ScreenshotPluginEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.ScreenshotPath))
            {
                TestContext.AddTestAttachment(args.ScreenshotPath);
            }
        }

        public void SubscribeVideoPlugin(IVideoPluginProvider provider)
        {
            provider.VideoGeneratedEvent += VideoGenerated;
        }

        public void UnsubscribeVideoPlugin(IVideoPluginProvider provider)
        {
            provider.VideoGeneratedEvent -= VideoGenerated;
        }

        public void VideoGenerated(object sender, VideoPluginEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.VideoPath))
            {
                TestContext.AddTestAttachment(args.VideoPath);
            }
        }
    }
}