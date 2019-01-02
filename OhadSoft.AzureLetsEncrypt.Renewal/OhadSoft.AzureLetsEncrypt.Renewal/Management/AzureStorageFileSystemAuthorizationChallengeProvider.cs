using System.Text;
using System.Threading.Tasks;
using ACMESharp;
using ACMESharp.ACME;
using LetsEncrypt.Azure.Core.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace OhadSoft.AzureLetsEncrypt.Renewal.Management
{
    public class AzureStorageFileSystemAuthorizationChallengeProvider : BaseHttpAuthorizationChallengeProvider
    {
        private readonly IAzureStorageEnvironment m_storageEnvironment;

        public AzureStorageFileSystemAuthorizationChallengeProvider(IAzureStorageEnvironment storageEnvironment)
        {
            m_storageEnvironment = storageEnvironment;
        }

        private async Task<ICloudBlob> GetChallengeBlobReferenceAsync(HttpChallenge challenge)
        {
            var blobStorageAccount = CloudStorageAccount.Parse(m_storageEnvironment.StorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference(m_storageEnvironment.ContainerName);
            await blobContainer.CreateIfNotExistsAsync();

            var fileName = challenge.FilePath.Substring(AcmeProtocol.HTTP_CHALLENGE_PATHPREFIX.Length);

            return blobContainer.GetBlockBlobReference(fileName);
        }

        public override async Task PersistsChallengeFile(HttpChallenge challenge)
        {
            var blobFile = await GetChallengeBlobReferenceAsync(challenge);
            blobFile.Properties.ContentType = "text/plain";

            var challengeBytes = Encoding.UTF8.GetBytes(challenge.FileContent);

            await blobFile.UploadFromByteArrayAsync(challengeBytes, 0, challengeBytes.Length);
        }

        public override async Task CleanupChallengeFile(HttpChallenge challenge)
        {
            var blobFile = await GetChallengeBlobReferenceAsync(challenge);

            await blobFile.DeleteIfExistsAsync();
        }
    }
}
