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

namespace BoatStation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string startupPath = AppDomain.CurrentDomain.BaseDirectory;
        public MyUser currentUser = null;
        public MainWindow()
        {
            InitializeComponent();
            string logoPath = MainWindow.startupPath + "\\catamarans.png";
            logo.Source = new BitmapImage(new Uri(logoPath));
        }

        private void AppClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void OnNewOrderClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnDelOrderClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnEditOrderClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnManualClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnLoginBtnClick(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            if (lw.ShowDialog() == true)
            {
                currentUser = MyUser.CheckUser(lw.userLogin, lw.userPassword);
                if (currentUser != null)
                {
                    loginView.Visibility = Visibility.Hidden;
                    if (currentUser.Rule == 3) adminView.Visibility = Visibility.Visible;
                    if (currentUser.Rule == 0) ordersView.Visibility = Visibility.Visible;
                    return;
                }
            }
            MessageBox.Show("Ошибка ввода логина и/или пароля !!!");
        }

        private void OnRegBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnSelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnNewUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnEditUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnDelUserClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
