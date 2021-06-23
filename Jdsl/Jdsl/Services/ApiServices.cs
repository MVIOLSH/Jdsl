using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Jdsl.Models;
using Flurl;
using Flurl.Http;
using Jdsl.Helpers;
using Jdsl.Views;
using Xamarin.Forms;

namespace Jdsl.Services
{
    class ApiServices
    {
        public async Task<bool> RegisterAsync(RegisterModel model)
        {
            var client = new HttpClient();
           
          
            var response = await client.PostAsJsonAsync<RegisterModel>("https://jdshops-api-app.azurewebsites.net/api/account/register/",
                model);
            return response.IsSuccessStatusCode;
            
        }

        public async Task<LoginResult> LoginAsync(LoginModel loginModel)
        {
            var client = new HttpClient();
            var response =
                await client.PostAsJsonAsync<LoginModel>("https://jdshops-api-app.azurewebsites.net/api/account/login", loginModel);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                Settings.SecurityToken = jsonString;
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(jsonString);
                var loginResult = new LoginResult()
                {
                    UserId = int.Parse(tokenS.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value),
                    Role = tokenS.Claims.First(c => c.Type == ClaimTypes.Role).Value,
                    UserName = tokenS.Claims.First(c => c.Type == ClaimTypes.Name).Value,
                    IsSuccesfull = true

                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jsonString);
                Settings.UserFullName = loginResult.UserName;
                Settings.UserRole = loginResult.Role;
                Settings.UserId = loginResult.UserId;
                Settings.Username = loginModel.Email;
                Settings.Password = loginModel.PasswordHash;


                if (!response.IsSuccessStatusCode)
                {
                    loginResult.IsSuccesfull = false;
                    return loginResult;
                }
                return loginResult;
            }
            else
            {
                var loginResult = new LoginResult()
                {
                    IsSuccesfull = false
                };
                return loginResult;
            }

        }
    }
}
