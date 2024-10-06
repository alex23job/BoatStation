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
            ClientOrdersWindow cow = new ClientOrdersWindow();
            if (currentClient != null)
            {
                cow.Title = $"Заказы клиента {currentClient.SecondName}";
                cow.SetBoatOrders(orders, currentClient.ClientUser.ID);
                if (cow.ShowDialog() == true)
                {
                    UpdateViewOrdersPanel();
                }
            }
        }

        private void OnDelOrderClick(object sender, RoutedEventArgs e)
        {
            EditOrdersWindow ew = new EditOrdersWindow();
            ew.Title = $"Заказы клиента {currentClient.SecondName}";
            ew.SetParams(orders, currentClient);
            if (ew.ShowDialog() == true) { UpdateViewOrdersPanel(); }
        }

        private void OnEditOrderClick(object sender, RoutedEventArgs e)
        {
            EditOrdersWindow ew = new EditOrdersWindow();
            ew.Title = $"Заказы клиента {currentClient.SecondName}";
            ew.SetParams(orders, currentClient);
            if (ew.ShowDialog() == true) { UpdateViewOrdersPanel(); }
        }

        private void OnManualClick(object sender, RoutedEventArgs e)
        {
            InstructionWindow wi = new InstructionWindow();
            wi.ShowDialog();
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
                    if (currentUser.Rule == 1) ViewOrdersPanel();
                    if (currentUser.Rule == 0)
                    {
                        currentClient = MyClient.GetClient(currentUser.ID);
                        ViewOrdersPanel();
                    }
                    dataClient.Text = $"{MyUser.Rules[currentUser.Rule]}\n{currentUser.Name}";
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
                            dataClient.Text = $"{MyUser.Rules[currentUser.Rule]}\n{currentUser.Name}";
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
            List<OrderView> orderViews = BoatOrder.GetOrdersViewList(orders, currentDate);
            if (orderViews.Count == 0)
            {
                if (boats == null) boats = Boat.GetBoatList();
                foreach (Boat bot in boats) if (bot.BoatNumStatus == 0) orders.Add(new BoatOrder(currentDate, bot.BoatID));
                orderViews = BoatOrder.GetOrdersViewList(orders, currentDate);
            }
            ordersData.ItemsSource = orderViews;
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
                //ordersData.ItemsSource = BoatOrder.GetOrdersViewList(orders, currentDate);
                bool isNew = false;
                foreach(BoatOrder bo in orders)
                {
                    if (bo.OrderID == 0)
                    {
                        DBUtils.AddBoatOrder(bo);
                        isNew = true;
                    }
                    else
                    {
                        if (bo.IsEdit)
                        {
                            DBUtils.UpdateBoatOrder(bo);
                            bo.ChangeSaved();
                        }
                    }
                }
                if (isNew)
                {
                    orders = BoatOrder.GetOrdersList();
                    
                }
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
            if (userList.SelectedItem == null)
            {
                MessageBox.Show("Для редактирования нужно выбрать пользователя !");
                return;
            }
            RegistrationWindow rw = new RegistrationWindow();
            string sNum = userList.SelectedItem as string;
            sNum = sNum.Substring(5, 4);
            MyUser editUser = null; 
            if (int.TryParse(sNum, out int uid)) editUser = MyUser.GetUser(uid);
            if (editUser != null) rw.SetUser(editUser);
            MyClient editClient = MyClient.GetClient(uid);
            if (editClient != null) rw.SetClient(editClient);
            if (rw.ShowDialog() == true)
            {
                if (rw.Client != null)
                {
                    if (editClient == null)
                    {   // не были заполнены поля клиента
                        DBUtils.AddClient(rw.Client);
                    }
                    else if (editClient.Compare(rw.Client) == false)
                    {   //  поля клиента изменены
                        DBUtils.UpdateClient(rw.Client);
                        if (editUser.Compare(rw.User) == false)
                        {
                            DBUtils.UpdateUser(rw.User);
                        }
                    }
                }
                else if (rw.User != null)
                {
                    if (editUser.Compare(rw.User) == false)
                    {
                        DBUtils.UpdateUser(rw.User);
                    }
                }
                ViewAdminPanel();
            }
        }

        private void OnDelUserClick(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem == null)
            {
                MessageBox.Show("Для удаления нужно выбрать пользователя !");
                return;
            }
            string sNum = userList.SelectedItem as string;
            sNum = sNum.Substring(5, 4);
            MyUser editUser = null;
            if (int.TryParse(sNum, out int uid)) editUser = MyUser.GetUser(uid);
            if (editUser != null)
            {
                if (MessageBox.Show($"Выбран пользователь : ID:{editUser.ID} {MyUser.Rules[editUser.Rule]} {editUser.Name} (логин:{editUser.Email})\n\nУдалить пользователя ?", "Удаление пользователя", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (editUser.Rule == 0)
                    {
                        DateTime dt = DateTime.Now;
                        orders = BoatOrder.GetOrdersList();
                        foreach (BoatOrder bo in orders)
                        {
                            if (bo.Date > dt) bo.ClearClientOrders(editUser.ID);
                            if (bo.IsEdit)
                            {
                                DBUtils.UpdateBoatOrder(bo);
                                bo.ChangeSaved();
                            }
                        }
                        MyClient delClient = MyClient.GetClient(editUser.ID);
                        if (delClient != null) DBUtils.DelClient(delClient);
                    }
                    DBUtils.DelUser(editUser);
                    ViewAdminPanel();
                }
            }
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
            if (boatList.SelectedItem == null)
            {
                MessageBox.Show("Для редактирования нужно выбрать плавсредство !");
                return;
            }
            BoatWindow bw = new BoatWindow();
            string sNum = boatList.SelectedItem as string;
            sNum = sNum.Substring(7, 4);
            Boat editBot = null;
            if (int.TryParse(sNum, out int uid)) editBot = Boat.GetBoat(uid);
            if (editBot != null) bw.SetBoat(editBot);
            if (bw.ShowDialog() == true)
            {
                if (bw.myBoat != null)
                {
                    if (editBot.Compare(bw.myBoat) == false)
                    {
                        DBUtils.UpdateBoat(bw.myBoat);
                    }
                }
                ViewBoatsPanel();
            }
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
                //MessageBox.Show($"BoatID:{bor.BoatNumber} {bor.BoatName}  hour={zn_col + 7}");
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
                if (currentUser.Rule == 1 && bo != null)
                {
                    int clientID = 0;
                    if (int.TryParse(bo.GetClientUID(zn_col - 2), out clientID))
                    {
                        MyClient cli = MyClient.GetClient(clientID);
                        if (cli != null)
                        {
                            ClientDataViewWindow cw = new ClientDataViewWindow();
                            cw.SetClient(cli, $"плавсредство <{bor.BoatNumber} {bor.BoatName}>\nЧас = {(zn_col + 7):D02}:00");
                            cw.ShowDialog();
                        }
                    }
                }
            }
        }
    }
}
