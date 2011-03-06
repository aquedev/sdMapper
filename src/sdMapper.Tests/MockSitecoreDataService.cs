using System;
using sdMapper.Data;

namespace sdMapper.Tests
{
    public class MockSitecoreDataService : ISitecoreDataService
    {
        public static class Guids
        {
            public static Guid ItemWithCheckedCheckboxField = Guid.NewGuid();
        }

        public static class TestFields
        {
            public static readonly ThinField CheckboxField;

            static TestFields()
            {
                CheckboxField = new ThinField { Id = Guid.NewGuid(), Name = "CheckboxField", Value = "1", Type = "Checkbox" };
            }
        }

        public ThinItem GetItem(Guid id)
        {
            if (id == Guids.ItemWithCheckedCheckboxField)
                    return GetItemWithCheckboxField();
              return null;
        }

        private ThinItem GetItemWithCheckboxField()
        {
            return new ThinItemBuilder()
                .WithName("Item with checkbox field")
                .AddField(TestFields.CheckboxField)
                .Build();
        }

        public ThinItem GetItem(string path)
        {
            throw new NotImplementedException();
        }


    }
}
