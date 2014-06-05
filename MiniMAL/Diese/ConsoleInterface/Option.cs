﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diese.ConsoleInterface
{
    public struct Option
    {
        public string Keyword { get; set; }
        public string Description { get; set; }
        public List<Argument> Arguments { get; set; }

        public Option(string keyword, string description = "*no description*", params Argument[] arguments)
            : this()
        {
            Keyword = keyword;
            Description = description;
            Arguments = arguments.ToList();
        }
    }
}
