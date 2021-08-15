﻿namespace GoodToCode.Shared.Analytics.Abstractions
{
    public struct HealthcareEntityResult : IAnalyticsResult
    {
        public string Text { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public double Confidence { get; set; }
    }
}