using System;
using Xunit;
using sdMapper.Data;
using System.Collections.Generic;

namespace sdMapper.Tests
{
    public class ItemConverterTests
    {
        private const string BodyFieldValue = "SampleText";
        private const string VoteCountValue = "10";
        private static readonly Guid SourceItemId = new Guid();

        [Fact]
        public void LoadEntity_ThowsIfEntityDoesntContainPropretyIdOfTypeGuid()
        {
            var loader = GetLoader();
            Assert.Throws<InvalidOperationException>(() => loader.Convert(GetSourceItem(), new MockEntityMap()));
        }

        private static ItemConverter GetLoader()
        {
            return new ItemConverter(new List<IFieldConverter>());
        }

        private static ThinItem GetSourceItem()
        {
            var source = new ThinItem();
            source.Template = new ThinItemTemplate { Id = new Guid(), Name = "MockTemplate" };
            source.Name = "MockItem";
            source.Id = SourceItemId;
            ThinField bodyField = new ThinField { Name = "Body", Value = BodyFieldValue };
            ThinField voteField = new ThinField { Name = "VoteCount", Value = VoteCountValue };
            source.Fields.Add(bodyField);
            source.Fields.Add(voteField);

            return source;
        }

        public class MockEntityMap : Map<MockEntity>
        {
            
            public MockEntityMap()
            {
                MapProperty(ent => ent.Body);
                MapProperty(ent => ent.Votes).To("VotesCount");
            }

            public override string TemplatePath
            {
                get { throw new NotImplementedException(); }
            }
        }

        public class MockEntityWithoutId
        {
            public string Name { get; set; }
        }

        public class MockEntity
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public string Body { get; set; }
            public int Votes { get; set; }

        }
        
    }



    public class SitecoreSessionTests
    {
        Mapper _mapper = new Mapper();


        //want to load an object from session
        //want to convert an object from sitecoreItem
        //want to convert string field to string property
        //want to convert int field to int property
        
    }
}
