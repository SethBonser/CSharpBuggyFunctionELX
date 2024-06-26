using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class c
    {
        [FunctionName("c")]
        public void Run([ServiceBusTrigger("elx", Connection = "SERVICE_BUS_CONNECTION_STRING")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            List<string> manufacturers = new List<string>();

            Random r = new Random();
            var n = r.Next(1, 100);

            if(n % 5 == 0) {
                manufacturers.Add(GetManufacturer("\"Gibson Fender Charvel Taylor Jackson MartinandCompany Dean Epiphone Tamine"));
            }
        }
        public static string GetManufacturer(string manufacturer) {
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(manufacturer, "\"(([^\\\\\"]*)(\\\\.)?)*\"");
            return m.ToString();
        }
    }
}
