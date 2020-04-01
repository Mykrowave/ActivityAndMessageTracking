using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class SentCampaignEmailActivity : ActivityType
    {
        public String CampaignId { get; set; }
        public int Session { get; set; }
        public String EmailSubject { get; set; }
        public List<CommunicationRecipient> Recipients { get; set; } = new List<CommunicationRecipient>();
    }
}
