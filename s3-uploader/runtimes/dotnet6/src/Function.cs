using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaPerf;

public class Function
{
    public Function()
    {
        
    }
    
    public object Handler(ILambdaContext context)
    {
        return new { statusCode = 200 };
    }
}