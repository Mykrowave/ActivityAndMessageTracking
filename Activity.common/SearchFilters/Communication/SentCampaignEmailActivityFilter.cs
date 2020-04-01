using Activity.common.DomainModels.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.SearchFilters.Communication
{
    public class SentCampaignEmailActivityFilter : BaseSearchFilter
    {
        public override string ActivityTypeName => nameof(SentCampaignEmailActivity);
        public List<CommunicationRecipient> CommunicationRecipients { get; set; } = new List<CommunicationRecipient>();
        public String EmailSubject { get; set; }

    }
}
