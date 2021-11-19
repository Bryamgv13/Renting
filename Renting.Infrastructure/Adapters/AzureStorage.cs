using Azure.Storage.Blobs;
using Renting.Application.Ports;
using System.IO;
using System.Threading.Tasks;

namespace Renting.Infrastructure.Adapters
{
    public class AzureStorage : IAlmacenamiento
    {
        private readonly BlobContainerClient BlobContainerClient;

        public AzureStorage(BlobContainerClient blobContainerClient)
        {
            BlobContainerClient = blobContainerClient;
        }        

        public async Task SubirArchivoBlobAsync(string nombreArchivo, Stream contenido)
        {
            await BlobContainerClient.UploadBlobAsync(nombreArchivo, contenido);
        }
    }
}
