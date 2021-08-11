using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using Jdsl.Helpers;
using Jdsl.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jdsl.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<Announcement> Announcements { get; set; }
        public Command LoadItemsCommand { get; }
        public ICommand OpenWebCommand { get; }

        public AboutViewModel()
        {
            Title = "Dashboard";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            Announcements = new ObservableCollection<Announcement>();


            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://szymanski.uk"));
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = false;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Settings.SecurityToken);
            var response = await client.GetAsync("https://jdshopsapi.szymanski.uk/api/announcements");
            var jsonString = await response.Content.ReadAsAsync<ObservableCollection<Announcement>>();
            var items = jsonString;
            Announcements.Clear();
            foreach (var item in items)
            {
                Announcements.Add(item);
            }
            var response2 = await client.GetAsync("https://jdshopsapi.szymanski.uk/api/shops");
            var jsonString2 = await response2.Content.ReadAsAsync<ObservableCollection<Shop>>();
            Settings.ItmesString = JsonConvert.SerializeObject(jsonString2);
        }
        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}