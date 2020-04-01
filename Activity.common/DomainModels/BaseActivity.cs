using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels
{
    public abstract class BaseActivity
    {
        public String Id { get; set; }
        public String Tenant { get; set; }
        public String ActionUserId { get; set; }
        public ActionUser ActionUser { get; set; }
        public DateTime ActionDate { get; set; }
        public abstract String ActivityType { get; }
        public String PartitionKey => $"{Tenant}-{ActivityType}";   //TODO: Is there anyway to remove this property from Domain Layer?
    }

    public class ActionUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String ImageRefUrl { get; set; }
    }
}
