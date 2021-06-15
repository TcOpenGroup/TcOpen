namespace TcOpen.Inxton.Abstractions.Security
{
    public interface IUser
    {
        bool CanUserChangePassword { get; set; }
        string Email { get; set; }
        string Level { get; set; }
        string[] Roles { get; set; }
        string Username { get; set; }
    }
}
