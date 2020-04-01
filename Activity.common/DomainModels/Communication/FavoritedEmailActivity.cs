using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class FavoritedEmailActivity : BaseActivity
    {
        public String UserEmailInstanceId { get; set; }
        public String EmailSubject { get; set; }

        public override string ActivityType => nameof(FavoritedEmailActivity);
    }
}
