using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Jdsl.Helpers;
using Jdsl.Models;
using Jdsl.Services;
using Jdsl.Views;
using Xamarin.Forms;

namespace Jdsl.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }

        public ICommand LoginCommand {
            get
            { 
                return new Command(async () =>
                {
                    var loginModel = new LoginModel()
                    {
                        Email = Username,
                        PasswordHash = Password
                    };

                   var isSuccess = await _apiServices.LoginAsync(loginModel);
                   if (isSuccess.IsSuccesfull == true)
                   {
                       Message = "Login Success";
                       await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                        Acr.UserDialogs.UserDialogs.Instance.Toast(Message, new TimeSpan(15));
                    }
                   else
                   {
                       Message = "Login Unsuccessful";
                       Acr.UserDialogs.UserDialogs.Instance.Toast(Message, new TimeSpan(15));
                    }
                   

                });
                

            }
        }

        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
        }

    }
}
