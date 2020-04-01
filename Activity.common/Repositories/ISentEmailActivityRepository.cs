using Activity.common.DomainModels;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Activity.common.Repositories
{
    public interface ISentEmailActivityRepository : IActivityRepository<SentCampaignEmailActivity>
    {
        
    }
}
