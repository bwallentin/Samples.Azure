﻿using System;
using AppInsightsLabs.Infrastructure;

namespace AppInsightsLabs
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {

            const string connString =
                "DefaultEndpointsProtocol=https;AccountName=adlaisstordev;AccountKey=4ulhZQVgcDs/HXfWMx8ZKTAMcjc2nHCE97zYrrHSA8xufBJR2Ql++t4Z6eKkRvYa3zDM7s9mdP7xXa/HD67bpQ==;EndpointSuffix=core.windows.net";
            const string containerName = "adlibris-product-ais-appinsights-dump";
            const string containerFolder = "adlibris-product-appinsight-dev_c73480e495214ae0916e8ffbe4587732";

            var blobReader = new AppInsightsCloudBlobReader(connString, containerName, containerFolder);
            var aiObserver = new AppInsightsObserver(blobReader);
            aiObserver.OnTraceItemsAdded += blobInfos =>
            {
                foreach (var blobInfo in blobInfos)
                {
                    Console.WriteLine(blobInfo.MessageRaw);
                }
            };

            aiObserver.PopulateTraces();

            Console.WriteLine("\n\nPress any key to exit ..");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}