using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Jdsl.Helpers;
using Jdsl.Models;
using Jdsl.Views;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace Jdsl.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Shop _selectedItem;

        public ObservableCollection<Shop> Items { get; }
        public ObservableCollection<Shop> jsonString { get; set; }

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        
        public Xamarin.Forms.Command<Shop> ItemTapped { get; }
        public Xamarin.Forms.Command<Shop> CallShopCommand { get; }
        public int LoadCounter { get; set; } = 50;
        public string PhraseSearch { get; set; }
        public Xamarin.Forms.Command SearchCommand { get; }

        public ItemsViewModel()
        {
            Title = "Shops";
            Items = new ObservableCollection<Shop>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Xamarin.Forms.Command<Shop>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
            
            CallShopCommand = new Xamarin.Forms.Command<Shop>(CallShop);
            SearchCommand = new Xamarin.Forms.Command(Search);
        }

         async void CallShop(Shop shop)
        {
            //Device.OpenUri(new Uri("tel:" + obj));
            await Launcher.OpenAsync(new Uri(("tel:" + shop.PhoneNumber)));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = false;
            
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Settings.SecurityToken);
                var response = await client.GetAsync("https://jdshops-api-app.azurewebsites.net/api/shops");
                jsonString = await response.Content.ReadAsAsync<ObservableCollection<Shop>>();
                var items = jsonString;
                foreach (var item in items)
                {
                    if (Items.Count <= LoadCounter)
                    {
                        Items.Add(item);
                    }
                    else
                    {
                        return;
                    }
                    
                    
                }

                

        }

        async void Search()
        {
            IsBusy = false;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Settings.SecurityToken);
            var response = await client.GetAsync($"https://jdshops-api-app.azurewebsites.net/api/shops?searchPhrase={PhraseSearch}");
            jsonString = await response.Content.ReadAsAsync<ObservableCollection<Shop>>();
            var items = jsonString;
            Items.Clear();
            foreach (var item in items)
            {
                Items.Add(item);
            }

            

        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Shop SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Shop item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            Settings.temp = item.ShopNumber.ToString();
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
            
        }

       
    }
}