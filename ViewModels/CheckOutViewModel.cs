using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class CheckOutViewModel : ObservableObject
    {
        public ICommand CheckOutCommand => new AsyncRelayCommand(CheckOutAsync);

        private async Task CheckOutAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"{ApiConstants.BaseUrl}/api/attendance/check-out?dolgozoId=1", null);
            if (!response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Hiba", "Nem sikerült a kijelentkezés!", "OK");
            }
        }
    }

}
