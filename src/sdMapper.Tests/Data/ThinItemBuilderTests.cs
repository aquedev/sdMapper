using System;
using sdMapper.Data;
using Xunit;

namespace sdMapper.Tests.Data
{
    public class ThinItemBuilderTests
    {
        private readonly ThinItemBuilder _builder;
        
        public ThinItemBuilderTests()
        {
            _builder = new ThinItemBuilder();
        }

        [Fact]
        public void AddField_WithFieldNameAndValue_AddsFieldToBuiltThinItem()
        {
            string fieldName = "name";
            string fieldValue = "value";
            _builder.AddField(fieldName, fieldValue);

            var item = _builder.Build();
           
            var field = item[fieldName];
            Assert.Equal(1, item.Fields.Count);
            Assert.Equal(fieldValue, field.Value);
        }

        [Fact]
        public void AddField_WithField_AddsFieldToBuiltThinItem()
        {
            var field = new ThinField() { Id = Guid.NewGuid(), Name = "FieldName", Value = "FieldValue", Type = "Text" };

            _builder.AddField(field);

            var item = _builder.Build();

            Assert.Contains<ThinField>(field, item.Fields);
        }

        [Fact]
        public void WithName_SetsItemsName()
        {
            string itemName = "itemName";
            _builder.WithName(itemName);

            Assert.Equal(itemName, _builder.Build().Name);
        }

        [Fact]
        public void WithTemplate_SetsTemplateOnItem()
        {
            Guid id = Guid.NewGuid();
            string name = "templateName";

            _builder.Template(id, name);
            var template = _builder.Build().Template;

            Assert.Equal(id, template.Id);
            Assert.Equal(name, template.Name);
        }

        [Fact]
        public void WithParent_SetsTheCurrentParent()
        {
            ThinItem item = new ThinItem();

            _builder.WithParent(item);

            Assert.Same(item, _builder.Build().Parent);
        }
    }
}
