using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.Common.School.Model.Feedback;
using System.Net.Http.Headers;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public static class CommentCreationRequestExtensions
    {
        public static CommentDto ToCommentDtoModel(this CommentCreationRequestDto request)
        {
            CommentDto dto = new CommentDto()
            {
                Text = request.Text,
                Ratings = new List<RatingDto>(),
                School = new SchoolDto { Id = request.SchoolId },
                CreatedBy = new UserDto { Id = request.CreatedById },
                CreatedOn = request.CreatedOn,
            };

            foreach (var rating in request.Ratings ?? Enumerable.Empty<RatingCreationRequestDto>())
            {
                dto.Ratings.Add(rating.ToRatingDtoModel());
            }

            return dto;
        }

        public static CommentCreationRequestDto ToDto(this CommentCreationRequest request)
        {
            CommentCreationRequestDto dto = new CommentCreationRequestDto()
            {
                Id = request.Id,
                Text = request.Text,
                Ratings = new List<RatingCreationRequestDto>(),
                SchoolId = request.SchoolId,
                School = request.School.ToDto(),
                CreatedBy = request.CreatedBy.ToDto(),
                CreatedById = request.CreatedById,
                CreatedOn = request.CreatedOn,
                RequestState = request.RequestState,
            };

            foreach(var rating in request.Ratings ?? Enumerable.Empty<RatingCreationRequest>())
            {
                dto.Ratings.Add(rating.ToDto());
            }

            return dto;
        }

        public static CommentCreationRequest ToModel(this CommentCreationRequestDto dto)
        {
            CommentCreationRequest model = new CommentCreationRequest()
            {
                Id = dto.Id,
                Text = dto.Text,
                Ratings = new List<RatingCreationRequest>(),
                SchoolId = dto.SchoolId,
                School = dto.School?.ToModel() ?? new Model.School { Id = dto.SchoolId },
                CreatedBy = dto.CreatedBy.ToModel(),
                CreatedById = dto.CreatedById,
                CreatedOn = dto.CreatedOn,
                RequestState = dto.RequestState,
            };

            foreach (var rating in dto.Ratings ?? Enumerable.Empty<RatingCreationRequestDto>())
            {
                model.Ratings.Add(rating.ToModel());
            }

            return model;
        }
    }
}
