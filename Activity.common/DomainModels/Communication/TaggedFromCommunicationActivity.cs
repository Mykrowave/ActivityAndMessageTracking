using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.DomainModels.Communication
{
    public class TaggedFromCommunicationActivity : ActivityType
    {
        public String TaggedEntityType { get; set; }
        public String TaggedEntityId { get; set; }
        public String TaggedEntityText { get; set; }

    }
}
