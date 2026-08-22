
namespace QuemVaiVai.Application.Interfaces.Security
{
    public interface IHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }
}
