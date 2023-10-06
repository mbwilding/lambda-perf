exports.handler = async (event, context) => {
    const aws = require('aws-sdk');
    const s3 = new aws.S3();
    
    const region = process.env.AWS_REGION;
    if (!region) {
        throw new Error("AWS_REGION not set");
    }

    const bucket_name = `lambda-perf-${region}`;
    const bucket_key = `test/${context.functionName}`;

    try {
        for (let i = 0; i < 250; i++) {
            const params = {
                Bucket: bucket_name,
                Key: bucket_key,
                ContentType: 'text/plain',
                Body: i.toString()
            };
            await s3.putObject(params).promise();
        }

        await s3.deleteObject({ Bucket: bucket_name, Key: bucket_key }).promise();
    } catch (e) {
        throw new Error(`An error occurred: ${e.message}`);
    }
};
