using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class f
    {
        [FunctionName("f")]
        public static void Run([CosmosDBTrigger(
            databaseName: "csharpguitar",
            containerName: "elx",
            Connection = "COSMOS_DB_CONNECTION_STRING",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)]IReadOnlyList<ToDoItem> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents retrieved in this invocation is " + input.Count);
                
                Random r = new Random();
                var n = r.Next(1,100);
                if(n % 10 == 0) {
                    try {
                        throw new Microsoft.Azure.WebJobs.Host.FunctionTimeoutException("Something unexpected happened...help me fix it");
                    } catch (Exception e) {
                        log.LogInformation($"Exception {e.HResult}, message: {e.Message}");
                    }
                }

                int iteration = 1;
                int length = 1000;
                if(input.Count > 40 && input.Count <= 50) length = 450;
                if(input.Count > 50) length = 250;

                string memoryTaker = "--WHAT-DOES-IMMUTABLE-MEAN";
                foreach(var document in input) {
                    n = r.Next(1, 100);
                    log.LogInformation($"Porcessing document with Id: {document.id} ({iteration} of {input.Count})");
                    iteration++;

                    if(n % 2 == 0) {
                        for(int i = 0; i < length; i++) {
                            memoryTaker = memoryTaker + "--WHAT-DOES-IMMUTABLE-MEAN--WHAT-DOES-IMMUTABLE-MEAN--WHAT-DOES-IMMUTABLE-MEAN";
                            System.Threading.Thread.Sleep(10);
                            if(i % 50 == 0) {
                                log.LogInformation($"Processing, iteration: {i} conuming: {memoryTaker.Length}");
                            }
                        }
                        if(n % 4 == 0) {
                            try {
                                throw new Microsoft.Azure.WebJobs.Host.RecoverableException("A transient issue happened, retrying....");
                            } catch (Exception ex) {
                                log.LogInformation($"A '{ex.HResult}' exception happened with the message: '{ex.Message}' so will retry....");
                            }
                        }
                    } else {
                        for (int i = 0; i < length; i++) {
                            System.Threading.Thread.Sleep(10);
                            if(i % 50 == 0) {
                                log.LogInformation($"Processing, iteration: {i}");
                            }
                        }
                    }
                }
                if(n % 50 == 0) throw new AccessViolationException("Why did you do that? You should know better");
            }
        }
    }

    // Customize the model with your own desired properties
    public class ToDoItem
    {
        public string id { get; set; }
        public string Description { get; set; }
    }
}
