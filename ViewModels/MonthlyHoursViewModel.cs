using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DolgozoiBeleptetoMobilApp.ViewModels;

public partial class MonthlyHoursViewModel : ObservableObject
{
    // ObservableCollection, ami tartalmazza a napi munkaidőket
    public ObservableCollection<HaviMunkaDto> HaviMunkaLista { get; set; } = new();

    // Parancs, amit az OnAppearing hív meg a nézeten
    public IAsyncRelayCommand LoadDataCommand { get; }

    public MonthlyHoursViewModel()
    {
        LoadDataCommand = new AsyncRelayCommand(LoadMonthlyData);
    }

    private async Task LoadMonthlyData()
    {
        try
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            var dolgozoId = Preferences.Get("dolgozoId", 0);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetFromJsonAsync<MonthlyMunkaResponse>(
                $"{ApiConstants.BaseUrl}/api/attendance/monthly-work/{dolgozoId}");

            HaviMunkaLista.Clear();

            if (response != null && response.Days != null)
            {
                foreach (var nap in response.Days)
                {
                    HaviMunkaLista.Add(new HaviMunkaDto
                    {
                        Datum = DateTime.Parse(nap.Datum).ToString("yyyy. MMM dd."),
                        LedolgozottIdo = $"{nap.LedolgozottIdoPerc / 60} óra {nap.LedolgozottIdoPerc % 60} perc"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Hiba", $"Nem sikerült betölteni: {ex.Message}", "OK");
        }
    }

    // DTO-k a szerver válaszához
    public class MonthlyMunkaResponse
    {
        public List<NapiMunka> Days { get; set; }
        public int TotalDaysWorked { get; set; }
    }

    public class NapiMunka
    {
        public string Datum { get; set; }
        public int LedolgozottIdoPerc { get; set; }
    }

    public class HaviMunkaDto
    {
        public string Datum { get; set; }
        public string LedolgozottIdo { get; set; }
    }
}


