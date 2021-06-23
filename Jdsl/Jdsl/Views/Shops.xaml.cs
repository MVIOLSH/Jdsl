using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jdsl.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Jdsl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shops : ContentPage
    {
        public Shops()
        { 
            InitializeComponent();
            this.BindingContext = new ShopViewModel();
        }
    }
}