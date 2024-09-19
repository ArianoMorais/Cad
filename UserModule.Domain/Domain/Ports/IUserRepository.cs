using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task<User> FindByCpfOrEmailAsync(string cpf, string email);
        Task<User> GetUserByEmailAsync(string email);
    }
}
