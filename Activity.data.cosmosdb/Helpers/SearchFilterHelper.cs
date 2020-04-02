using Activity.common.DomainModels;
using Activity.common.SearchFilters;
using Activity.data.cosmosdb.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Activity.data.cosmosdb.Helpers
{
    public class SearchFilterHelper
    {

        public static CosmosSQLQuery CreateBaseSqlQuery(String tenant, String collectionName, BaseSearchFilter filter)
        {
            CosmosSQLQuery query = new CosmosSQLQuery();
            query.Parameters.Add("@startDate", filter.Start.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
            query.Parameters.Add("@endDate", filter.End.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'"));
            query.Parameters.Add("@activityType", filter.ActivityTypeName);
            query.Parameters.Add("@tenant", tenant);

            query.Query = $"SELECT * FROM {collectionName} as t ";
            // Required Fields
            query.Query += $" WHERE t.{nameof(BaseActivity.ActivityType).ToCamelCase()} <= @activityType ";
            query.Query += $" AND t.{nameof(BaseActivity.ActionDate).ToCamelCase()} >= @startDate ";
            query.Query += $" AND t.{nameof(BaseActivity.ActionDate).ToCamelCase()} <= @endDate ";
            query.Query += $" AND t.{nameof(BaseActivity.Tenant).ToCamelCase()} = @tenant ";

            return query;
        }
    }
}
