# HackerNewsAPI (Clean Architecture + MediatR)

This is a RESTful API implemented in ASP.NET Core using **Clean Architecture** and the **Mediator** pattern with **MediatR**. It retrieves the details of the top `n` stories from Hacker News, sorted by their score in descending order.

---

## Configuration

The URLs for the Hacker News API are configured in the `appsettings.json` file:

```json
{
  "HackerNewsApi": {
    "BestStoriesUrl": "https://hacker-news.firebaseio.com/v0/beststories.json",
    "StoryDetailsUrl": "https://hacker-news.firebaseio.com/v0/item/{0}.json"
  }
}
```

---

## How to Run the Project

Follow these steps to run the project on your local environment:

### Prerequisites

1. **.NET SDK**: Make sure you have the .NET 6 SDK or later installed. You can download it from [here](https://dotnet.microsoft.com/download).
2. **Code Editor**: We recommend using [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/).

### Steps to Run the Project

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-username/HackerNewsAPI.git
   cd HackerNewsAPI
   ```

2. **Restore Dependencies**:
   Run the following command to restore NuGet packages:
   ```bash
   dotnet restore
   ```

3. **Configure API URLs (Optional)**:
   If you want to change the Hacker News API URLs, edit the `appsettings.json` file in the project's root.

4. **Run the Application**:
   Use the following command to run the application:
   ```bash
   dotnet run
   ```

   This will start the API at `http://localhost:5000` (or `http://localhost:8080` if port 5000 is occupied).

5. **Test the API**:
   Once the application is running, you can test the API using tools like [Postman](https://www.postman.com/) or directly from your browser.

   - **Endpoint**: Get the top `n` stories.
     ```
     GET http://localhost:5000/api/stories/best?n=5
     ```

   - **Expected Response**:
     ```json
     [
       {
         "title": "A uBlock Origin update was rejected from the Chrome Web Store",
         "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
         "postedBy": "ismaildonmez",
         "time": "2019-10-12T13:43:01+00:00",
         "score": 1716,
         "commentCount": 572
       },
       {
         "title": "Another Story Title",
         "uri": "https://example.com/another-story",
         "postedBy": "anotheruser",
         "time": "2023-01-01T00:00:00+00:00",
         "score": 1500,
         "commentCount": 300
       }
     ]
     ```

---

## Project Structure

The project is organized into the following layers:

- **Application**: Contains use cases and business logic.
- **Domain**: Defines entities and contracts.
- **Infrastructure**: Implements access to the Hacker News API.
- **WebAPI**: Exposes the API endpoints.

---
