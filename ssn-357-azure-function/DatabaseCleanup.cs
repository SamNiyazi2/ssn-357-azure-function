using System;


// 04/22/2022 03:11 am - SSN
// Souece: https://docs.microsoft.com/en-us/azure/azure-functions/functions-scenario-database-table-cleanup#add-a-timer-triggered-function

using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace ssn_357_azure_function
{
    internal class DatabaseCleanup
    {


        [FunctionName("ssn_357_DatabaseCleanup")]
        public static async Task Run([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            // Get the connection string from app settings and use it to create a connection.
            var str = Environment.GetEnvironmentVariable("sqldb_connection");
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "insert into [ssn-357].[ssn-357-TestTable] (testRecord) values ( 'Test from Azure Function')  ";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    // Execute the command and log the # rows affected.
                    var rows = await cmd.ExecuteNonQueryAsync();
                    log.LogInformation($"{rows} rows were updated");
                }
            }
        }


    }
}
