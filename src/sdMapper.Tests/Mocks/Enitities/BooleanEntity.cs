using System;
using sdMapper.Data;

namespace sdMapper.Tests.Mocks.Enitities
{
    public class BooleanEntity : MockEntityBase
    {
        public bool BooleanProperty { get; set; }
    }

    public class BooleanEntityMap : Map<BooleanEntity>
    {
        public BooleanEntityMap()
        {
            MapProperty(ent => ent.BooleanProperty).To(MockSitecoreDataService.TestFields.CheckboxField.Name);
        }

        public override string TemplatePath
        {
            get { throw new NotImplementedException(); }
        }
    }

}
