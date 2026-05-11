using System;

namespace Shared
{
    public class GpiConfig
    {
        public string ConnString_Server { get; set; }
        public string ConnString_Local { get; set; }

        public string DbName_Server() {

            return GetDatabaseName(this.ConnString_Server);


        }

        public string DbName_Local()
        {

            return GetDatabaseName(this.ConnString_Local);


        }

        private   string GetDatabaseName(string connectionString)
        {
            // Split the connection string into key-value pairs
            string[] parameters = connectionString.Split(';');
            foreach (string param in parameters)
            {
                // Check if the parameter starts with "Database="
                if (param.Trim().StartsWith("Database=", StringComparison.OrdinalIgnoreCase))
                {
                    // Return the value after "Database="
                    return param.Substring("Database=".Length).Trim();
                }
            }

            // Return null or throw an exception if not found
            return null;
        }

    }
}
