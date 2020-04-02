using Activity.common.DomainModels.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityAndMessageTracking.web.Models
{
    public class CommunicationActivities
    {
        public List<SentCampaignEmailActivity> SentEmailActivities { get; set; }
        public List<DownloadedFromCommunicationActivity> DownloadedFromCommunicationActivities { get; set; }
    }
}
