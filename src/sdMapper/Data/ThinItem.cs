using System;
using System.Collections.Generic;

namespace sdMapper.Data
{
    public class ThinItem
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public ThinItem Parent { get; set; }
        public IList<ThinItem> Children { get; set; }

        public ThinItemTemplate Template { get; set; }

        private IList<ThinField> _fields;
        public IList<ThinField> Fields
        {
            get { return _fields; }
        }

        public ThinItem()
        {
            _fields = new List<ThinField>();
        }
    }

    public class ThinItemTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ThinField
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
