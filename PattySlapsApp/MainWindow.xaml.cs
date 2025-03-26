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
        { /*
            RecruitmentManagementWindow recruitmentWindow = new RecruitmentManagementWindow();
            recruitmentWindow.Show();
        */
        }
}
}