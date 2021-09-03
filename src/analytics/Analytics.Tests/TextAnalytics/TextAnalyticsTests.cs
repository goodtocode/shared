﻿using GoodToCode.Shared.Analytics.Abstractions;
using GoodToCode.Shared.Analytics.CognitiveServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics
{
    [TestClass]
    public class TextAnalyticsTests
    {
        private IConfiguration configuration;
        private ILogger<TextAnalyzerService> log;
        private CognitiveServiceOptions config;
        private TextAnalyzerService service;

        public string SutText { get; set; }

        public TextAnalyticsTests() { }

        [TestInitialize]
        public void Initialize()
        {
            log = LoggerFactory.CreateLogger<TextAnalyzerService>();
            configuration = new AppConfigurationFactory().Create();
            config = new CognitiveServiceOptions(
                configuration[AppConfigurationKeys.TextAnalyticsKeyCredential],
                configuration[AppConfigurationKeys.TextAnalyticsEndpoint]);
            service = new TextAnalyzerService(config);
        }

        [TestMethod]
        public async Task TextAnalytics_Language()
        {
            SutText = "Ce document est rédigé en Français.";
            var sutResult = await service.DetectLanguageAsync(SutText);
            Assert.IsTrue(sutResult != null);
            Assert.IsTrue(sutResult.Length > 0);
        }

        [TestMethod]
        public async Task TextAnalytics_KeyPhrase()
        {
            SutText = "My cat might need to see a veterinarian.";
            var sutResult = await service.ExtractKeyPhrasesAsync(SutText);
            Assert.IsTrue(sutResult != null);
            Assert.IsTrue(sutResult.Any());
        }

        [TestMethod]
        public async Task TextAnalytics_Entities()
        {
            SutText = "I had a wonderful trip to Seattle last week.";
            var sutResult = await service.ExtractEntitiesAsync(SutText);
            Assert.IsTrue(sutResult != null);
            Assert.IsTrue(sutResult.Any());
            var sutFirst = sutResult.FirstOrDefault();
            Assert.IsTrue(sutFirst.Category.Length > 0);
            Assert.IsTrue(sutFirst.AnalyzedText.Length > 0);
            Assert.IsTrue(sutFirst.Confidence > -1);
        }

        [TestMethod]
        public async Task TextAnalytics_Links()
        {
            SutText = @"Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, \
                to develop and sell BASIC interpreters for the Altair 8800. \
                During his career at Microsoft, Gates held the positions of chairman, \
                chief executive officer, president and chief software architect, \
                while also being the largest individual shareholder until May 2014.";
            var sutResult = await service.ExtractEntityLinksAsync(SutText);
            Assert.IsTrue(sutResult != null);
            Assert.IsTrue(sutResult.Any());
            var sutFirst = sutResult.FirstOrDefault();
            Assert.IsTrue(sutFirst.Matches.Any());
            Assert.IsTrue(sutFirst.Name.Length > 0);
        }

        [TestMethod]
        public async Task TextAnalytics_Opinion()
        {
            SutText = "The food and service were unacceptable, but the concierge were nice.";
            var sutResult = await service.ExtractOpinionAsync(SutText);
            Assert.IsTrue(sutResult != null);
            Assert.IsTrue(sutResult.Any());
            var sutFirst = sutResult.FirstOrDefault();
            Assert.IsTrue(sutFirst.DocumentSentiment != null);
            Assert.IsTrue(sutFirst.OpinionSentiments != null);
            Assert.IsTrue(sutFirst.SentenceOpinion != null);
            Assert.IsTrue(sutFirst.SentenceSentiment != null);
        }

        [TestMethod]
        public async Task TextAnalytics_Sentiment()
        {
            SutText = "I had the best day of my life. I wish you were there with me.";
            var sutResult = await service.AnalyzeSentimentAsync(SutText);
            Assert.IsTrue(sutResult != null);
        }
    }
}
