﻿using GoodToCode.Shared.Analytics.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodToCode.Shared.Analytics.CognitiveServices
{
    public interface ITextAnalyzerService
    {
        Task<string> DetectLanguageAsync(string text);
        Task<KeyPhraseResult> ExtractKeyPhrasesAsync(string text);
        Task<ISentimentResult> AnalyzeSentimentAsync(string text);
        Task<IList<ISentimentResult>> AnalyzeSentimentBatchAsync(string text);
        string[] SplitParagraph(string paragraph);
    }
}