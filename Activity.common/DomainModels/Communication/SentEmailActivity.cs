using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class FavoritedEmailActivity : ActivityType
    {
        public String UserEmailInstanceId { get; set; }
        public String EmailSubject { get; set; }

    }
}
