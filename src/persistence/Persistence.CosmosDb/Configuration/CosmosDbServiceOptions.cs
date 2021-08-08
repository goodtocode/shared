﻿namespace GoodToCode.Shared.Persistence.CosmosDb
{
    public class CosmosDbServiceOptions : ICosmosDbServiceConfiguration
    {
        public string ContainerName { get; set; }

        public string PartitionKeyPath { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}