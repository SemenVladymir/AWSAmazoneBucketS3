using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Amazon.S3.Model;
using AWSAmazoneBucket;
using System;

AmazonS3.GetClient();
        string inp, fileName, bucketName;
        do
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?\n\n");
            Console.WriteLine("1. Read file in the Azure blob");
            Console.WriteLine("2. Add/Upload file into the Azure blob");
            Console.WriteLine("3. Show all buckets consist the Amazon S3");
            Console.WriteLine("4. Delete file from the bucket in the Amazon S3\n");
            Console.WriteLine("0. Exit from program\n");
            inp = Console.ReadLine();
            if (int.TryParse(inp, out int res))
            {
                switch (res)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter the name of the bucket where the file is exist\n");
                        bucketName = Console.ReadLine();
                        Console.Write("Enter the name of file which you want to read\n");
                        fileName = Console.ReadLine();
                        AmazonS3.ReadFileFromBucket(bucketName, fileName);
                        Thread.Sleep(3000);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Enter the path to the file which you want to upload\n");
                        string path = Console.ReadLine();
                        Console.Write("Enter the name of file which you want to upload\n");
                        fileName = Console.ReadLine();
                        AmazonS3.UploadFileToBucket(path, fileName);
                        Thread.Sleep(3000);
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Enter the path to the file which you want to upload\n");
                        await AmazonS3.ShowAllBuckets();
                        Thread.Sleep(3000);
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Enter the name of the bucket where the file is exist\n");
                        bucketName = Console.ReadLine();
                        Console.Write("Enter the name of file which you want to delete\n");
                        fileName = Console.ReadLine();
                        await AmazonS3.DeleteFile(bucketName, fileName);
                        Thread.Sleep(3000);
                        break;
                    case 0:
                        return;
                }
            }
        } while (true);
