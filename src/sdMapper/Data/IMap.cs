using System;
using System.Collections.Generic;

namespace sdMapper.Data
{
    public interface IMap
    {
        IList<Mapping> Mappings { get; set; }
        string TemplatePath { get;  }
    }
}
