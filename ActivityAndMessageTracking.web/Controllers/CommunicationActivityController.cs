using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Activity.common.Communication.Repositories;
using Activity.common.DomainModels;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories.Communication;
using Activity.common.SearchFilters.Communication;
using ActivityAndMessageTracking.web.Models;
using ActivityAndMessageTracking.web.SeedHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ActivityAndMessageTracking.web.Controllers
{
    public class CommunicationActivityController : Controller
    {
        private readonly ISentEmailActivityRepository _sentEmailRepository;
        private readonly IDownloadedFromCommunicationActivityRepository _downloadedRepository;

        public CommunicationActivityController(ISentEmailActivityRepository sentEmailRepository, IDownloadedFromCommunicationActivityRepository downloadedRepository)
        {
            _sentEmailRepository = sentEmailRepository;
            _downloadedRepository = downloadedRepository;
        }



        // TODO: Move Seed Functionality to External Console App
        [HttpPost]
        public async Task<IActionResult> CreateRandomActivities()
        {
            await ActivityGenerator.GenerateActivities(
                _sentEmailRepository, 
                _downloadedRepository);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetActivities(String tenant = "gsba")
        {
            //hardcode Filter for debug
            CommunicationActivities result = new CommunicationActivities(); 

            SentCampaignEmailActivityFilter sentEmailfilter = new SentCampaignEmailActivityFilter
            {
                Start = DateTime.Now.AddDays(-3),
                End = DateTime.Now
            };

            result.SentEmailActivities = await _sentEmailRepository.Query(tenant, sentEmailfilter);

            DownloadedFromCommunicationActivityFilter downloadedFilter = new DownloadedFromCommunicationActivityFilter
            {
                Start = DateTime.Now.AddDays(-14),
                End = DateTime.Now
            };
            result.DownloadedFromCommunicationActivities = await _downloadedRepository.Query(tenant, downloadedFilter);
           
            return Ok(result);
        }


        

        
        
    }
}
