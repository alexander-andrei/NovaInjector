using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaInjector
{
    public class InjectionTest
    {
        private string _supInj;

        public InjectionTest(SuperInjectionTest test, string x, string z)
        {
            _supInj = test.HelloWorld() + x + z;
        }

        public string test()
        {
            return _supInj;
        }
    }
}
