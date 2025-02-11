using ServiceStack.DataAnnotations;
using System;

namespace Final_Core.Models
{
    [Alias("api_logs")] // Explicitly map this class to the "api_logs" table
    public class ApiLog
    {
        [AutoIncrement]
        [PrimaryKey]
        [Alias("LogID")] // Map the "Id" property to the "LogID" column in the database
        public int Id { get; set; }

        public DateTime Timestamp { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string Route { get; set; }
        public string RequestBody { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }
        public string ClientIP { get; set; }
    }
}