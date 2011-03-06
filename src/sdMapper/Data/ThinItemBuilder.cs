using System;

namespace sdMapper.Data
{
    public class ThinItemBuilder
    {
        ThinItem _item = new ThinItem();

        public ThinItemBuilder AddField(string name, string value)
        {
            _item.AddField(name, value);
            return this;
        }

        public ThinItemBuilder AddField(ThinField field)
        {
            _item.Fields.Add(field);
            return this;
        }

        public ThinItemBuilder WithName(string name)
        {
            _item.Name = name;
            return this;
        }

        public ThinItemBuilder WithParent(ThinItem item)
        {
            _item.Parent = item;
            return this;
        }

        public ThinItemBuilder Template(Guid id, string name)
        {
            _item.Template = new ThinItemTemplate { Id = id, Name = name };
            return this;
        }

        public ThinItem Build()
        {
            return _item;
        }
    }
}
