# ElasticSearchMinimalAPI.NET8

**ElasticSearchMinimalAPI.NET8** is a project built with .NET 8 using Minimal API architecture, designed to perform CRUD and management operations on ElasticSearch. This project provides a set of endpoints to simplify working with ElasticSearch for data indexing, querying, and deletion.

## Features

- **ElasticSearch Integration**: Connects to ElasticSearch to manage index listing, document insertion, updating, and deletion operations.
- **Minimal API**: Leverages .NET 8 Minimal API architecture to provide a fast and performant RESTful API.
- **Nest and Newtonsoft.Json Libraries**: Utilizes `Nest` for ElasticSearch operations and `Newtonsoft.Json` to manage JSON data effectively.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- ElasticSearch instance (local or remote) with a connection string

### Installation

1. **Clone the Repository:**

    ```bash
    git clone https://github.com/Moongazing/elasticsearch-dotnet8.git
    cd ElasticSearchMinimalAPI.NET8
    ```

2. **Configure ElasticSearch Connection:**
   
   Update the `appsettings.json` file with your ElasticSearch connection details.

   ```json
   {
       "ElasticSearchConfig": {
           "ConnectionString": "http://localhost:9200"
       }
   }


The API will start at http://localhost:5000 by default.
API Endpoints
Index Management

    GET /api/indexes - Lists all indexes.
    POST /api/indexes - Creates a new index with settings.

Document Management

    POST /api/documents/bulk - Bulk inserts documents into the specified index.
    DELETE /api/documents/{indexName}/{elasticId} - Deletes a document by its ElasticSearch ID.
    POST /api/documents - Inserts a single document.
    PUT /api/documents/{indexName}/{elasticId} - Updates a document by its ID.

Search

    GET /api/documents/search - Retrieves all documents from the specified index with pagination.
    GET /api/documents/searchByField - Searches documents by a specific field.
    GET /api/documents/searchByQuery - Performs a search using a simple query string.

Example Usage

Here is an example request for bulk inserting documents:

curl -X POST "http://localhost:5000/api/documents/bulk" -H "Content-Type: application/json" -d '[
    { "id": 1, "name": "Document 1" },
    { "id": 2, "name": "Document 2" }
]'

Technologies Used

    .NET 8 Minimal API
    Nest for ElasticSearch client
    Newtonsoft.Json for JSON serialization and handling

Contributing

Feel free to fork this project, submit issues, or suggest improvements. All contributions are welcome!
