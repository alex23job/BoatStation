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
        int clientID = 0;
        int userID = 0;
        MyUser user = null;
        MyClient client = null;

        public MyClient Client { get { return client; } }
        public MyUser User { get { return user; } }
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
            if (cmbRule.SelectedIndex == 0)
            {
                user = new MyUser(userID, 0, secondName.Text, login.Text, pass1.Text);
                client = new MyClient(clientID, firstName.Text, secondName.Text, tlf.Text, pasport.Text, user);
            }
            if (cmbRule.SelectedIndex > 0)
            {
                user = new MyUser(userID, cmbRule.SelectedIndex, secondName.Text, login.Text, pass1.Text); 
            }
            DialogResult = true;
        }

        public void SetClient(MyClient mc)
        {
            clientID = mc.ClientID;
            //textID.Text = $"{mc.TblClient}";
            firstName.Text = mc.FirstName;
            secondName.Text = mc.SecondName;
            login.Text = mc.ClientUser.Email;
            pass1.Text = mc.ClientUser.Password;
            pass2.Text = pass1.Text;
            tlf.Text = mc.Tlf;
            pasport.Text = mc.Pasport;
        }

        public void SetUser(MyUser mu)
        {
            userID = mu.ID;
            textID.Text = $"{mu.ID:0000}";
            cmbRule.SelectedIndex = mu.Rule;
            login.Text = mu.Email;
            pass1.Text = mu.Password;
            pass2.Text = pass1.Text;
            secondName.Text = mu.Name;
            mode = mu.Rule;
        }
    }
}
