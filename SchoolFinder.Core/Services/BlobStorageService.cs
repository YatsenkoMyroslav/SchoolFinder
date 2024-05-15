using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using SchoolFinder.Common;

namespace SchoolFinder.Core.Services
{
    public class BlobStorageService
    {
        private const string ContainerName = "photos";
        public const string SuccessMessageKey = "SuccessMessage";
        public const string ErrorMessageKey = "ErrorMessage";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _containerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            _containerClient.CreateIfNotExists();
        }

        public async Task<FileBytes> Upload(FileBytes file)
        {
            try
            {
                if (file.Data == null)
                {
                    throw new ArgumentNullException(nameof(file));
                }

                DateTimeOffset now = DateTimeOffset.UtcNow;
                long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();

                BlobClient blobClient = _containerClient.GetBlobClient(unixTimeMilliseconds.ToString());
                BlobHttpHeaders blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpeg" };
                await blobClient.UploadAsync(new MemoryStream(file.Data), new BlobUploadOptions { HttpHeaders = blobHttpHeader });
                file.Url = blobClient.Uri.AbsoluteUri;
                return file;
            }
            catch (Exception ex)
            {
                return file;
            }
        }
    }
}
