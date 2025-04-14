using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Timers;
using DolgozoiBeleptetoMobilApp.Views;

namespace DolgozoiBeleptetoMobilApp.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
    [ObservableProperty] private string welcomeText = "Üdvözöllek!";
    [ObservableProperty] private string workedTime = "00:00:00";

    private DateTime? startTime;
    private System.Timers.Timer timer;
    private readonly HttpClient client;
    private readonly int dolgozoId;
    private readonly string nev;

    public DashboardViewModel()
    {
        client = new HttpClient();
        dolgozoId = Preferences.Get("dolgozoId", 0);
        nev = Preferences.Get("nev", "");

        WelcomeText = $"Üdvözöllek, {nev}!";

        timer = new System.Timers.Timer(1000);
        timer.Elapsed += (s, e) => UpdateWorkedTime();
        timer.AutoReset = true;
    }

    public IRelayCommand StartWorkCommand => new RelayCommand(async () =>
    {
        startTime = DateTime.Now;
        timer.Start();

        await client.PostAsync($"{ApiConstants.BaseUrl}/api/attendance/check-in?dolgozoId={dolgozoId}", null);
    });

    public IRelayCommand EndWorkCommand => new RelayCommand(async () =>
    {
        timer.Stop();

        await client.PostAsync($"{ApiConstants.BaseUrl}/api/attendance/check-out?dolgozoId={dolgozoId}", null);

        startTime = null;
        WorkedTime = "00:00:00";
    });

    public IRelayCommand ViewMonthlyCommand => new RelayCommand(async () =>
    {
        await Shell.Current.GoToAsync("MonthlyHoursPage");
    });

        private void UpdateWorkedTime()
        {
            if (startTime.HasValue)
            {
                var elapsed = DateTime.Now - startTime.Value;
                var formatted = elapsed.ToString("hh\\:mm\\:ss");

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    WorkedTime = formatted;
                });
            }
        }

    }


}



