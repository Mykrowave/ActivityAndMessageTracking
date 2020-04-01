using Activity.common.DomainModels;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories;
using Activity.common.SearchFilters.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Activity.common.Communication.Repositories
{
    public interface ISentEmailActivityRepository : IActivityRepository<SentCampaignEmailActivity>
    {
        Task<List<SentCampaignEmailActivity>> Query(String tenant, SentCampaignEmailActivityFilter filter);
    }

    
}
