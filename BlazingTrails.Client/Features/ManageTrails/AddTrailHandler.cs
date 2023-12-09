using BlazingTrails.Shared.Features.ManageTrails;
using MediatR;
using System.Net.Http.Json;
using System.Security.Principal;

namespace BlazingTrails.Client.Features.ManageTrails;

public class AddTrailHandler : IRequestHandler<AddTrailRequest, AddTrailRequest.Response>
{
    private readonly HttpClient _httpClient;
    public AddTrailHandler(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AddTrailRequest.Response> Handle(AddTrailRequest request, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(AddTrailRequest.RouteTemplate, request, cancellationToken);

        //the request was successful, then the trailId is read from the response and returned using
        //the AddTrail-Request.Response record we previously defined.
        if (response.IsSuccessStatusCode)
        {
            var trailId = await response.Content.ReadFromJsonAsync<int>(cancellationToken: cancellationToken);

            return new AddTrailRequest.Response(trailId);
        }
        //If the request failed, a response is returned containing a negative number. This will be used
        //in the calling code to identify a problem.
        else
        {
            return new AddTrailRequest.Response(-1);
        }
    }
}
