using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Shared
{
    public static class GPI
    {
        public static string JsonFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\GpiConfig.json";
        public static string jsonString = File.ReadAllText(JsonFilePath, Encoding.UTF8);
        public static string Status="";
        public static GpiConfig GetConfig()
        {
            try
            {
                if (File.Exists(JsonFilePath))
                {
                    GpiConfig config = JsonConvert.DeserializeObject<GpiConfig>(jsonString);
                    
                    return config;
                }
                else
                {

                    throw new Exception($@"GPI Configuration File Not Found @ [{JsonFilePath}] .");
                }

            }
            catch (System.Exception ex)
            {
                throw new Exception($"GPI Configuration File Error, Error Message = [{ex.Message}] .");
            }
        }

       


    }
}
