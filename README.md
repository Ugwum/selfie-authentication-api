# Selfie Authentication API

This is a .NET Core Web API project that provides endpoints for registering and authenticating selfies using Amazon Rekognition and Amazon S3.

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- .NET Core SDK
- AWS account with access to Amazon Rekognition and Amazon S3

### Installing

1. Clone the repository:

   ```bash
   git clone https://github.com/ugwum/selfie-authentication-api.git

2. Navigate to the project directory:

   ```bash
   cd selfie-authentication-api
3. Update AWS credentials and configuration:

    Open appSettings.json and replace "YOUR_AWS_ACCESS_KEY", "YOUR_AWS_SECRET_KEY", "YOUR_S3_BUCKET_NAME", and "YOUR_REKOGNITION_COLLECTION_ID" with your actual AWS credentials and details using the AWSSettings section.

4. Build and run the project:

	```bash
    dotnet build
    dotnet run
## API Documentation
For detailed documentation on the API endpoints, refer to API Documentation.

## Built With
	.NET Core
	Amazon Rekognition
	Amazon S3
## Authors
Obinna Agim 

License
This project is licensed under the MIT License - see the LICENSE file for details.
