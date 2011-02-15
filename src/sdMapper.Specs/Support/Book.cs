using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sdMapper.Specs.Support
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int NumberOfViews { get; set; }
    }
}
