using Data_Access_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Login_Logic
{
    public interface IUserAuthentication
    {
        Task<string> AuthenticateAsync(User_Login_Dto userLoginDto);
        Task<string> RegisterAsync(User_Register_Dto userRegisterDto);


    }
}
