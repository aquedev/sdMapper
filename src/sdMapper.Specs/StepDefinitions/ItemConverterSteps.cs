using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using sdMapper.Specs.Support;
using sdMapper.Data;
using Xunit;
using sdMapper.Tests;

namespace sdMapper.Specs.StepDefinitions
{
    [Binding]
    public class ItemConverterSteps
    {
        private ThinItem _currentItem;
        readonly List<ThinItem> _items = new List<ThinItem>();
        private IMap _currentMap = new BookMap();
        private Book _convertedItem;

        [BeforeScenario]
        public void SetupMapper()
        {
            new MapperSetup().Setup();
        }

        [Given(@"the following items")]
        public void GivenTheFollowingItems(Table table)
        {
            foreach (var row in table.Rows)
            {
                ThinItem item = new ThinItem();
                item.Name = row["Name"];
                item.AddField("Title", row["Title"]);
                item.AddField("Body", row["Body"]);
                item.AddField("NumberOfViews", row["NumberOfViews"]);
                item.AddField("Image", row["Image"]);
                _items.Add(item);
            }
        }

        [Given(@"I have item with name (\w*)")]
        public void GivenItemWithName(string name)
        {
            _currentItem = _items.Single(item => item.Name == name);
        }

        [Given(@"I have BookMap without a Mapping for (.*)")]
        public void GivenIHaveBookMapWithoutAMappingForName(string fieldName)
        {
            Assert.Null(_currentMap.Mappings.SingleOrDefault(mapping => mapping.FieldName == fieldName));
        }

        [Given(@"I have BookMap with Mapping for (.*)")]
        public void GivenIHaveBookMapWithMappingForTitle(string fieldName)
        {
            Assert.NotNull(_currentMap.Mappings.SingleOrDefault(mapping => mapping.FieldName == fieldName));
        }

        [When(@"I Convert the item")]
        public void WhenIConvertTheItem()
        {
            ItemConverter converter = Mapper.Resolver.Resolve<ItemConverter>();
            _convertedItem = converter.Convert(_currentItem, _currentMap) as Book;
        }

        [Then(@"the resulting book entity's Name property is set to (.*)")]
        public void ThenTheResultingBookEntitySNamePropertyIsSetToItem1(string value)
        {
            Assert.Equal(value, _convertedItem.Name);
        }

        [Then(@"the resulting book entity's Title proprety is ""(.*)""")]
        public void ThenTheResultingBookEntitySTitlePropretyIsBlueWhale(string value)
        {
            Assert.Equal(value, _convertedItem.Title);
        }
    }
}
