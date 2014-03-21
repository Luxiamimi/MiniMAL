using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public class Argument
    {
        public Func<string, bool> Validator { get; set; }
        public bool isOptional { get; set; }

        public Argument(Func<string, bool> validator, bool optional = false)
        {
            Validator = validator;
            isOptional = optional;
        }

        public bool isValid(string s)
        {
            return Validator.Invoke(s);
        }
    }
}
