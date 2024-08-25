using Microsoft.AspNetCore.Mvc;

namespace MossadAPI.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> Login(string id);
    }
}
