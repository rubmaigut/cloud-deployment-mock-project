using Azure.Storage.Blobs;

namespace AzureFullstackPractice.Services;

public class BlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task UploadFileAsync(string containerName, string filePath)
    {
        var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
        await blobContainer.CreateIfNotExistsAsync();
        var blobClient = blobContainer.GetBlobClient(Path.GetFileName(filePath));

        await blobClient.UploadAsync(filePath, true);
    }

}
