using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityAndMessageTracking.Config
{
    public class CosmosDbConfig
    {
        public String DatabaseName { get; set; }
        public List<CollectionInfo> CollectionNames { get; set; }
    }

    public class CollectionInfo
    {
        public String Name { get; set; }
        public String PartitionKey { get; set; }
    }

}
