using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PattySlapsApp
{
    /// <summary>
    /// Interaction logic for InventoryManagementWindow.xaml
    /// </summary>
    public partial class InventoryManagementWindow : Window
    {
        private readonly ApiService _apiService;

        public InventoryManagementWindow()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private void InventoryRecordsManagement_Click(object sender, RoutedEventArgs e)
        {
            InventoryRecordManagementWindow inventoryRecordManagementWindow = new InventoryRecordManagementWindow();
            inventoryRecordManagementWindow.Show();
        }

        private void WasteManagement_Click(object sender, RoutedEventArgs e)
        {
            WasteManagementWindow wasteManagementWindow = new WasteManagementWindow();
            wasteManagementWindow.Show();
        }

        private void QCChecklist_Click(object sender, RoutedEventArgs e)
        {
            QCChecklistWindow qcChecklistWindow = new QCChecklistWindow(_apiService);
            qcChecklistWindow.Show();
        }
    }
}
