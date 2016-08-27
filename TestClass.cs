using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaInjector
{
    public class TestClass
    {
        private InjectionTest _o;
        private string _t;
        private string _z;

        public TestClass(InjectionTest o, string t, string z)
        {
            _o = o;
            _t = t;
            _z = z;
        }

        public string SUPERTEST()
        {
            return _o.test();
        }
    }
}
