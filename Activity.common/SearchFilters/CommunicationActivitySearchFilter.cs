using Activity.common.DomainModels.Communication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity.common.SearchFilters
{
    public class CommunicationActivitySearchFilter
    {
        /// <summary>
        /// Filter designed to isolate responses to a specific User
        /// </summary>
        public String ActionUserId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        /// <summary>
        /// Creates the Ability filter based on Activity Type
        /// </summary>
        public List<String> ActivityTypes { get; set; } = new List<String>();
        /// <summary>
        /// Search the recipients property to find specific recipient types based on filled out information (matching)
        /// Remember to be smart with implementation, not all
        /// </summary>
        public List<CommunicationRecipient> Recipients { get; set; } = new List<CommunicationRecipient>();
    }

}
