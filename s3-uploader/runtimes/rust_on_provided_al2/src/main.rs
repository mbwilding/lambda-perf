use aws_smithy_http::byte_stream::ByteStream;
use bytes::Bytes;
use lambda_runtime::{service_fn, Error, LambdaEvent};
use rand::Rng;
use serde_json::Value;

#[tokio::main]
async fn main() -> Result<(), Error> {
    let func = service_fn(func);
    lambda_runtime::run(func).await?;
    Ok(())
}

async fn func(event: LambdaEvent<Value>) -> Result<(), Error> {
    let region = std::env::var("AWS_REGION")?;
    let bucket_name = format!("lambda-perf-{}", region);
    let bucket_key = format!("test/{}", event.context.env_config.function_name);

    let aws_config = aws_config::load_from_env().await;
    let s3 = aws_sdk_s3::Client::new(&aws_config);

    for _ in 0..1000 {
        let random_number = rand::thread_rng().gen_range(10_000_000..100_000_000);
        let random_string = random_number.to_string();
        let random_bytes = Bytes::from(random_string);
        let body = ByteStream::from(random_bytes);

        let _ = s3
            .put_object()
            .bucket(&bucket_name)
            .key(&bucket_key)
            .content_type("text/plain")
            .body(body)
            .send()
            .await;
    }

    s3.delete_object()
        .bucket(&bucket_name)
        .key(&bucket_key)
        .send()
        .await?;

    Ok(())
}
