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
        var region = Environment.GetEnvironmentVariable("AWS_REGION") ?? throw new Exception("AWS_REGION not set");
        var bucketName = $"lambda-perf-{region}";
        var bucketKey = $"test/{context.FunctionName}/test.txt";
        
        var s3 = new AmazonS3Client();
        
        for (var i = 0; i < 250; i++)
        {
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = bucketKey,
                ContentType = "text/plain",
                ContentBody = i.ToString()
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
