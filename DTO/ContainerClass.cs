using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaInjector.DTO
{
    public class ContainerClass
    {
        public string Name { get; set; }
        public IList<string> Constructor { get; set; }
        public IList<string> Methods { get; set; }
    }
}
