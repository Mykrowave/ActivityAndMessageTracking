using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.data.cosmosdb.Helpers
{
    public class CosmosSQLQuery
    {
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
        public String Query { get; set; }
    }
}
