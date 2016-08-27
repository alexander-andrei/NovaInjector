using NovaInjector.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaInjector
{
    public class NovaServices
    {
        private static string _jsonFile;

        public NovaServices(string jsonFile)
        {
            _jsonFile = jsonFile;
        }

        public static dynamic get(string NameSpace)
        {
            var container = new Container(_jsonFile, NameSpace);

            return container.ParseJsonFile();
        }
    }
}
