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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        int mode = 0;
        MyUser user = null;
        public RegistrationWindow()
        {
            InitializeComponent();
            cmbRule.ItemsSource = MyUser.Rules;
            cmbRule.SelectedIndex = 0;
        }

        public void SetMode(int mod)
        {
            mode = mod;
        }

        private void OnOK_Click(object sender, RoutedEventArgs e)
        {
            if (firstName.Text == "") { MessageBox.Show("Не указана фамилия !"); return; }
            if (secondName.Text == "") { MessageBox.Show("Не указано имя !"); return; }
            if (login.Text == "") { MessageBox.Show("Не указан логин (email) !"); return; }
            if ((pass1.Text == "") || (pass2.Text == "") || (pass1.Text != pass2.Text)) { MessageBox.Show("Не указаны или не соврадают пароли !"); return; }
            if (tlf.Text == "") { MessageBox.Show("Не указан тедефон !"); return; }
            if (pasport.Text == "") { MessageBox.Show("Не указаны данные паспорта !"); return; }
        }
    }
}
