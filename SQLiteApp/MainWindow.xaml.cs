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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SQLiteApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();
            DataAccess.AddData("กฤษณพงศ์", "เทพวีระกุล", "kritsanaphong-keang@hotmail.com");
            DataAccess.AddData("วรนุช", "กิจการ", "nuchyyyy113112@gmail.com");
        }

        private void Show(object sender, RoutedEventArgs e)
        {
            List<Customer> list = DataAccess.GetData();
            string result = "";
            foreach (Customer customer in list)
            {
                result += customer.FirstName + " " + customer.LastName + " " + customer.Email + "\n";
            }
            MessageBox.Show(result);
        }
    }
}
