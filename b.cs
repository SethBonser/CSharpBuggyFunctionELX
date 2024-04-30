using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class b
    {
        [FunctionName("b")]
        public static async Task Run([EventHubTrigger("elx", Connection = "EVENT_HUB_CONNECTION_STRING")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                
                string messageBody = eventData.EventBody.ToString() + " " + eventData.EnqueuedTime.ToString();
                log.LogInformation($"C# Event Hub trigger processed a message: {messageBody}");

                Random r = new Random();
                var zero = 0;
                var number = r.Next(1,100);
                if(number % 2 == 0) {
                    var unCaughtZero = 1 / zero;
                } else {
                    log.LogInformation($"The Event Hub message: {messageBody} was processed succesfully...this time");
                }
                
                await Task.Yield();
                
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
