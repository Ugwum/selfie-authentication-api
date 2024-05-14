namespace Selfie_Authentication.API.Model
{
    public class AWSSettings
    {
        public string AWSAccessKey { get; set; }
        public string AWSSecretKey { get; set; }
        public string AWSS3BucketName { get; set; }
        public string AWSRekognitionCollectionID { get; set; }
         
    }

    public class RequestResult
    {
        public string code { get; set; } = string.Empty;
        public string message { get; set; }
        public dynamic data { get; set; }

        public bool Succeeded { get; set; }
    }
}

