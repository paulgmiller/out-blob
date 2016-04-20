using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace out_blob
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("outblobaccount"));
            var container = account.CreateCloudBlobClient().GetContainerReference("outblob");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
            var blob = container.GetAppendBlobReference(Guid.NewGuid().ToString());
            Console.WriteLine(blob.Uri);
            if (!blob.Exists()) blob.CreateOrReplace();
            
            blob.AppendFromStream(Console.OpenStandardInput());
        }
    }
}
