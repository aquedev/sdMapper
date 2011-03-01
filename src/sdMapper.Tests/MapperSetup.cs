using System;
using sdMapper.StructureMapAdapter;
using StructureMap;
using sdMapper.Data;


namespace sdMapper.Tests
{
    public class MapperSetup
    {
        public void Setup()
        {
            Mapper.Initialise(new StructureMapServiceResolver());
            InitiliseStructureMap();
        }

        private void InitiliseStructureMap()
        {
            ObjectFactory.Initialize(init => {
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
