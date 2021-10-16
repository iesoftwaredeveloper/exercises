# SystemTextJson

## How to use this project

- Download the files in this directory
- dotnet build
- dotnet run

## JsonDocument

The ```JsonDocument``` class can be used to examine the content of a JSON value. It is effective when you need to find a value in a large JSON object and want to mimimize memory usage.

### Parse(string json, System.Text.Json.JsonDocumentOptions options = default)
