using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty] private string welcomeText = "Üdvözöllek!";
        [ObservableProperty] private string workedTime = "00:00:00";

        private DateTime? startTime;
        private Timer timer;
        private readonly HttpClient client;
        private readonly int dolgozoId;
        private readonly string nev;

        public DashboardViewModel()
        {
            client = new HttpClient();
            dolgozoId = Preferences.Get("dolgozoId", 0);
            nev = Preferences.Get("nev", "");

            WelcomeText = $"Üdvözöllek, {nev}!";

            timer = new Timer(UpdateWorkedTime, null, Timeout.Infinite, 1000);
        }

        public IRelayCommand StartWorkCommand => new RelayCommand(async () =>
        {
            startTime = DateTime.Now;
            timer.Change(0, 1000);

            var munka = new
            {
                dolgozoId = dolgozoId,
                belepesIdo = DateTime.Now
            };

            await client.PostAsJsonAsync($"{ApiConstants.BaseUrl}/api/attendance/checkin", munka);
        });

        public IRelayCommand EndWorkCommand => new RelayCommand(async () =>
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            var munka = new
            {
                dolgozoId = dolgozoId,
                kilepesIdo = DateTime.Now
            };

            await client.PostAsJsonAsync($"{ApiConstants.BaseUrl}/api/attendance/checkout", munka);

            startTime = null;
            WorkedTime = "00:00:00";
        });

        public IRelayCommand ViewMonthlyCommand => new RelayCommand(async () =>
        {
            await Shell.Current.GoToAsync("//MonthlyHoursPage");
        });

        private void UpdateWorkedTime(object state)
        {
            if (startTime.HasValue)
            {
                var elapsed = DateTime.Now - startTime.Value;
                WorkedTime = elapsed.ToString("hh\\:mm\\:ss");
            }
        }
    }

}
