using SchoolFinder.Common.School.Model.Feedback;

namespace SchoolFinder.Common.School.Request.Feedback
{
    public static class RatingCreationRequestExtensions
    {
        public static RatingDto ToRatingDtoModel(this RatingCreationRequestDto request)
        {
            return new RatingDto()
            {
                Value = request.Value,
                Category = request.Category
            };
        }

        public static RatingCreationRequestDto ToDto(this RatingCreationRequest request)
        {
            return new RatingCreationRequestDto()
            {
                Id = request.Id,
                Value = request.Value,
                Category = request.Category,
                CommentRequestId = request.CommentRequestId,
            };
        }

        public static RatingCreationRequest ToModel(this RatingCreationRequestDto dto)
        {
            return new RatingCreationRequest()
            {
                Id = dto.Id,
                Value = dto.Value,
                Category = dto.Category,
                CommentRequestId = dto.CommentRequestId,
            };
        }
    }
}
