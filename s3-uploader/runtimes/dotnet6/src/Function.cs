using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaPerf;

// ReSharper disable once UnusedType.Global
public class Function
{
    // ReSharper disable once UnusedMember.Global
    public async Task Handler(ILambdaContext context)
    {
        var bucketName = Environment.GetEnvironmentVariable("BUCKET_NAME") ?? throw new Exception("BUCKET_NAME not set");
        var bucketKey = $"test/{context.FunctionName}";
        
        var s3 = new AmazonS3Client();
        
        for (var i = 0; i < 1000; i++)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = bucketKey,
                ContentBody = Random.Shared.Next(10000000, 100000000).ToString()
            };

            await s3.PutObjectAsync(request);
        }

        await s3.DeleteObjectAsync(new DeleteObjectRequest
        {
            BucketName = bucketName,
            Key = bucketKey
        });
    }
}
