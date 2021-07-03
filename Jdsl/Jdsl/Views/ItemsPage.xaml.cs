using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jdsl.Models;
using Jdsl.ViewModels;
using Jdsl.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jdsl.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel();
            _viewModel.LoadItemsCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}