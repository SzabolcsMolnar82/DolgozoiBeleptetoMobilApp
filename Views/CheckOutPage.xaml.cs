using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolgozoiBeleptetoMobilApp.ViewModels;

namespace DolgozoiBeleptetoMobilApp.Views
{
    public partial class CheckOutPage : ContentPage
    {
        public CheckOutPage()
        {
            InitializeComponent();
            BindingContext = new CheckOutViewModel();
        }
    }
}
