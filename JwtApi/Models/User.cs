namespace JwtApi.Models;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public int Age { get; set; }
	public Roles Role { get; set; }

	public User() { }
	public User(int id, string login, string password, int age, Roles role)
	{
		Id = id;
		Login = login;
		Password = password;
		Age = age;
		Role = role;
	}
}
