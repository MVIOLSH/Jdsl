using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Flurl;
using Jdsl.Helpers;
using Jdsl.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Jdsl.ViewModels
{
   
    public class ItemDetailViewModel : BaseViewModel
    {
        public ObservableCollection<Shop> Items { get; }
        public ObservableCollection<Shop> jsonString { get; set; }
        public ObservableCollection<ImgShop> Images { get; }
        public Xamarin.Forms.Command CallShopCommand { get; }
        public Command LoadItemsCommand { get; }
        public Command LaunchMap { get; }
        public string mapCoordinatesLongitude { get; set; }
        public string mapCoordinatesLatitude { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string SIshopNumber { get; set; }
        public string SITown { get; set; }
        public string SIPhoneNumber { get; set; }
        public string SIDeliveryDescription { get; set; }

        public Shop shopitem { get; set; }
        

        private string itemId = Settings.temp;

        public ItemDetailViewModel()
        {
            Items = new ObservableCollection<Shop>();
            Images = new ObservableCollection<ImgShop>();
            Title = "Shop Details";
            CallShopCommand = new Xamarin.Forms.Command(async ()=> await CallShop());
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemId());
            LaunchMap = new Command(async () => await LaunchMapMethod());
            mapCoordinatesLatitude = "";
            mapCoordinatesLongitude = "";
            SIDeliveryDescription = "";
            SIshopNumber = "";
            SIPhoneNumber = "";
            SITown = "";





        }

        
        async Task ExecuteLoadItemId()
            {
                Items.Clear();
                IsBusy = false;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.SecurityToken);
                var response = await client.GetAsync($"https://jdshops-api-app.azurewebsites.net/api/shops/{itemId}");
                jsonString = await response.Content.ReadAsAsync<ObservableCollection<Shop>>();
                var items = jsonString;
                foreach (var item in items)
                {
                    Items.Add(item);
                    mapCoordinatesLongitude = item.MapCoordinatesLongitude;
                    mapCoordinatesLatitude = item.MapCoordinatesLatitude;
                    SIDeliveryDescription = item.DeliveryInfo;
                    SIshopNumber = item.ShopNumber.ToString();
                    SIPhoneNumber = item.PhoneNumber;
                    SITown = item.Town;
            }
                
                Images.Clear();
                var imageResponse = await client.GetAsync($"https://jdshops-api-app.azurewebsites.net/api/images/{itemId}");
                var jsonImage = await imageResponse.Content.ReadAsAsync<ObservableCollection<ImgShop>>();
                foreach (var img in jsonImage)
                {
                    var url = Url.Combine("https://jdshops-api-app.azurewebsites.net/api/images/",
                        img.ShopNumber, img.FileName);
                    img.Url = url;
                    Images.Add(img);
                }





            }

            async Task CallShop()
            {
                await Launcher.OpenAsync(new Uri(("tel:" + shopitem.PhoneNumber)));
            }

            public void OnAppearing()
            {
                IsBusy = true;
                
            }

            async Task LaunchMapMethod()
            {
                lon = double.Parse(mapCoordinatesLongitude, CultureInfo.InvariantCulture);
                lat = double.Parse(mapCoordinatesLatitude, CultureInfo.InvariantCulture);
                var location = new Location(lat, lon);
                var options = new MapLaunchOptions { Name = Settings.temp};

                try
                {
                    await Map.OpenAsync(location, options);
                }
                catch (Exception ex)
                {
                    // No map application available to open
                }

        }


        
    }
}
