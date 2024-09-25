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
        public MyClient currentClient = null;
        private List<MyUser> users = null;
        private List<Boat> boats = null;
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
                    if (currentUser.Rule == 3) ViewAdminPanel();
                    if (currentUser.Rule == 2) ViewBoatsPanel();
                    if (currentUser.Rule == 0) ViewOrdersPanel();
                    return;
                }
            }
            MessageBox.Show("Ошибка ввода логина и/или пароля !!!");
        }

        private void OnRegBtnClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            if (rw.ShowDialog() == true)
            {
                if (rw.Client != null)
                {
                    if (rw.User != null)
                    {
                        if (MyUser.CheckUser(rw.User.Email, rw.User.Password) == null)
                        {
                            DBUtils.AddUser(rw.User);
                            currentUser = MyUser.CheckUser(rw.User.Email, rw.User.Password);
                            rw.Client.SetUser(currentUser);
                            DBUtils.AddClient(rw.Client);
                            currentClient = MyClient.GetClient(currentUser.ID);
                            ViewOrdersPanel();
                        }
                        else
                        {
                            MessageBox.Show($"Клиент с Email <{rw.User.Email}> уже есть в базе !");
                        }
                    }
                }
            }
        }

        private void OnSelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ViewAdminPanel()
        {
            users = MyUser.GetUserList();
            List<string> list = new List<string>();
            foreach (MyUser mu in users) list.Add(mu.ToString());
            userList.ItemsSource = list;            
            adminView.Visibility = Visibility.Visible;
        }

        private void ViewOrdersPanel()
        {
            loginView.Visibility = Visibility.Hidden;
            ordersView.Visibility = Visibility.Visible;
        }

        private void ViewBoatsPanel()
        {
            boats = Boat.GetBoatList();
            List<string> list = new List<string>();
            foreach (Boat b in boats) list.Add(b.ToString());
            boatList.ItemsSource = list;
            loginView.Visibility = Visibility.Hidden;
            boatView.Visibility = Visibility.Visible;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            loginView.Visibility = Visibility.Visible;
            adminView.Visibility = Visibility.Hidden;
        }

        private void OnNewUserClick(object sender, RoutedEventArgs e)
        {
            RegistrationWindow rw = new RegistrationWindow();
            if (rw.ShowDialog() == true)
            {
                if (rw.Client != null)
                {
                    if (rw.User != null)
                    {
                        if (MyUser.CheckUser(rw.User.Email, rw.User.Password) == null)
                        {
                            DBUtils.AddUser(rw.User);
                            currentUser = MyUser.CheckUser(rw.User.Email, rw.User.Password);
                            rw.Client.SetUser(currentUser);
                            DBUtils.AddClient(rw.Client);
                            //currentClient = MyClient.GetClient(currentUser.ID);
                        }
                    }
                }
                else if (rw.User != null)
                {
                    DBUtils.AddUser(rw.User);
                }
                ViewAdminPanel();
            }
        }

        private void OnEditUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnDelUserClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnNewBoatClick(object sender, RoutedEventArgs e)
        {
            BoatWindow bw = new BoatWindow();
            if (bw.ShowDialog() == true)
            {

            }
        }

        private void OnEditBoatClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnDelBoatClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnCancelBoatClick(object sender, RoutedEventArgs e)
        {
            loginView.Visibility = Visibility.Visible;
            boatView.Visibility = Visibility.Hidden;
        }
    }
}
