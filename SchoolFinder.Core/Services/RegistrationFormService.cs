using SchoolFinder.Common.Abstraction.Pagination;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.DAL.Stores;

namespace SchoolFinder.Core.Services
{
    public class RegistrationFormService
    {
        private BlobStorageService _blobStorageService { get; set; }
        private RegistrationFormStore _store { get; set; } 

        public RegistrationFormService(BlobStorageService blobStorageService, RegistrationFormStore store) {
            _blobStorageService = blobStorageService;
            _store = store;
        }

        public async Task<int> Create(RegistrationForm form)
        {
            form.DocumentApprove = await _blobStorageService.Upload(form.DocumentApprove);
            return await _store.Create(form);
        }

        public async Task<PagedList<RegistrationForm>> Find(RegistrationFormFilter filter)
        {
            IReadOnlyCollection<RegistrationForm> forms = await _store.Find(filter);
            int totalCount = await _store.GetTotalCount(filter);

            return new PagedList<RegistrationForm>(forms, totalCount);
        }

        public Task<int> Update(RegistrationForm form)
        {
            form.DocumentApprove = null!;
            return _store.Update(form);
        }
    }
}
