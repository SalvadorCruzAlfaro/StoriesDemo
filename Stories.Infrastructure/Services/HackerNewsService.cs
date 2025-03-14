using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Stories.Domain.Configuration;
using Stories.Domain.Entities;
using Stories.Domain.Interfaces;
using System.Net.Http.Json;
using System.Runtime;
using System.Text.Json;

namespace Stories.Infrastructure.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly HackerNewsApiSettings _settings;

    public HackerNewsService(HttpClient httpClient, IMemoryCache cache, IOptions<HackerNewsApiSettings> settings)
    {
        _httpClient = httpClient;
        _cache = cache;
        _settings = settings.Value;
    }

    public async Task<IEnumerable<HackerNewsStory>> GetBestStoriesAsync(int n)
    {
        var storyIds = await GetBestStoryIdsAsync();
        var stories = new List<HackerNewsStory>();

        foreach (var id in storyIds.Take(n))
        {
            var story = await GetStoryDetailsAsync(id);
            if (story != null) // Only add the story if it's valid
            {
                stories.Add(story);
            }
        }

        return stories.OrderByDescending(s => s.Score);
    }

    private async Task<IEnumerable<int>> GetBestStoryIdsAsync()
    {
        const string cacheKey = "best_story_ids";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<int> storyIds))
        {
            var response = await _httpClient.GetAsync(_settings.BestStoriesUrl);
            response.EnsureSuccessStatusCode();
            storyIds = await response.Content.ReadFromJsonAsync<IEnumerable<int>>();
            _cache.Set(cacheKey, storyIds, TimeSpan.FromMinutes(5));
        }
        return storyIds;
    }

    private async Task<HackerNewsStory> GetStoryDetailsAsync(int id)
    {
        var cacheKey = $"story_{id}";
        if (!_cache.TryGetValue(cacheKey, out HackerNewsStory story))
        {
            var url = string.Format(_settings.StoryDetailsUrl, id);
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var storyJson = await response.Content.ReadFromJsonAsync<JsonElement>();

            // Check if required fields exist in the JSON response
            if (!storyJson.TryGetProperty("title", out var titleProp) ||
                !storyJson.TryGetProperty("url", out var urlProp) ||
                !storyJson.TryGetProperty("by", out var byProp) ||
                !storyJson.TryGetProperty("time", out var timeProp) ||
                !storyJson.TryGetProperty("score", out var scoreProp) ||
                !storyJson.TryGetProperty("descendants", out var descendantsProp))
            {
                // If any required field is missing, skip this story
                return null;
            }

            story = new HackerNewsStory
            {
                Title = titleProp.GetString(),
                Url = urlProp.GetString(),
                PostedBy = byProp.GetString(),
                Time = DateTimeOffset.FromUnixTimeSeconds(timeProp.GetInt64()).DateTime,
                Score = scoreProp.GetInt32(),
                CommentCount = descendantsProp.GetInt32()
            };

            _cache.Set(cacheKey, story, TimeSpan.FromMinutes(5));
        }
        return story;
    }
}
