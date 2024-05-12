using Microsoft.AspNetCore.Authorization;

namespace JwtApi.Claims.Requirements;

class AgeRequirement : IAuthorizationRequirement
{
	protected internal int Age { get; set; }
	public AgeRequirement(int age) => Age = age;
}

