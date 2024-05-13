using JwtApi.Models;

namespace JwtApi;
public static class Consts
{
	public static string REGULAR_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMiIsIm5hbWUiOiJ1c2VyIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE3MTYyMzkwMjJ9.xjlyWN16miKFcgRM0x1qoolJws30pZ6zQXMKSS5DSS8";
	public static string ADMIN_TOKEN = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMiIsIm5hbWUiOiJ1c2VyIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjE3MTYyMzkwMjIsInJvbGUiOiJhZG1pbiJ9.J5qBrtktHtR70Wa22JafF_XuvZgmcpapwtFn2FhalkE";

	public static string SECRET_KEY = "super_secret_signing_key_for_jwt_test";
	
	public static HashSet<User> users = new()
	{
		new User(1, "admin", "admin",100, Roles.ADMIN),
		new User(2, "user", "user", 40, Roles.USER),
		new User(3, "st0rm", "12345678", 15,Roles.USER),
		new User(4, "myLogin", "1234", 30, Roles.USER),
		new User(5, "yupi", "yupi1234", 18, Roles.USER),
		new User(6, "qwerty", "qwerty",35, Roles.USER)
	};
}
