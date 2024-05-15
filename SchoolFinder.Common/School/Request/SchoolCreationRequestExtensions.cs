using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Request
{
    public static class SchoolCreationRequestExtensions
    {
        public static IQueryable<SchoolCreationRequest> FilterBy(this IQueryable<SchoolCreationRequest> requests, SchoolCreationRequestFilter filter)
        {
            return requests
                .Where(f => filter.SearchText == null
                        || f.Name.Contains(filter.SearchText)
                        || f.ShortDescription.Contains(filter.SearchText)
                        || f.LongDescription.Contains(filter.SearchText));
        }

        public static IQueryable<SchoolCreationRequest> SortBy(this IQueryable<SchoolCreationRequest> requests, SchoolCreationRequestFilter filter)
        {
            switch (filter.SortBy)
            {
                case SchoolFieldIdentifier.SchoolName:
                    return requests.OrderBy(f => f.Name, filter.OrderBy);
                default:
                    return requests;
            }
        }

        public static SchoolCreationRequestDto ToDto(this SchoolCreationRequest request)
        {
            SchoolCreationRequestDto dto = new SchoolCreationRequestDto()
            {
                Id = request.Id,
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                LongDescription = request.LongDescription,
                SchoolWebsiteUrl = request.SchoolWebsiteUrl,
                SchoolPhoneNumber = request.SchoolPhoneNumber,
                Photos = request.Photos,
                Location = request.Location,
                Owner = request.Owner.ToDto(),
                CreatedOn = request.CreatedOn,
            };

            return dto;
        }

        public static SchoolCreationRequest ToModel(this SchoolCreationRequestDto dto)
        {
            SchoolCreationRequest school = new SchoolCreationRequest()
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
                State = dto.State,
            };

            return school;
        }

        public static Model.School ToSchoolModel(this SchoolCreationRequest request)
        {
            Model.School school = new Model.School()
            {
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                LongDescription = request.LongDescription,
                SchoolWebsiteUrl = request.SchoolWebsiteUrl,
                SchoolPhoneNumber = request.SchoolPhoneNumber,
                Owner = request.Owner,
                Location = request.Location,
                Photos = request.Photos
            };

            return school;
        }

        public static SchoolDto ToSchoolDtoModel(this SchoolCreationRequestDto request)
        {
            SchoolDto schoolDto = new SchoolDto()
            {
                Name = request.Name,
                ShortDescription = request.ShortDescription,
                LongDescription = request.LongDescription,
                SchoolWebsiteUrl = request.SchoolWebsiteUrl,
                SchoolPhoneNumber = request.SchoolPhoneNumber,
                Owner = request.Owner,
                Location = request.Location,
                Photos = request.Photos
            };

            return schoolDto;
        }
    }
}
