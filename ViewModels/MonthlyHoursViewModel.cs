using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DolgozoiBeleptetoMobilApp.Models;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class MonthlyHoursViewModel : ObservableObject
    {
        [ObservableProperty] private List<string> ledolgozottNapok = new();

        public ICommand LoadDataCommand => new AsyncRelayCommand(LoadDataAsync);

        private async Task LoadDataAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"{ApiConstants.BaseUrl}/api/havimunka?dolgozoId=1");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<List<HaviMunkaDto>>();
                LedolgozottNapok = data.Select(d => $"{d.Datum.ToShortDateString()}: {d.LedolgozottIdoPerc} perc").ToList();
            }
        }
    }
}


