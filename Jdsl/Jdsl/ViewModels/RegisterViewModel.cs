using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Jdsl.Helpers;
using Jdsl.Models;
using Jdsl.Services;
using Jdsl.Views;
using  Xamarin.Forms;

namespace Jdsl.ViewModels
{
    class RegisterViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Message { get; set; }
        public ICommand RegisterCommand 
        {
            get
            {
                return new Command(async () =>
                {
                    var model = new RegisterModel()
                    {
                        email = Username,
                        passwordHash = Password,
                        confirmPassword = ConfirmPassword,
                        fname = Name,
                        lname = Surname
                    };
                    var isSuccess =
                        await _apiServices.RegisterAsync(model);

                    Settings.Username = Username;
                    Settings.Password = Password;

                    if (isSuccess)
                    {
                        Message = "Reistered Succesfully";
                        Acr.UserDialogs.UserDialogs.Instance.Toast(Message, new TimeSpan(15));
                        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                    }
                    else
                    {
                        Message = "Something went wrong! Contact admin or try later";
                        Acr.UserDialogs.UserDialogs.Instance.Toast(Message, new TimeSpan(15));
                    }
                });
            }

            set { }

  
            
        }
        
    }
   
}

