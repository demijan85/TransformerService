# TransformerService

A simple ASP.NET Core Web API application that provides an endpoint to apply a series of transformers to input strings.

## Prerequisites

- **Docker**: Ensure Docker is installed and running on your machine.

## Getting Started

### Building the Docker Image

1. Open a terminal window.
2. Navigate to the root directory of the project where the `Dockerfile` is located.
3. Run the following command to build the Docker image:

   ```bash
   docker build -t transformer-service:latest .
   ```

   - **`-t transformer-service:latest`**: Tags the image with the name `transformer-service` and the tag `latest`.
   - **`.`**: Specifies the build context (current directory).

### Running the Docker Container

Run the following command to start the Docker container:

```bash
docker run -d -p 8080:80 --name transformer-service transformer-service:latest
```

- **`-d`**: Runs the container in detached mode (in the background).
- **`-p 8080:80`**: Maps port `80` in the container to port `8080` on the host machine.
- **`--name transformer-service`**: Names the running container `transformer-service`.
- **`transformer-service:latest`**: Specifies the image to run.

### Verifying the Service is Running

You can verify that the service is running by accessing the API endpoint (`http://localhost:8080/swagger/index.html`) or checking the container status:

- **Check Container Status**:

  ```bash
  docker ps
  ```

  You should see `transformer-service` listed among the running containers.
  
## Running tests

Run the following command to start tests:

```bash
dotnet test
```

  
## Using the API

Navigate to `http://localhost:8080/swagger/index.html` and you can test the api from there or use Postman to send a test request to the API


### API Endpoint

- **URL**: `http://localhost:8080/api/transform`
- **Method**: `POST`
- **Content-Type**: `application/json`

### Request Format

The API expects a JSON payload with the following structure:

```json
{
  "elements": [
    {
      "value": "string",
      "transformers": [
        {
          "groupId": "string",
          "transformerId": "string",
          "parameters": {
            "additionalProp1": "string",
            "additionalProp2": "string",
            "additionalProp3": "string"
          }
        }
      ]
    }
  ]
}
```

### Example
```
{
    "elements": [
        {
            "value": "Dejjan Test 11122",
            "transformers": [
                {
                    "groupId": "Group1",
                    "transformerId": "RegexRemoval",
                    "parameters": {
                        "regex": "\\d+"
                    }
                },
                {
                    "groupId": "Group1",
                    "transformerId": "RegexReplacement",
                    "parameters": {
                        "regex": "j",
                        "replacement": "k"
                    }
                }
            ]
        },
        {
            "value": "Дејан Поповић Test 11122 ДП",
            "transformers": [
                {
                    "groupId": "Group1",
                    "transformerId": "RegexRemoval",
                    "parameters": {
                        "regex": "\\d+"
                    }
                },
                {
                    "groupId": "Group1",
                    "transformerId": "CyrillicGreekToLatin",
                    "parameters": {
                    }
                },
                {
                    "groupId": "Group1",
                    "transformerId": "RegexReplacement",
                    "parameters": {
                        "regex": "j",
                        "replacement": "k"
                    }
                }
            ]
        }
    ]
}
```