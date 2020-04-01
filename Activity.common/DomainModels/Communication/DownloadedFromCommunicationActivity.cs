using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class DownloadedFromCommunicationActivity
    {
        public String FileType { get; set; }
        public String FileId { get; set; }
        public String FileText { get; set; }
        public String MessageSourceType { get; set; }
        public String MessageCampaignId { get; set; }
        public int MessageSession { get; set; }
        public String MessageText { get; set; }
    }
}
