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
    /// Логика взаимодействия для ClientOrdersWindow.xaml
    /// </summary>
    public partial class ClientOrdersWindow : Window
    {
        DateTime currentDate = DateTime.Now;
        List<BoatOrder> orders = null;
        public ClientOrdersWindow()
        {
            InitializeComponent();
        }

        private void OnOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OnSelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {
            currentDate = (date.SelectedDate == null) ? DateTime.Now : (DateTime)date.SelectedDate;
            if (currentDate < DateTime.Now)
            {
                MessageBox.Show("Выбирайте дату сегодня или позже. В прошлом забронировать нельзя !");
            }
            else
            {
                List<Boat> lb = Boat.GetBoatList();
                List<string> ls = new List<string>();
                foreach (Boat b in lb)
                {
                    if (b.BoatNumStatus == 0) ls.Add($"BoatID {b.BoatID:D04} {b.BoatName,-20}");
                }
                cmbBoats.ItemsSource = ls;
                cmbBoats.SelectedIndex = 0;
            }
        }

        private void OnSelectionHourChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnSelectionBoatChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbBoats.SelectedItem != null)
            {
                string sNum = cmbBoats.SelectedItem as string;
                sNum = sNum.Substring(7, 4);
                if (int.TryParse(sNum, out int uid) && orders != null)
                {
                    foreach(BoatOrder bor in orders)
                    {
                        if (bor.CheckOrders(currentDate, uid))
                        {
                            cmbHour.ItemsSource = bor.GetFreeHours();
                            cmbHour.SelectedIndex = 0;
                            return;
                        }
                    }
                    BoatOrder boatOrder = new BoatOrder(currentDate, uid);
                    cmbHour.ItemsSource = boatOrder.GetFreeHours();
                    cmbHour.SelectedIndex = 0;
                    orders.Add(boatOrder);
                }
            }
        }

        public void SetBoatOrders(List<BoatOrder> boatOrders)
        {
            orders = boatOrders;
        }
    }
}
