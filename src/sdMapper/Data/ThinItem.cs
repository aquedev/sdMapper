using System;
using System.Collections.Generic;
using System.Linq;

namespace sdMapper.Data
{
    public class ThinItem
    {
        public ThinField this[string fieldName]
        {
            get { return _fields.Single(fld => fld.Name.Equals(fieldName)); }
        }

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

        public void AddField(string name, string value)
        {
            _fields.Add(new ThinField { Name = name, Value = value });
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
