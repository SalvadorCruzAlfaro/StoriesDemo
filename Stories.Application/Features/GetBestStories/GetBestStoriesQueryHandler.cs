using MediatR;
using Stories.Domain.Entities;
using Stories.Domain.Interfaces;

namespace Stories.Application.Features.GetBestStories;

public class GetBestStoriesQueryHandler : IRequestHandler<GetBestStoriesQuery, IEnumerable<HackerNewsStory>>
{
    private readonly IHackerNewsService _hackerNewsService;

    public GetBestStoriesQueryHandler(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    public async Task<IEnumerable<HackerNewsStory>> Handle(GetBestStoriesQuery request, CancellationToken cancellationToken)
    {
        return await _hackerNewsService.GetBestStoriesAsync(request.NumberOfStories);
    }
}
