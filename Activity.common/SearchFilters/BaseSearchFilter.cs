using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.SearchFilters
{
    public abstract class BaseSearchFilter
    {
        public String ActionUserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public abstract String ActivityTypeName { get; } 
    }
}
