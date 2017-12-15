using Santa.Classes;
using Santa.Models;

namespace Santa.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        User Authenticate(UserLogin user);
    }
}