﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using System;

namespace GoodToCode.Shared.TextAnalytics
{
    public class AppConfigurationFactory
    {
        public IConfiguration Configuration { get; private set; }
        public IConfiguration Create()
        {
            var environment = Environment.GetEnvironmentVariable(EnvironmentVariableKeys.EnvironmentAspNetCore) ?? EnvironmentVariableDefaults.Environment;
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration(options =>
                    options
                        .Connect(Environment.GetEnvironmentVariable(EnvironmentVariableKeys.AppSettingsConnection))
                        .ConfigureRefresh(refresh =>
                        {
                            refresh.Register(AppConfigurationKeys.SentinelSetting, refreshAll: true)
                                    .SetCacheExpiration(new TimeSpan(0, 60, 0));
                        })
                        .Select(KeyFilter.Any, LabelFilter.Null)
                        .Select(KeyFilter.Any, environment)
                    );
            Configuration = builder.Build();
            return Configuration;
        }
    }
}