# Selfie Identification API Documentation

## Introduction

This document provides detailed documentation for the Selfie Identification API endpoints.

## Register Selfie

Registers a selfie image by uploading it to Amazon S3 and indexing it with Amazon Rekognition.

- **Endpoint**: POST /api/v1/selfie/register
- **Request Body**: Base64 encoded string of the image.
- **Responses**:
  - 201 Created: Returns a success message if the registration is successful.
    - Content Type: application/json
    - Body: {"code": "00", "message": "Success", "succeeded": true}
  - 400 Bad Request: Returns an error message if the request is malformed or missing required parameters.
    - Content Type: application/json
    - Body: {"code": "400", "message": "Bad Request", "succeeded": false}
  - 500 Internal Server Error: Returns an error message if there is a server-side issue.
    - Content Type: application/json
    - Body: {"code": "500", "message": "Server Error", "succeeded": false}

## Authenticate Selfie

Authenticates a selfie image by comparing it with the registered faces in the Amazon Rekognition collection.

- **Endpoint**: POST /api/v1/selfie/authenticate
- **Request Body**: Base64 encoded string of the image.
- **Responses**:
  - 200 OK: Returns "Authentication successful!" if the selfie matches a registered face.
    - Content Type: application/json
    - Body: {"code": "200", "message": "Authentication successful!", "succeeded": true}
  - 400 Bad Request: Returns an error message if the request is malformed or missing required parameters.
    - Content Type: application/json
    - Body: {"code": "400", "message": "Bad Request", "succeeded": false}
  - 500 Internal Server Error: Returns an error message if there is a server-side issue.
    - Content Type: application/json
    - Body: {"code": "500", "message": "Server Error", "succeeded": false}

## Error Responses

- 400 Bad Request: Returned if the request is malformed or missing required parameters.
- 500 Internal Server Error: Returned in case of any server-side error with a message indicating the error.

## Example Usage

### Register Selfie

```http
POST /api/v1/selfie/register
Content-Type: application/json

{
  "base64Image": "<Base64 encoded image string>"
}

### Authenticate Selfie
```http
POST /api/v1/selfie/authenticate
Content-Type: application/json

{
  "base64Image": "<Base64 encoded image string>"
}

Response

{"code": "SELFIE_MATCH", "message": "Selfie Authentication successful!", "succeeded": true}
