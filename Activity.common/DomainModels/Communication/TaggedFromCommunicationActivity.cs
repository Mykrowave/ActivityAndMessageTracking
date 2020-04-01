using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class TaggedFromCommunicationActivity : BaseActivity
    {
        public String TaggedEntityType { get; set; }
        public String TaggedEntityId { get; set; }
        public String TaggedEntityText { get; set; }

        public override string ActivityType => nameof(TaggedFromCommunicationActivity);
    }
}
