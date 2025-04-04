using System.Windows;

namespace PattySlapsApp
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;

        public MainWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private void OpenEmployeeManagement_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagementWindow employeeWindow = new EmployeeManagementWindow();
            employeeWindow.Show();
        }

        private void OpenInventoryManagement_Click(object sender, RoutedEventArgs e)
        {
            InventoryManagementWindow inventoryWindow = new InventoryManagementWindow();
            inventoryWindow.Show();
        }

        private void OpenRecruitmentManagement_Click(object sender, RoutedEventArgs e)
        {
            HireRequestsWindow recruitmentWindow = new HireRequestsWindow();
            recruitmentWindow.Show();
        }

        private void OpenApplicationManagement_Click(object sender, RoutedEventArgs e)
        {
            ApplicationManagementWindow recruitmentWindow = new ApplicationManagementWindow();
            recruitmentWindow.Show();
        }

        private void OpenShiftSchedule_Click(object sender, RoutedEventArgs e)
        {
            ShiftScheduleWindow shiftScheduleWindow = new ShiftScheduleWindow(_apiService);
            shiftScheduleWindow.Show();
        }
    }
}