using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model.Feedback;
using SchoolFinder.Common.School.Request;

namespace SchoolFinder.Common.School.Model
{
    public static class SchoolExtensions
    {
        public static IQueryable<School> FilterBy(this IQueryable<School> schools, SchoolFilter filter)
        {
            return schools
                .Where(f => !f.Deleted)
                .FilterByDistance(filter)
                .FilterBySearch(filter)
                .FilterByOwner(filter);
        }

        private static IQueryable<School> FilterByDistance(this IQueryable<School> schools, SchoolFilter filter)
        {
            return schools.Where(f => filter.MinRange == null || 
                (6371 * 2 * Math.Atan2(Math.Sqrt(Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude) * Math.Cos(f.Location.Latitude)), Math.Sqrt(1 - Math.Sin((f.Location.Latitude
                        - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude * (Math.PI / 180)) * Math.Cos(f.Location.Latitude * (Math.PI / 180)))) > filter.MinRange
                ) && ( filter.MaxRange == null ||
                (6371 * 2 * Math.Atan2(Math.Sqrt(Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude) * Math.Cos(f.Location.Latitude)), Math.Sqrt(1 - Math.Sin((f.Location.Latitude
                        - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude * (Math.PI / 180)) * Math.Cos(f.Location.Latitude * (Math.PI / 180))))
                < filter.MaxRange)));
        }

        private static IQueryable<School> FilterBySearch(this IQueryable<School> schools, SchoolFilter filter)
        {
            return schools.Where(f => filter.SearchText == null
                        || f.Name.Contains(filter.SearchText)
                        || f.ShortDescription.Contains(filter.SearchText)
                        || f.LongDescription.Contains(filter.SearchText));
        }

        private static IQueryable<School> FilterByOwner(this IQueryable<School> schools, SchoolFilter filter)
        {
            return schools.Where(f => filter.OwnerId == null
                        || f.Owner.Id == filter.OwnerId.ToString());
        }

        public static IQueryable<School> SortBy(this IQueryable<School> schools, SchoolFilter filter)
        {
            switch (filter.SortBy)
            {
                case SchoolFieldIdentifier.SchoolName:
                    return schools.OrderBy(f => f.Name, filter.OrderBy);
                case SchoolFieldIdentifier.Newest:
                    return schools.OrderBy(f => f.CreatedOn, filter.OrderBy);
                case SchoolFieldIdentifier.Range:
                    return schools.OrderBy(f =>
                        6371 * 2 * Math.Atan2(Math.Sqrt(Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude) * Math.Cos(f.Location.Latitude)), Math.Sqrt(1 - Math.Sin((f.Location.Latitude 
                        - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Latitude - filter.UserLocation.Latitude) * (Math.PI / 180) / 2) +
                        Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Sin((f.Location.Longitude - filter.UserLocation.Longitude) * (Math.PI / 180) / 2) * Math.Cos(filter.UserLocation.Latitude * (Math.PI / 180)) * Math.Cos(f.Location.Latitude * (Math.PI / 180)))), filter.OrderBy);
                case SchoolFieldIdentifier.Description:
                    return schools.OrderBy(f => f.ShortDescription, filter.OrderBy);
                default:
                    return schools;
            }
        }

        public static SchoolDto ToDto(this School school)
        {
            if(school is null)
            {
                return null!;
            }

            SchoolDto dto = new SchoolDto()
            {
                Id = school.Id,
                Name = school.Name,
                ShortDescription = school.ShortDescription,
                LongDescription = school.LongDescription,
                SchoolWebsiteUrl = school.SchoolWebsiteUrl,
                SchoolPhoneNumber = school.SchoolPhoneNumber,
                Photos = school.Photos,
                Location = school.Location,
                Owner = school.Owner.ToDto(),
                CreatedOn = school.CreatedOn,
                Deleted = school.Deleted,
                DeletedOn = school.DeletedOn,
                Comments = new List<CommentDto>()
            };

            return dto;
        }

        public static School ToModel(this SchoolDto dto)
        {
            School school = new School()
            {
                Id = dto.Id,
                Name = dto.Name,
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                SchoolWebsiteUrl = dto.SchoolWebsiteUrl,
                SchoolPhoneNumber = dto.SchoolPhoneNumber,
                Photos = dto.Photos,
                Location = dto.Location,
                Owner = dto.Owner.ToModel(),
                CreatedOn = dto.CreatedOn,
                Deleted = dto.Deleted,
                DeletedOn = dto.DeletedOn,
                Comments = new List<Comment>()
            };

            return school;
        }

        public static SchoolCreationRequestDto ToRequest(this SchoolDto school)
        {
            SchoolCreationRequestDto request = new SchoolCreationRequestDto()
            {
                Name = school.Name,
                ShortDescription = school.ShortDescription,
                LongDescription = school.LongDescription,
                SchoolWebsiteUrl = school.SchoolWebsiteUrl,
                SchoolPhoneNumber = school.SchoolPhoneNumber,
                Owner = school.Owner,
                Location = school.Location,
                Photos = school.Photos
            };

            return request;
        }
    }
}
