using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sdMapper.Data;

namespace sdMapper.Specs.Support
{
    public class BookMap : Map<Book>
    {
        /// <summary>
        /// Initializes a new instance of the BookMap class.
        /// </summary>
        public BookMap()
        {
            MapProperty(book => book.Title);
        }

        public override string TemplatePath
        {
            get { throw new NotImplementedException(); }
        }
    }
}
