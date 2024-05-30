using SchoolFinder.Common.School.Model;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace SchoolFinder.DAL.QueryBuilders
{
    internal static class SchoolQueryBuilder
    {
        internal static string Build(SchoolFilter filter)
        {
            string query = 
                $"SELECT [sch].[Id], [sch].[CreatedOn], [sch].[Deleted], [sch].[DeletedOn], [sch].[LocationId], [sch].[LongDescription], [sch].[Name], [sch].[OwnerId], [sch].[SchoolPhoneNumber], [sch].[SchoolWebsiteUrl], [sch].[ShortDescription] " +
                $"FROM ";

            string? previousIdentifier = null;

            if(filter.MinTotalRating != null || filter.MaxTotalRating != null || filter.RatingCategoryFilters.Any())
            {
                if(filter.MinTotalRating != null || filter.MaxTotalRating != null)
                {
                    previousIdentifier = "[sch]";
                    query += GetRatingFilteringComponent(previousIdentifier, filter.MinTotalRating, filter.MaxTotalRating);
                }

                for(int i = 0; i < filter.RatingCategoryFilters.Count; i++)
                {
                    if(previousIdentifier != null)
                    {
                        query += " INNER JOIN ";
                        string identifier = $"[sch{i}]";
                        query += GetRatingFilteringComponent(identifier, filter.RatingCategoryFilters.ElementAt(i).MinValue, filter.RatingCategoryFilters.ElementAt(i).MaxValue, (int)filter.RatingCategoryFilters.ElementAt(i).Category);
                        query += $" ON {previousIdentifier}.Id = {identifier}.Id ";
                        previousIdentifier = identifier;
                    }
                    else
                    {
                        previousIdentifier = $"[sch]";
                        query += GetRatingFilteringComponent(previousIdentifier, filter.RatingCategoryFilters.ElementAt(i).MinValue, filter.RatingCategoryFilters.ElementAt(i).MaxValue, (int)filter.RatingCategoryFilters.ElementAt(i).Category);
                    }
                }
            }
            else
            {
                query += "Schools [sch]";
            }

            return query;
        }

        private static string GetRatingFilteringComponent(string compIdentifier, double? min = null, double? max = null, int? category = null)
        {
            string whereLine = category != null ? $"where [r].Category = {category} " : "";
            string minRule = min != null ? $"AVG(Cast([r].Value as Float)) > {min!.Value.ToString(CultureInfo.InvariantCulture)}" : "";
            string maxRule = max != null ? $"AVG(Cast([r].Value as Float)) < {max!.Value.ToString(CultureInfo.InvariantCulture)}" : "";
            string and = min != null && max != null ? "and" : "";

            string component = 
                $"(SELECT [sch].[Id], [sch].[CreatedOn], [sch].[Deleted], [sch].[DeletedOn], [sch].[LocationId], [sch].[LongDescription], [sch].[Name], [sch].[OwnerId], [sch].[SchoolPhoneNumber], [sch].[SchoolWebsiteUrl], [sch].[ShortDescription] " +
                $"FROM Schools sch " +
                $"LEFT JOIN Comments [c] " +
                $"ON [c].SchoolId = sch.Id " +
                $"LEFT JOIN Ratings [r] " +
                $"ON [r].CommentId = [c].Id " +
                $"{whereLine} " +
                $"GROUP BY [sch].[Id], [sch].[CreatedOn], [sch].[Deleted], [sch].[DeletedOn], [sch].[LocationId], [sch].[LongDescription], [sch].[Name], [sch].[OwnerId], [sch].[SchoolPhoneNumber], [sch].[SchoolWebsiteUrl], [sch].[ShortDescription] " +
                $"HAVING {minRule} {and} {maxRule}) {compIdentifier}";

            return component; 
        }
    }
}
