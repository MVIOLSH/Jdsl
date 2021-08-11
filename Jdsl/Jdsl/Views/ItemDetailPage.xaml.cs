using System.ComponentModel;
using Jdsl.ViewModels;
using Xamarin.Forms;

namespace Jdsl.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel _viewModel;
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = _viewModel =new ItemDetailViewModel();
            
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
           

        }
    }
}