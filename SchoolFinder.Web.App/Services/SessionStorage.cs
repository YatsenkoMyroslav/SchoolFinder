using Blazored.SessionStorage;

namespace SchoolFinder.Web.App.Services
{
    public class SessionStorage
    {
        private readonly ISessionStorageService _storage;

        public SessionStorage(ISessionStorageService storage)
        {
            _storage = storage;
        }

        public async Task Clear()
        {
            await _storage.ClearAsync();
        }

        public async Task<T?> Get<T>(string key) where T : class 
        {
            if(await _storage.ContainKeyAsync(key))
            {
                return await _storage.GetItemAsync<T>(key);
            }
            
            return null;
        }

        public async Task Set(string key, object value)
        {
            await _storage.SetItemAsync(key, value);
        }
    }
}
