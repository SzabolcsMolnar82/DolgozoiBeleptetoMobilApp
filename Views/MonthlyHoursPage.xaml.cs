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
        public MonthlyHoursPage()
        {
            InitializeComponent();
            BindingContext = new MonthlyHoursViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is MonthlyHoursViewModel vm && vm.LoadDataCommand is IAsyncRelayCommand asyncCmd)
            {
                await asyncCmd.ExecuteAsync(null);
            }
        }
    }
}