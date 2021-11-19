using System.ComponentModel;

namespace lab.LocalCosmosDbApp.Utility
{
    public class Enums
    {
        public enum EntityState
        {
            Added = 1,
            Modified =2,
            Deleted = 3
        }

        public enum CountryEnum
        {
            [Description("United States")]
            UnitedStates = 1,
            [Description("Bangladesh")]
            Bangladesh = 2
        }

        public enum StateEnum
        {
            [Description("Colorado")]
            Colorado = 1,
            [Description("Minnesota")]
            Minnesota = 2,
            [Description("New York")]
            NewYork = 3
        }

        public enum SeasonEnum
        {
            //Spring begins on the 80th day, on a leap year it begins one the 81
            Spring,
            //Summer begins on the 172nd day, on a leap year it begins one the 173
            Summer,
            //Fall the 266th, on a leap year it begins one the 267
            Fall,
            //Winter the 355th, on a leap year it begins one the 356
            Winter
        }
    }
}
