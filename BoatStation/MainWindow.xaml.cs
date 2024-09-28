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
        private List<BoatOrder> orders = null;
        private DateTime currentDate = DateTime.Now;
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
                    if (currentUser.Rule == 0)
                    {
                        currentClient = MyClient.GetClient(currentUser.ID);
                        ViewOrdersPanel();
                    }                    
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
            currentDate = (date.SelectedDate == null) ? DateTime.Now : (DateTime)date.SelectedDate;
            ViewOrdersPanel();
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
            date.SelectedDate = currentDate;
            orders = BoatOrder.GetOrdersList();
            if (orders.Count == 0)
            {
                if (boats == null) boats = Boat.GetBoatList();
                foreach (Boat bot in boats) orders.Add(new BoatOrder(currentDate, bot.BoatID));
            }
            ordersData.ItemsSource = BoatOrder.GetOrdersViewList(orders, currentDate);
            loginView.Visibility = Visibility.Hidden;
            ordersView.Visibility = Visibility.Visible;
        }

        private void UpdateViewOrdersPanel()
        {
            date.SelectedDate = currentDate;
            //orders = BoatOrder.GetOrdersList();
            /*if (orders.Count == 0)
            {
                if (boats == null) boats = Boat.GetBoatList();
                foreach (Boat bot in boats) orders.Add(new BoatOrder(currentDate, bot.BoatID));
            }*/
            if (orders != null)
            {
                //ordersData.ItemsSource = null;
                ordersData.ItemsSource = BoatOrder.GetOrdersViewList(orders, currentDate);
            }
            //loginView.Visibility = Visibility.Hidden;
            //ordersView.Visibility = Visibility.Visible;
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
                if (bw.myBoat != null)
                {
                    if (Boat.CheckBoat(bw.myBoat) == null)
                    {
                        DBUtils.AddBoat(bw.myBoat);
                    }
                }
                ViewBoatsPanel();
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

        private void OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void OnCurrentCellsChanged(object sender, EventArgs e)
        {
            //MessageBox.Show($"sender => {sender.ToString()}\nargs => {e.ToString()}");
            DataGrid dg = sender as DataGrid;
            if (dg != null)
            {
                OrderView bor = dg.CurrentCell.Item as OrderView;
                int zn_col = dg.CurrentColumn.DisplayIndex;
                MessageBox.Show($"BoatID:{bor.BoatNumber} {bor.BoatName}  hour={zn_col + 7}");
                BoatOrder bo = null;
                if (orders != null)
                {
                    int.TryParse(bor.BoatNumber, out int id);
                    foreach(BoatOrder b in orders)
                    {
                        if (b.CheckOrders(currentDate, id))
                        {
                            bo = b;
                            break;
                        }
                    }
                }
                if (currentUser.Rule == 0)
                {
                    if (bo != null)
                    {
                        if (bo.hourOrders[zn_col - 2] == "")
                        {   //  забронировать для текущего клиента
                            //bo.hourOrders[zn_col - 2] = currentClient.TblClient;
                            bo.SetOrder(zn_col - 2, currentClient.ClientUser.ID);
                        }
                        else
                        {   //  снять бронь ?
                            if (MessageBox.Show($"Выбрана запись клиента : BoatID:{bor.BoatNumber} {bor.BoatName}  hour={zn_col + 7}\n\nОтменить бронь ?", "Снятие брони клиента", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            {
                                bo.ClearOrder(zn_col - 2);
                            }
                        }
                        UpdateViewOrdersPanel();
                    }
                }
                if (currentUser.Rule == 1)
                {

                }
            }
        }
    }
}
