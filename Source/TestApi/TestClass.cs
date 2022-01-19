using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi
{
    public class TestClass
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public TestClass() { }

        public TestClass(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
