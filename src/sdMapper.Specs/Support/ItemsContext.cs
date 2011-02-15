using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sdMapper.Data;

namespace sdMapper.Specs.Support
{
    public class BookMap : Map<Book>
    {
        public override string TemplatePath
        {
            get { throw new NotImplementedException(); }
        }
    }
}
