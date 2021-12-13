using CsvHelper.Configuration;
using lab.LocalCosmosDbApp.ViewModels;
using TinyCsvParser.Mapping;

namespace lab.LocalCosmosDbApp.Helpers
{
    public class BusinessUnitToolInfoCsvHelperMap : ClassMap<BusinessUnitToolInfoViewModel>
    {
        public BusinessUnitToolInfoCsvHelperMap() : base()
        {
            Map(m => m.BU).Name("BU");
            Map(m => m.FiscalQuarter).Name("FiscalQuarter");
            Map(m => m.ToolIndex).Name("ToolIndex");
            Map(m => m.Tool).Name("Tool");
            Map(m => m.Building).Name("Building");
            Map(m => m.Platform).Name("Platform");
            Map(m => m.Products).Name("FiscalQuarter");
            Map(m => m.Applications).Name("Applications");
            Map(m => m.BaysInUse).Name("BaysInUse");
            Map(m => m.BaysNeeded).Name("BaysNeeded");
            Map(m => m.InstalledChambers).Name("InstalledChambers");
            Map(m => m.PossibleChambers).Name("PossibleChambers");
            Map(m => m.BaySize).Name("BaySize");
        }
    }

    public class BusinessUnitToolInfoTinyCsvParserMap : CsvMapping<BusinessUnitToolInfoViewModel>
    {
        public BusinessUnitToolInfoTinyCsvParserMap() : base()
        {
            MapProperty(0, x => x.BU);
            MapProperty(1, x => x.FiscalQuarter);
            MapProperty(2, x => x.ToolIndex);
            MapProperty(3, x => x.Tool);
            MapProperty(4, x => x.Building);
            MapProperty(5, x => x.Platform);
            MapProperty(6, x => x.Products);
            MapProperty(7, x => x.Applications);
            MapProperty(8, x => x.ToolCount);
            MapProperty(9, x => x.BaysInUse);
            MapProperty(10, x => x.BaysNeeded);
            MapProperty(11, x => x.InstalledChambers);
            MapProperty(12, x => x.PossibleChambers);
            MapProperty(13, x => x.BaySize);
        }
    }
}
