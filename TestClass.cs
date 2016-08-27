using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaInjector
{
    public class TestClass
    {
        private string _o;
        private string _t;
        private string _z;

        public TestClass(string o, string t, string z)
        {
            _o = o;
            _t = t;
            _z = z;
        }

        public string SUPERTEST()
        {
            return _o + _t + _z;
        }
    }
}
