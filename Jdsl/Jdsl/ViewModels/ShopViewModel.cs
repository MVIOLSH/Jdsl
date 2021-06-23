using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Jdsl.Models;
using MvvmHelpers.Commands;
using Flurl.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Jdsl.ViewModels
{
    class ShopViewModel : BaseViewModel
    {
        public List<Shop> shops { get; set; }

        public ObservableCollection<Shop> shopsCollection { get; set; }
        public AsyncCommand RefreshCommand { get; }
        public Command DelayLoadMore { get; }
        public Command LoadMoreCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public bool isBusy { get; set; }

        public void LoadList()
        {
            var Http = new HttpClient();
            var response = Http.GetAsync("https://jdshops-api-app.azurewebsites.net/api/shops");
            var json = JsonConvert.DeserializeObject<ObservableCollection<Shop>>(response.ToString());
            shopsCollection = json;
        }

        void Refresh()
        {
            IsBusy = true;
        }
        void CallShopCommand()
        {

        }

        void EditShopCommand()
        {

        }

    }
}








