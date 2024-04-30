using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class g
    {
        [FunctionName("g")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            Random r = new Random();
            var number = r.Next(1, 100);
            if (number %3 == 0) {
                try {
                    System.Threading.Thread.Sleep(65000);
                    if(number % 5 == 0) {
                        throw new SystemException("Something kind of expected happened, I didn't have time to test completely, Thank goodness I used a try..catch...");

                    }
                } catch (Exception ex) {
                    log.LogInformation($" I caught the exception, here is the message: '{ex.Message}'");
                }
            } else {
                if (number % 3 == 0) {
                    System.Threading.Thread.Sleep(5000);
                    log.LogInformation($"C# Timer trigger is working slow...I'm getting nervous");
                    System.Threading.Thread.Sleep(20000);
                    log.LogInformation("An Unhandled exception was thrown, at least that's what I think.");
                } else {
                    System.Threading.Thread.Sleep(25000);
                    log.LogInformation($"C# Timer trigger fuction successfully completed at: {DateTime.Now}");
                }
            }
        }
    }
}
