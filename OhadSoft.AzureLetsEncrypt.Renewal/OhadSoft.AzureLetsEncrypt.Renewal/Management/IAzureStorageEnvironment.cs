using System;
using LetsEncrypt.Azure.Core.Models;

namespace OhadSoft.AzureLetsEncrypt.Renewal.Management
{
    public interface IAzureStorageEnvironment : IAzureEnvironment
    {
        string StorageConnectionString { get; }
        string ContainerName { get; }
    }

    public class AzureStorageEnvironment : AzureEnvironment, IAzureStorageEnvironment
    {
        public AzureStorageEnvironment(string tenant, Guid subscription, Guid clientId, string clientSecret, string resourceGroup, string storageConnectionString, string containerName)
            : base(tenant, subscription, clientId, clientSecret, resourceGroup)
        {
            this.StorageConnectionString = storageConnectionString;
            this.ContainerName = containerName;
        }

        public string StorageConnectionString { get; set; }
        public string ContainerName { get; set; }
    }
}
