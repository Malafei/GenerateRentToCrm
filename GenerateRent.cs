using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public static class GenerateRent
    {
        private static Random random = new Random();

        public static DateTime GetRandomDate_Pickup(DateTime startPos, DateTime endPos)
        {
            return startPos.AddDays(random.Next((endPos - startPos).Days));
        }

        public static DateTime GetRandomDate_Returned(DateTime startPos, DateTime endPos)
        {
            DateTime ReturnDate = startPos.AddDays(random.Next(1, 30));
            if (ReturnDate < endPos)
                return ReturnDate;
            else
                return endPos;
        }

        public static Guid GetRandomEntityReference(List<Guid> guids)
        {
            return guids.ElementAt(random.Next(0, (guids.Count)));
        }

        public static Money GetRandomCurency()
        {
            return new Money(random.Next(100, 500));
        }

        public static Guid GetRandomCar(Dictionary<Guid, Guid> Cars, Guid carClass)
        {
            var newDictionary = Cars.Where(c => c.Value == carClass);
            if (newDictionary.Count() > 0)
                return newDictionary.ElementAt(random.Next((newDictionary.Count()))).Key;
            
            return Guid.Empty;
        }

        public static OptionSetValue GetRandomOptionsPickup_Returned()
        {
            return new OptionSetValue(754300000 + random.Next(0, 3));
        }
        public static OptionSetValue GetStatusState(OptionSetValue statusReason)
        {
            if (statusReason.Value <= 754300002)
                return new OptionSetValue(0);
            else
                return new OptionSetValue(1);
        }

        public static OptionSetValue GetRandomStatusCode()
        {
            int tmp = random.Next(101);
            if (tmp >= 16 && tmp <= 90)
                return new OptionSetValue(754300004);
            else if (tmp >= 91 && tmp <= 100)
                return new OptionSetValue(754300003);
            else if (tmp >= 6 && tmp <= 10)
                return new OptionSetValue(754300001);
            else if (tmp >= 11 && tmp <= 15)
                return new OptionSetValue(754300002);
            else
                return new OptionSetValue(754300000);
        }

        public static bool GetRandomPaided(OptionSetValue statusReason)
        {
            int tmp = random.Next(10001);
            switch (statusReason.Value)
            {
                case 754300001:
                    if (tmp <= 9000)
                        return true;
                    return false;
                case 754300002:
                    if (tmp <= 9990)
                        return true;
                    return false;
                case 754300004:
                    if (tmp <= 9998)
                        return true;
                    return false;
                default:
                    if (tmp >= 9998)
                        return true;
                    return false;
            }
        }


    }
}
