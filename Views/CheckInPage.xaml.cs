using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DolgozoiBeleptetoMobilApp.ViewModels;

namespace DolgozoiBeleptetoMobilApp.Views
{
    public partial class CheckInPage : ContentPage
    {
        public CheckInPage()
        {
            InitializeComponent();
            BindingContext = new CheckInViewModel();
        }
    }
}