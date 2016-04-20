using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Management.Automation;  // Windows PowerShell assembly.


namespace out_blob
{
    class Program
    {
        static void Main(string[] args)
        {
            
        
        }
    }
    
   
namespace OutBlob
{
  // Declare the class as a cmdlet and specify and 
  // appropriate verb and noun for the cmdlet name.
  [Cmdlet(VerbsCommunications.Out, "Blob")]
  public class OutBlobCommand : Cmdlet
  {
    // Declare the parameters for the cmdlet.
     [Parameter(
        Position = 0,
        ValueFromPipeline = true,
        ValueFromPipelineByPropertyName = true
    )]
    [ValidateNotNullOrEmpty]
    public Object[] Inputs
    {
      get;
      set;
    }
    private CloudAppendBlob blob;
    
    public override BeginProcessing()
    {
        var account = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("outblobaccount"));
        var container = account.CreateCloudBlobClient().GetContainerReference("outblob");
        container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
        blob = container.GetAppendBlobReference(Guid.NewGuid().ToString());
        if (!blob.Exists()) blob.CreateOrReplace();            
        Console.WriteLine(blob.Uri);
    }

    // Overide the ProcessRecord method to process
    // the supplied user name and write out a 
    // greeting to the user by calling the WriteObject
    // method.
    protected override  ProcessRecord()
    {
        Console.WriteLine($"input count {Inputs.Length}");
        foreach (var i in Inputs)
            blob.AppendFromText(i.ToString());
    }
  }
}
}
