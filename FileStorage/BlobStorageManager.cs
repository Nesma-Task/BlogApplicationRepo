using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace FileStorage
{
    public class BlobStorageManager
    {
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=karimblobstorage;AccountKey=7wN9SAx1ncwHdhfRI0NDaKUqTsCwvf+R01S78HBkPFyqc8/3wNfEFf8tV6LF/z9XrkDP5aqYPNMfj6FZMVNQ3w==;EndpointSuffix=core.windows.net";
        public async Task CreateContainerAsync(string containerName)
        {

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            //Create a unique name for the container
            string finalContainerName = containerName + Guid.NewGuid().ToString();

            // Create the container and return a container client object
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(finalContainerName);
        }


        public async Task<string> UploadBlobImageAsync(Stream image)
        {

          
            string fileName = "image" + Guid.NewGuid().ToString() + ".jpg";
             


            // Open the file and upload its data
            Bitmap b = new Bitmap(image);

            using (MemoryStream ms =new MemoryStream())
            {
                b.Save(ms, ImageFormat.Jpeg);
                ms.Position = 0;
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("createcontainerinruntimee82b6d20-66bf-4a0c-ad47-cdc2d402e358");

                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                BlobUploadOptions buo = new BlobUploadOptions();

                buo.HttpHeaders = new BlobHttpHeaders();
                buo.HttpHeaders.ContentType = "Image/Jpeg";
               await blobClient.UploadAsync(ms,buo);
            }
            
            return GetBlobItem(fileName);
        }

        public async Task UploadBlobAsync()
        {

            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("createcontainerinruntimee82b6d20-66bf-4a0c-ad47-cdc2d402e358");

            string fileName = "image" + Guid.NewGuid().ToString() + ".jpg";

            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);


            // Open the file and upload its data
            Bitmap b = new Bitmap(File.OpenRead(@"D:\bus.jpg"));
             
            using FileStream uploadFileStream = File.OpenRead(@"D:\bus.jpg");
          
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {

                Console.WriteLine("\t" + blobItem.Name);
            }
        }

        public string GetBlobItem(string imageName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("createcontainerinruntimee82b6d20-66bf-4a0c-ad47-cdc2d402e358");

            BlobClient blobClient = containerClient.GetBlobClient(imageName/*"image379d9ae5-8f1d-4406-874f-2eae1b8f5459.jpg"*/);
            return blobClient.Uri.AbsoluteUri;
        }

    }
}
