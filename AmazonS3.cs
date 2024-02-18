using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace AWSAmazoneBucket
{
    internal class AmazonS3
    {
        private string accessKey = "AKIA6ODUZCHYMHVJ45PO";
        private string secretKey = "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";
        private static string bucketName = "semenvladbucket";
        private static AmazonS3 _instance;
        private static AmazonS3Client s3Client;

        private AmazonS3()
        {
            BasicAWSCredentials credentials = new BasicAWSCredentials(accessKey, secretKey);
            s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.EUNorth1);
        }

        public static AmazonS3 GetClient()
        {
            if (_instance == null)
            {
                _instance = new AmazonS3();
            }
            return _instance;
        }

        public static void UploadFileToBucket(string filePath, string fileName) 
        {
            try
            {
                // Upload the file to Amazon S3
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);
                fileTransferUtility.Upload(filePath, bucketName, fileName);
                Console.WriteLine("Upload completed!");

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public static async Task ReadFileFromBucket(string bucketName, string fileName)
        {
            try
            {
                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };
                using (var getResponse = await s3Client.GetObjectAsync(getRequest))
                using (var sr = new StreamReader(getResponse.ResponseStream))
                {
                    var getResponseString = await sr.ReadToEndAsync();
                    Console.WriteLine(getResponseString);
                }

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public static async Task ShowAllBuckets()
        {
            try
            {
                var list = await s3Client.ListBucketsAsync();
            if (list.ContentLength > 0)
            {

                foreach (var bucket in list.Buckets)
                {
                    Console.WriteLine($"Bucket-{bucket.BucketName}\n");
                    var filelist = s3Client.ListObjectsAsync(new ListObjectsRequest() { BucketName = bucket.BucketName });

                    foreach (var file in filelist.Result.S3Objects)
                    {
                        Console.WriteLine($"File---{file.Key}\n");
                    }
                }
            }
            else
                Console.WriteLine("The bucket is empty!");
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public static async Task DeleteFile(string bucketName, string fileName)
        {
            try 
            {
                DeleteObjectRequest deleteRequest = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = fileName
                };
                await s3Client.DeleteObjectAsync(deleteRequest);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

    }
}
