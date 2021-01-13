namespace Owl.Storage.Models
{
    public class  AmazonS3StorageOptions
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public string Region { get; set; }

        public string Bucket { get; set; }
    }
}