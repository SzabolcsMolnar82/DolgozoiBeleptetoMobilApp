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
    public partial class CheckInViewModel : ObservableObject
    {
        public ICommand CheckInCommand => new AsyncRelayCommand(CheckInAsync);

        private async Task CheckInAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync($"{ApiConstants.BaseUrl}/api/attendance/check-in?dolgozoId=1", null);
            if (!response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Hiba", "Nem sikerült a bejelentkezés!", "OK");
            }
        }
    }
}
