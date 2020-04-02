using Activity.common.DomainModels.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.SearchFilters.Communication
{
    public class DownloadedFromCommunicationActivityFilter : BaseSearchFilter
    {
        public List<String> FileTypes { get; set; }

        public override string ActivityTypeName => nameof(DownloadedFromCommunicationActivity);
    }
}
