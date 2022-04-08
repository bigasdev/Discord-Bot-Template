using System.IO;
using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace discordbottemplate.utilities{
    /// <summary>
    /// Class to extend the use of jsons
    /// </summary>
    public class BJson{
        private static readonly BJson _instance = new();
        public static BJson Instance{
            get{return _instance;}
        }
        /// <summary>
        /// Construct and return a json with type <T>
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> ConstructJson<T>(string fileLocation){
            try
            {
                var json = string.Empty;
    
                using(var fs = File.OpenRead(fileLocation)){
                    using(var sr = new StreamReader(fs)){
                        json = await sr.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
    
                var configJson = JsonConvert.DeserializeObject<T>(json);
                return configJson;
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Probably couldnt find the file {fileLocation}, {e}");
            }
            return default;
        }
    }
}