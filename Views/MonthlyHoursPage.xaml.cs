using CommunityToolkit.Mvvm.Input;
using DolgozoiBeleptetoMobilApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolgozoiBeleptetoMobilApp.Views
{
    public partial class MonthlyHoursPage : ContentPage
    {
        private MonthlyHoursViewModel ViewModel => (MonthlyHoursViewModel)BindingContext;

        public MonthlyHoursPage()
        {
            InitializeComponent();
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await ((AsyncRelayCommand)ViewModel.LoadDataCommand).ExecuteAsync(null);
        }
    }
}
