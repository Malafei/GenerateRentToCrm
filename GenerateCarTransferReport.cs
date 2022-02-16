using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGenerator
{
    public static class GenerateCarTransferReport
    {
        private static Random random = new Random();


        public static bool GenereteDamageReport()
        {
            int tmp = random.Next(100);
            if (tmp <= 5)
                return true;
            return false;
        }


    }
}
