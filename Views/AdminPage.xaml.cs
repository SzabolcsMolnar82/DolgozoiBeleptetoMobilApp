using DolgozoiBeleptetoMobilApp.Models;
using DolgozoiBeleptetoMobilApp.ViewModels;

namespace DolgozoiBeleptetoMobilApp.Views
{
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
    }
}
