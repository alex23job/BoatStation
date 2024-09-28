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
    /// Логика взаимодействия для ClientDataViewWindow.xaml
    /// </summary>
    public partial class ClientDataViewWindow : Window
    {
        MyClient cli = null;
        public ClientDataViewWindow()
        {
            InitializeComponent();
        }

        public void SetClient(MyClient client, string boatOrder)
        {
            cli = client;
            order.Text = $"Заказ : {boatOrder}";
            fname.Text = $"Фамилия : {cli.FirstName}";
            sname.Text = $"Имя : {cli.SecondName}";
            pasport.Text = $"Паспорт : {cli.Pasport}";
            tlf.Text = $"Телефон : {cli.Tlf}";
        }
    }
}
