namespace Tournament.Management.API.Helpers.Interfaces
{
    public interface IPasswordHelper
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string enteredPassword, string storedHash);
    }
}