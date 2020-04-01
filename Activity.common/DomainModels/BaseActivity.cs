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
        public String ActionUser { get; set; }
        public DateTime ActionDate { get; set; }
    }

    public class ActionUser
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String ImageRefUrl { get; set; }
    }
}
