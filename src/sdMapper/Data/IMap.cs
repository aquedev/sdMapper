using System;
using System.Collections.Generic;

namespace sdMapper.Data
{
    public interface IMap
    {
        Type EntityType { get; }
        IList<Mapping> Mappings { get; }
        string TemplatePath { get;  }
    }
}
