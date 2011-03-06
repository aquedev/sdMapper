using System;
using sdMapper.StructureMapAdapter;
using StructureMap;
using sdMapper.Data;


namespace sdMapper.Tests
{
    public class MapperSetup
    {
        public Mapper Setup()
        {
            InitiliseStructureMap();
            return Mapper.Initialise(new StructureMapServiceResolver());
        }

        private void InitiliseStructureMap()
        {
            ObjectFactory.Initialize(init => {

                init.For<ISitecoreDataService>().Use<MockSitecoreDataService>();

                init.Scan(scanner => {
                    scanner.AssemblyContainingType<Mapper>();
                    scanner.AssemblyContainingType<MapperSetup>();
                    
                    scanner.WithDefaultConventions();
                    scanner.AddAllTypesOf<IFieldConverter>();
                    scanner.ConnectImplementationsToTypesClosing(typeof(Map<>));
                });
            
            });

        }
    }
}
