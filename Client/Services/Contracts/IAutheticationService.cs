using Entities;

namespace BlazorStore.Client;

public interface IAutheticationService
{
    Task<bool> Login(UserLogin info);
    Task Logout();
}