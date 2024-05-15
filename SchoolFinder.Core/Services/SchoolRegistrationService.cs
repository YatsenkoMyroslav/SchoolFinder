using Microsoft.AspNetCore.Identity;
using SchoolFinder.Common;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Request;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class SchoolRegistrationService
    {
        private readonly BlobStorageService _blobStorageService;
        private readonly SchoolRegistrationStore _store;
        private readonly UserManager<User> _userManager;

        public SchoolRegistrationService(SchoolRegistrationStore store, BlobStorageService blobStorageService, UserManager<User> userManager)
        {
            _store = store;
            _blobStorageService = blobStorageService;
            _userManager = userManager;
        }

        public async Task<int> Create(SchoolCreationRequestDto requestDto)
        {
            foreach (FileBytes file in requestDto.Photos ?? Enumerable.Empty<FileBytes>())
            {
                FileBytes uploaded = await _blobStorageService.Upload(file);
                file.Url = uploaded.Url;
            }

            SchoolCreationRequest request = requestDto.ToModel();

            User Owner = await _userManager.FindByIdAsync(request.Owner.Id);

            request.Owner = Owner;

            return await _store.Create(request);
        }

        public async Task<PagedList<SchoolCreationRequestDto>> Find(SchoolCreationRequestFilter filter)
        {
            IReadOnlyCollection<SchoolCreationRequest> requests = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<SchoolCreationRequestDto>(requests.Select(r => r.ToDto()), totalCount);
        }

        public Task<int> Update(SchoolCreationRequestDto requestDto)
        {
            SchoolCreationRequest request = requestDto.ToModel();
            request.Owner = null!;
            request.Photos = null!;
            request.Location = null!;
            return _store.Update(request);
        }
    }
}
