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

namespace BoatStation
{
    /// <summary>
    /// Логика взаимодействия для EditOrdersWindow.xaml
    /// </summary>
    public partial class EditOrdersWindow : Window
    {
        List<BoatOrder> orders = null;
        List<Boat> boats = null;
        MyClient client = null;
        int indexHour = -1, newHour = -1;
        int orderID = -1;

        public EditOrdersWindow()
        {
            InitializeComponent();
            delBtn.IsEnabled = false;
        }

        private void OnDelButtonClick(object sender, RoutedEventArgs e)
        {
            if (indexHour >= 0 && indexHour < 12 && orderID != -1)
            {
                foreach(BoatOrder bor in orders)
                {
                    if (bor.OrderID == orderID)
                    {
                        bor.ClearOrder(indexHour);
                        DBUtils.UpdateBoatOrder(bor);
                        bor.ChangeSaved();
                    }
                }
                UpdateListOrders();
            }
        }

        private void OnSelectionHourChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbHours.SelectedItem != null)
            {
                string sHour = cmbHours.SelectedItem as string;
                sHour = sHour.Substring(0, 2);
                int.TryParse(sHour, out newHour);
                newHour -= 9;
            }
        }

        private void OnSelectionOrderChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listOrders.SelectedItem == null) return;
            string sOrder = listOrders.SelectedItem as string;
            string sID = sOrder.Substring(sOrder.Length - 5, 4);
            string sHour = sOrder.Substring(sOrder.Length - 14, 2);
            if (int.TryParse(sID, out int oid) && int.TryParse(sHour, out indexHour))
            {
                indexHour -= 9;
                orderID = oid;
                foreach(BoatOrder bo in orders)
                {
                    if (bo.OrderID == oid)
                    {
                        cmbHours.ItemsSource = bo.GetFreeHours();
                        if (cmbHours.Items.Count > 0)
                        {
                            cmbHours.SelectedIndex = 0;
                            newHour = -1;
                        }
                    }
                }
            }
            delBtn.IsEnabled = true;
        }

        public void SetParams(List<BoatOrder> list, MyClient cli)
        {
            orders = list;
            client = cli;
            boats = Boat.GetBoatList();
            UpdateListOrders();
        }

        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OnChangeBtnClick(object sender, RoutedEventArgs e)
        {
            if (indexHour >= 0 && indexHour < 12 && orderID != -1 && newHour != -1)
            {
                foreach (BoatOrder bor in orders)
                {
                    if (bor.OrderID == orderID)
                    {
                        bor.ClearOrder(indexHour);
                        bor.SetOrder(newHour, client.ClientUser.ID);
                        DBUtils.UpdateBoatOrder(bor);
                        bor.ChangeSaved();
                    }
                }
                UpdateListOrders();
            }
        }

        private void UpdateListOrders()
        {
            DateTime dt = DateTime.Now, curDate = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
            List<string> clientOrders = new List<string>();
            foreach(BoatOrder bo in orders)
            {
                Boat b = GetBoat(bo.BoatID);
                if (bo.Date > curDate)
                {
                    clientOrders.AddRange(bo.GetClientOrder($"{client.ClientUser.ID:D04}", b));
                }
            }
            listOrders.ItemsSource = clientOrders;
        }

        private Boat GetBoat(int bid)
        {
            if (boats == null) return null;
            foreach(Boat b in boats)
            {
                if (b.BoatID == bid) return b;
            }
            return null;
        }
    }
}
