using System;
using System.Collections.Generic;
using System.IO;
using Authlete.Util;
using System.Text;

namespace CRM.Helpers
{
    public class PropertiesUtil
    {
        public static IDictionary<string, string> LoadProperty(string propertiesURL)
        {
            try
            {
                IDictionary<string, string> prop = null;
                using (TextReader reader = new StreamReader(propertiesURL))
                {
                    prop = PropertiesLoader.Load(reader);
                }
                return prop;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
