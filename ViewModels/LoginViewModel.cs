using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DolgozoiBeleptetoMobilApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] private string felhasznaloNev;
        [ObservableProperty] private string jelszo;
        [ObservableProperty] private string errorMessage;

        public ICommand LoginCommand => new AsyncRelayCommand(LoginAsync);

        private async Task LoginAsync()
        {
            try
            {
                // var loginData = new { FelhasznaloNev = FelhasznaloNev, Jelszo = Jelszo };

                var loginData = new LoginRequest
                {
                    felhasznaloNev = FelhasznaloNev,
                    jelszo = Jelszo
                };


                var client = new HttpClient();
                var response = await client.PostAsJsonAsync($"{ApiConstants.BaseUrl}/api/auth/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    await SecureStorage.SetAsync("jwt_token", result.Token);
                    await Shell.Current.GoToAsync("//CheckInPage");
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Bejelentkezési hiba: {response.StatusCode}\n{content}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private class LoginResponse { public string Token { get; set; } }
    }
}
