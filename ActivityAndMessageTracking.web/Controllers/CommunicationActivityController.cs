using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Activity.common.Communication.Repositories;
using Activity.common.DomainModels;
using Activity.common.DomainModels.Communication;
using Activity.common.SearchFilters.Communication;
using ActivityAndMessageTracking.web.SeedHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ActivityAndMessageTracking.web.Controllers
{
    public class CommunicationActivityController : Controller
    {
        private readonly ISentEmailActivityRepository _sentEmailRepository;

        public CommunicationActivityController(ISentEmailActivityRepository sentEmailRepository)
        {
            _sentEmailRepository = sentEmailRepository;
        }

        // TODO: Move Seed Functionality to External Console App
        [HttpPost]
        public async Task<IActionResult> CreateRandomActivities()
        {
            List<String> userIds = SeedConstants.GenerateSetOfRandomStringGuidIds(10);
            List<String> groupIds = SeedConstants.GenerateSetOfRandomStringGuidIds(3);
            List<String> tagIds = SeedConstants.GenerateSetOfRandomStringGuidIds(5);
            Random rnd = new Random();

            
            var generatedSentEmailActivities = new List<SentCampaignEmailActivity>();
            for(int i = 0; i < 50; i++)
            {
                generatedSentEmailActivities.Add(
                    _CreateRandomSentEmailActivity(userIds, groupIds, tagIds, rnd));
            }
            var storageResult = 
                await _sentEmailRepository.AddRangeAsync(generatedSentEmailActivities);


            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            //hardcode Filter for debug
            SentCampaignEmailActivityFilter filter = new SentCampaignEmailActivityFilter
            {
                Start = DateTime.Now.AddDays(-14),
                End = DateTime.Now
            };

            var results = await _sentEmailRepository.Query("tweet", filter);

            return Ok(results);
        }



        private SentCampaignEmailActivity _CreateRandomSentEmailActivity(List<String> userIds, List<String> groupIds, List<String> tagIds, Random rnd)
        {
            SentCampaignEmailActivity activity = new SentCampaignEmailActivity
            {
                Id = Guid.NewGuid().ToString(),
                ActionDate = DateTime.Now.ToUniversalTime(),
                Tenant = SeedConstants.Tenants[rnd.Next(SeedConstants.Tenants.Length)],
                ActionUser = new ActionUser
                {
                    FirstName = "doesnt",
                    LastName = "matter",
                    ImageRefUrl = "https://specials-images.forbesimg.com/imageserve/5c79a48231358e35dd27cb73/960x0.jpg?fit=scale"
                },
                ActionUserId = userIds[rnd.Next(userIds.Count)],
                CampaignId = Guid.NewGuid().ToString(),
                Session = 0,
                EmailSubject = "Subject Doesnt Matter but it should represent a good subject",
            };

            int upperBounds = rnd.Next(4);
            for(int i = 0; i < upperBounds; i++)
            {
                
                Console.WriteLine();
                activity.Recipients.Add(new CommunicationRecipient
                {
                    Type = SeedConstants.RecipientType[rnd.Next(SeedConstants.RecipientType.Length)],
                    RecipientId = userIds[rnd.Next(userIds.Count)]
                });
            }

            return activity;
        }
        
    }
}
