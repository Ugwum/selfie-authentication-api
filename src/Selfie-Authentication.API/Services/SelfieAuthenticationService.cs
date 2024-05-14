using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Selfie_Authentication.API.Model;
using static System.Net.Mime.MediaTypeNames;
using Image = Amazon.Rekognition.Model.Image;
using S3Object = Amazon.Rekognition.Model.S3Object;

namespace Selfie_Authentication.API.Services
{
    public class SelfieAuthenticationService : ISelfieAuthenticationService 
    {
        private readonly ILogger<SelfieAuthenticationService> _logger;
        private readonly AWSSettings _awsSettings;
        public SelfieAuthenticationService(ILogger<SelfieAuthenticationService> logger,IOptions<AWSSettings> awsSettings)
        {
            _logger = logger;
            _awsSettings = awsSettings.Value;
        }

        public async  Task<RequestResult> AuthenticateUserSelfie(string base64Image)
        {
            try
            {
                using (var rekognitionClient = new AmazonRekognitionClient(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey, RegionEndpoint.USWest2))
                using (var s3Client = new AmazonS3Client(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey, RegionEndpoint.USWest2))
                {
                    var imageBytes = Convert.FromBase64String(base64Image);
                    var imageStream = new MemoryStream(imageBytes);

                    var s3Object = new PutObjectRequest
                    {
                        BucketName = _awsSettings.AWSS3BucketName,
                        Key = Guid.NewGuid().ToString(), // Unique key for the image in S3
                        InputStream = imageStream
                    };

                    await s3Client.PutObjectAsync(s3Object);

                    var searchFaceRequest = new SearchFacesByImageRequest
                    {
                        CollectionId =_awsSettings.AWSRekognitionCollectionID,
                        Image = new Image { S3Object = new S3Object { Bucket = _awsSettings.AWSS3BucketName, Name = s3Object.Key } }
                    };

                    var searchFaceResponse = await rekognitionClient.SearchFacesByImageAsync(searchFaceRequest);

                    if (searchFaceResponse.FaceMatches.Count <= 0) throw new CustomException("SELFIE_VERIFICATION_FAILED", "Authentication failed. Selfie does not match any registered face.");

                    return new RequestResult
                    {
                        code = "SELFIE_MATCH",
                        data = searchFaceResponse.FaceMatches,
                        message = "Selfie Authentication successful!",
                        Succeeded = true

                    };                     
                }


            }
            catch (CustomException ex)
            {
                _logger.LogError($"An error occurred, {ex.Message}, {ex.StackTrace}");
                return new RequestResult { code = ex.code, data = null, message = ex.Message, Succeeded = false };
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred, {ex.Message}, {ex.StackTrace}");
                return new RequestResult { code = "UNEXPECTED_ERROR", data = null, message = "An unexpected error occurred.", Succeeded = false };
            }
        }

        public async Task<RequestResult> RegisterUserSelfie(string base64Image)
        {
            try
            {
                using (var rekognitionClient = new AmazonRekognitionClient(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey, RegionEndpoint.USWest2))
                using (var s3Client = new AmazonS3Client(_awsSettings.AWSAccessKey, _awsSettings.AWSSecretKey, RegionEndpoint.USWest2))
                {
                    var imageBytes = Convert.FromBase64String(base64Image);
                    var imageStream = new MemoryStream(imageBytes);

                    var s3Object = new PutObjectRequest
                    {
                        BucketName = _awsSettings.AWSS3BucketName,
                        Key = Guid.NewGuid().ToString(), // Unique key for the image in S3
                        InputStream = imageStream
                    };

                    await s3Client.PutObjectAsync(s3Object);

                    var indexFaceRequest = new IndexFacesRequest
                    {
                        CollectionId = _awsSettings.AWSRekognitionCollectionID,
                        Image = new Image { S3Object = new S3Object { Bucket = _awsSettings.AWSS3BucketName, Name = s3Object.Key } }
                    };

                    var indexFaceResponse = await rekognitionClient.IndexFacesAsync(indexFaceRequest);

                    return new RequestResult
                    {
                        code = "00",
                        data = indexFaceResponse.FaceRecords,
                        message = "Success",
                        Succeeded = true

                    };
                }

            }
            catch (CustomException ex)
            {
                _logger.LogError($"An error occurred, {ex.Message}, {ex.StackTrace}");
                return new RequestResult { code = ex.code, data = null, message = ex.Message, Succeeded = false };
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred, {ex.Message}, {ex.StackTrace}");
                return new RequestResult { code = "UNEXPECTED_ERROR", data = null, message = "An unexpected error occurred.", Succeeded = false };
            }
        }
    }
}
