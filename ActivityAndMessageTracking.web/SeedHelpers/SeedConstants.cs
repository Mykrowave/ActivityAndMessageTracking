using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityAndMessageTracking.web.SeedHelpers
{
    public class SeedConstants
    {

        public static List<String> GenerateSetOfRandomStringGuidIds(int numberOfTotalunique)
        {
            List<String> results = new List<String>();
            for (int i = 0; i < numberOfTotalunique; i++)
            {
                results.Add(Guid.NewGuid().ToString());
            }

            return results;
        }

        public static String[] Tenants =
        {
            "tenant-1",
            "default",
            "gsba",
            "jsuurt",
            "ee4rr",
            "tweet",
            "gjjou",
            "uipp"
        };

        public static String[] RecipientType =
        {
            "person",
            "tag",
            "group",
            "organization"
        };

    }
}
