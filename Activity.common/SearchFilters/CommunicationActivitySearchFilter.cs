using Activity.common.DomainModels.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.SearchFilters
{
    public class CommunicationActivitySearchFilter
    {
        public String ActionUserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public List<String> ActivityTypes { get; set; } = new List<String>();
        public List<CommunicationRecipient> Recipients { get; set; } = new List<CommunicationRecipient>();
    }

}
