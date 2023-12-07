using FluentValidation;
using MediatR;

namespace BlazingTrails.Shared.Features.ManageTrails;
public record AddTrailRequest(TrailDto Trail): IRequest<AddTrailRequest.Response>
{
    //defines the address of the API endpoint for the request
    public const string RouteTemplate = "/api/trails";

    //defines the response data for the request.
    public record Response(int TrailId);
}
//A validator for the request. This will be executed by the API to make sure the request datais valid.
public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
{
    public AddTrailRequestValidator()
    {
        //Specifies the TrailValidator as the validator for the Trail property
        RuleFor(x => x.Trail).SetValidator(new TrailValidator());
    }
}
