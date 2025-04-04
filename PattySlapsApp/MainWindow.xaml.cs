using System.Windows;

namespace PattySlapsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}