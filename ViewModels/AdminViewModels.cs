using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using DolgozoiBeleptetoMobilApp.Models;


namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class AdminViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<DolgozoDto> dolgozok = new();
        [ObservableProperty] private string ujNev;
        [ObservableProperty] private string ujFelhasznaloNev;
        [ObservableProperty] private string ujJelszo;

        public AdminViewModel()
        {
            LoadDolgozok();
        }

        public IRelayCommand RefreshCommand => new RelayCommand(LoadDolgozok);

        public IRelayCommand<int> DeleteCommand => new RelayCommand<int>(async (id) =>
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync($"{ApiConstants.BaseUrl}/api/admin/delete-employee/{id}");

            if (response.IsSuccessStatusCode)
            {
                var torlendo = Dolgozok.FirstOrDefault(d => d.Id == id);
                if (torlendo != null) Dolgozok.Remove(torlendo);

                await Shell.Current.DisplayAlert("Siker", "Dolgozó törölve!", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Hiba", $"Törlés sikertelen: {response.StatusCode}", "OK");
            }
        });

        private async void LoadDolgozok()
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetFromJsonAsync<List<DolgozoDto>>($"{ApiConstants.BaseUrl}/api/admin/get-employees");

                if (result != null)
                    Dolgozok = new ObservableCollection<DolgozoDto>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Dolgozók betöltése sikertelen: {ex.Message}");
            }
        }

        public IRelayCommand AddCommand => new RelayCommand(async () =>
        {
            try
            {
                var token = await SecureStorage.GetAsync("jwt_token");

                var dolgozo = new
                {
                    Nev = UjNev,
                    FelhasznaloNev = UjFelhasznaloNev,
                    Jelszo = UjJelszo
                };

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsJsonAsync($"{ApiConstants.BaseUrl}/api/admin/add-employee", dolgozo);
                if (response.IsSuccessStatusCode)
                {
                    UjNev = UjFelhasznaloNev = UjJelszo = string.Empty;
                    LoadDolgozok();
                }
                else
                {
                    Console.WriteLine($"❌ Hozzáadás sikertelen: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Hiba új dolgozó hozzáadásakor: {ex.Message}");
            }
        });


    }
}