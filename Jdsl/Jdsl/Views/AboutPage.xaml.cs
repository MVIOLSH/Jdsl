using System;
using System.ComponentModel;
using Jdsl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jdsl.Views
{
    public partial class AboutPage : ContentPage
    {
        private AboutViewModel _viewModel;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AboutViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}