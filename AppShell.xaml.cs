using DolgozoiBeleptetoMobilApp.Views;

namespace DolgozoiBeleptetoMobilApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("AdminPage", typeof(AdminPage));
            Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("CheckInPage", typeof(CheckInPage));
            Routing.RegisterRoute("CheckOutPage", typeof(CheckOutPage));
            Routing.RegisterRoute("MonthlyHoursPage", typeof(MonthlyHoursPage));
        }
    }
}
