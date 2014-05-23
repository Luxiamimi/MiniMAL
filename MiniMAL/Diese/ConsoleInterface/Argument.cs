using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public class Argument
    {
        public Func<string, bool> Validator { get; set; }
        public string MessageIfUnvalid { get; set; }

        public Argument(Func<string, bool> validator, string msgIfUnvalid = "")
        {
            Validator = validator;
            MessageIfUnvalid = msgIfUnvalid;
        }

        public bool isValid(string s)
        {
            return Validator.Invoke(s);
        }
    }
}
