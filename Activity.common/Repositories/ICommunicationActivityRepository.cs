using Activity.common.DomainModels;
using Activity.Communication.common.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Activity.common.Repositories
{
    public interface ICommunicationActivityRepository : IActivityRepository<CommunicationActivity>
    {
        //Task<IEnumerable<CommunicationActivity>> GetTaggedActivity(string tenant, )
    }
}
