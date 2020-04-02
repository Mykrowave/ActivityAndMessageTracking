using Activity.common.Communication.Repositories;
using Activity.common.DomainModels;
using Activity.common.DomainModels.Communication;
using Activity.common.Repositories.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivityAndMessageTracking.web.SeedHelpers
{
    public class ActivityGenerator
    {

        public static async Task GenerateActivities(
            ISentEmailActivityRepository sentEmailRepository, 
            IDownloadedFromCommunicationActivityRepository downloadedRepository)
        {
            List<String> userIds = SeedConstants.GenerateSetOfRandomStringGuidIds(10);
            List<String> groupIds = SeedConstants.GenerateSetOfRandomStringGuidIds(3);
            List<String> tagIds = SeedConstants.GenerateSetOfRandomStringGuidIds(5);
            Random rnd = new Random();




            var generatedDownloadedActivities = new List<DownloadedFromCommunicationActivity>();
            for (int i = 0; i < 5000; i++)
            {
                generatedDownloadedActivities.Add(
                    ActivityGenerator.CreateRandomDownloadedFromCommunicationActivity(userIds, groupIds, tagIds, rnd));
            }
            try
            {
                await downloadedRepository.AddRangeAsync(generatedDownloadedActivities);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                //TODO : Handle
            }


            var generatedSentEmailActivities = new List<SentCampaignEmailActivity>();
            for (int i = 0; i < 5000; i++)
            {
                generatedSentEmailActivities.Add(
                    ActivityGenerator.CreateRandomSentEmailActivity(userIds, groupIds, tagIds, rnd));
            }
            try
            {
                await sentEmailRepository.AddRangeAsync(generatedSentEmailActivities);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //TODO : Handle
            }
        }
        public static SentCampaignEmailActivity CreateRandomSentEmailActivity(List<String> userIds, List<String> groupIds, List<String> tagIds, Random rnd)
        {
            SentCampaignEmailActivity activity = new SentCampaignEmailActivity
            {
                Id = Guid.NewGuid().ToString(),
                ActionDate = DateTime.Now
                    .AddHours(rnd.Next(-3000, 3000))
                    .ToUniversalTime(),
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
            for (int i = 0; i < upperBounds; i++)
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
        public static DownloadedFromCommunicationActivity CreateRandomDownloadedFromCommunicationActivity(List<String> userIds, List<String> groupIds, List<String> tagIds, Random rnd)
        {
            DownloadedFromCommunicationActivity activity = new DownloadedFromCommunicationActivity
            {
                Id = Guid.NewGuid().ToString(),
                ActionDate = DateTime.Now
                    .AddHours(rnd.Next(-3000, 3000))
                    .ToUniversalTime(),
                Tenant = SeedConstants.Tenants[rnd.Next(SeedConstants.Tenants.Length)],
                ActionUser = new ActionUser
                {
                    FirstName = "doesnt",
                    LastName = "matter",
                    ImageRefUrl = "https://specials-images.forbesimg.com/imageserve/5c79a48231358e35dd27cb73/960x0.jpg?fit=scale"
                },
                ActionUserId = userIds[rnd.Next(userIds.Count)],
                FileId = Guid.NewGuid().ToString(),
                FileType = SeedConstants.FileType[rnd.Next(SeedConstants.FileType.Length)],
                FileText = "bla blah",
                MessageText = "This is the Message Text",
                MessageCampaignId = Guid.NewGuid().ToString(),
                MessageSession = 0,
                MessageSourceType = SeedConstants.MessageSource[rnd.Next(SeedConstants.MessageSource.Length)]
            };

            

            return activity;
        }

    }
}
