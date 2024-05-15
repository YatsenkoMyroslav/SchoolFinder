using Microsoft.AspNetCore.Identity;
using Nest;
using SchoolFinder.Common;
using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.User;
using SchoolFinder.Common.School.Model;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class SchoolService
    {
        private readonly SchoolStore _store;
        private readonly UserManager<User> _userManager;
        private readonly BlobStorageService _blobStorageService;
        private readonly ElasticClient _elasticClient;

        public SchoolService(ElasticClient elasticClient, SchoolStore schoolStore, BlobStorageService blobStorageService, UserManager<User> userManager)
        {
            _elasticClient = elasticClient;
            _store = schoolStore;
            _blobStorageService = blobStorageService;
            _userManager = userManager;
        }

        public async Task<int> Create(SchoolDto schoolDto)
        {
            School school = schoolDto.ToModel();

            foreach(FileBytes file in school.Photos ?? Enumerable.Empty<FileBytes>())
            {
                file.Id = Guid.NewGuid();
                FileBytes uploaded = await _blobStorageService.Upload(file);
                file.Url = uploaded.Url;
            }

            User owner = await _userManager.FindByIdAsync(school.Owner.Id);
            school.Owner = owner;

            return await _store.Create(school);
        }

        public async Task<int> Delete(Guid schoolId)
        {
            School? school = await _store.Get(schoolId);
            if(school != null) {
                school.Deleted = true;
                school.DeletedOn = DateTime.UtcNow;
                int result = await _store.Update(school);
                return result;
            }

            return 0;
        }

        public async Task<PagedList<SchoolDto>> Find(SchoolFilter filter)
        {
            IReadOnlyCollection<School> schools = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<SchoolDto>(schools.Select(s => s.ToDto()), totalCount);
        }

        public Task<int> Update(SchoolDto schoolDto)
        {
            School school = schoolDto.ToModel();
            school.Owner = null!;
            school.Comments = null!;
            return _store.Update(school);
        }
    }
}
