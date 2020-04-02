using Activity.common.DomainModels.Communication;
using Activity.common.SearchFilters.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Activity.common.Repositories.Communication
{
    public interface IDownloadedFromCommunicationActivityRepository : IActivityRepository<DownloadedFromCommunicationActivity>
    {

        Task<List<DownloadedFromCommunicationActivity>> Query(String tenant, DownloadedFromCommunicationActivityFilter filter);
    }
}
