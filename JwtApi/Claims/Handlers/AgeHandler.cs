using System.Security.Claims;
using JwtApi.Claims.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace JwtApi.Claims.Handlers;

class AgeHandler : AuthorizationHandler<AgeRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
		AgeRequirement requirement)
	{
		var ageClaim = context.User.FindFirst(claim => claim.Type == "age");
		if (ageClaim is not null)
		{
			if (int.TryParse(ageClaim.Value, out var age))
			{
				if (age>=requirement.Age)
				{
					context.Succeed(requirement); 
				}
			}
		}
		return Task.CompletedTask;
	}
}