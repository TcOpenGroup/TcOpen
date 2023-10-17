using System;

using MongoDB.Bson;
using MongoDB.Driver;

using TcoCore;

using TcOpen.Inxton.TcoCore.Blazor.TcoDiagnosticsAlternative.Mapping;

public class FilterHelper
{
    public static FilterDefinition<MongoDbLogItem> BuildDateFilter(DateTime? startDate, DateTime? endDate)
    {
        var filterBuilder = Builders<MongoDbLogItem>.Filter;
        var filter = filterBuilder.Empty;

        if (startDate.HasValue)
        {
            filter &= filterBuilder.Gte(item => item.UtcTimeStamp, startDate.Value);
        }
        if (endDate.HasValue)
        {
            filter &= filterBuilder.Lte(item => item.UtcTimeStamp, endDate.Value);
        }

        return filter;
    }

    public static FilterDefinition<MongoDbLogItem> BuildKeywordFilter(string keyword)
    {
        var filterBuilder = Builders<MongoDbLogItem>.Filter;

        return !string.IsNullOrEmpty(keyword)
            ? filterBuilder.Regex(item => item.RenderedMessage, new BsonRegularExpression(keyword, "i"))
            : filterBuilder.Empty;
    }

    public static FilterDefinition<MongoDbLogItem> BuildCategoryFilter(eMessageCategory? category)
    {
        var filterBuilder = Builders<MongoDbLogItem>.Filter;

        if (category.HasValue)
        {
            var levelsToInclude = MessageCategoryMapper.GetAllLevelsGreaterThanOrEqualTo(category.Value);
            return filterBuilder.In(item => item.Level, levelsToInclude);
        }

        return filterBuilder.Empty;
    }

    public static FilterDefinition<MongoDbLogItem> BuildTimeStampAcknowledgedFilter()
    {
        return Builders<MongoDbLogItem>.Filter.Eq(item => item.TimeStampAcknowledged, null);
    }

    public static FilterDefinition<MongoDbLogItem> BuildTimeStampAcknowledgedForArchiveFilter()
    {
        var filterBuilder = Builders<MongoDbLogItem>.Filter;
        return filterBuilder.Ne(item => item.TimeStampAcknowledged, null);
    }
}
