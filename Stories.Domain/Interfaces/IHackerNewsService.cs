using Stories.Domain.Entities;

namespace Stories.Domain.Interfaces;

public interface IHackerNewsService
{
    Task<IEnumerable<HackerNewsStory>> GetBestStoriesAsync(int n);
}
