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
    /// Логика взаимодействия для BoatWindow.xaml
    /// </summary>
    public partial class BoatWindow : Window
    {
        public Boat myBoat = null;
        public BoatWindow()
        {
            InitializeComponent();
        }

        private void OnOK_Click(object sender, RoutedEventArgs e)
        {
            if (boat_name.Text == "") { MessageBox.Show("Не указано наименование плавсредства !"); return; }
            if (boat_descr.Text == "") { MessageBox.Show("Не указано описание плавсредства !"); return; }
            if (boat_sn.Text == "") { MessageBox.Show("Не указан серийный номер плавсредства !"); return; }
            int id = 0;
            int.TryParse(boat_id.Text, out id);
            myBoat = new Boat(id, boat_name.Text, boat_descr.Text, boat_sn.Text);
            DialogResult = true;
        }

        public void SetBoat(Boat bot)
        {
            boat_id.Text = $"{bot.BoatID:0000}";
            boat_name.Text = bot.BoatName;
            boat_descr.Text = bot.Description;
            boat_sn.Text = bot.SerialNumber;
        }
    }
}
