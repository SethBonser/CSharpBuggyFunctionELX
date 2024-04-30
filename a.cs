using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public class a
    {
        [FunctionName("a")]
        public void Run([BlobTrigger("elx/{name}", Connection = "BLOB_CONNECTION_STRING")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            var zero = 0;
            try {
                Random r = new Random();
                var number = r.Next(1,100);
                if(number % 2 == 0) {
                    var caughtZero = 1 / zero;
                } else {
                    log.LogInformation($"The blob with name: {name} was processed successfully...this time.");
                
                }
            } catch (System.Exception ex) {
                log.LogInformation("A handled exception occcurred.");
                log.LogInformation("Why did this happen and how can I resolve it or find out the issue?");
                log.LogInformation(ex.Message);
            }
        }
    }
}
