using MediatR;
using Stories.Domain.Entities;

namespace Stories.Application.Features.GetBestStories;

public class GetBestStoriesQuery : IRequest<IEnumerable<HackerNewsStory>>
{
    public int NumberOfStories { get; set; }
}
