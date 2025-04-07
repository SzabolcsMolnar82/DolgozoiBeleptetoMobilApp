using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace DolgozoiBeleptetoMobilApp.Views
{
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();
            _ = LogoutAsync();
        }

        private async Task LogoutAsync()
        {
            await SecureStorage.SetAsync("jwt_token", string.Empty);
            Preferences.Remove("dolgozoId");
            Preferences.Remove("nev");

            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
