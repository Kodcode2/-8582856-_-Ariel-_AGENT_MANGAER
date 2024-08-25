using Microsoft.AspNetCore.Mvc;
using MossadAPI.Services.Interfaces;

namespace MossadAPI.Services.Implementation
{
    public class LoginService : ILoginService
    {
        public async Task<string> Login(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            else if (id == "SimulationServer" || id == "MVCServer")
            {
                return Guid.NewGuid().ToString();
            }
            throw new Exception("Faild");
        }
    }
}
