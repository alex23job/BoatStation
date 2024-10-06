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
    /// Логика взаимодействия для InstructionWindow.xaml
    /// </summary>
    public partial class InstructionWindow : Window
    {
        public InstructionWindow()
        {
            InitializeComponent();
            instr.Text = $"*. При посадке в лодку нельзя вставать на борт или сиденья.\n*. Не перегружайте лодку или катер.\n*.На ходу не выставляйте руки за борт.\n*.Не ныряйте с катера или лодки.\n*.Не садитесь на борт, не пересаживайтесь с места на место, не пересаживайтесь в воде в другие плавсредства.\n*.Не берите с собой детей до 7 лет и не разрешайте пользоваться плавсредствами детям до 16 лет.\n*.Не разрешается кататься в тумане, вблизи шлюзов, плотин, а также останавливаться вблизи мостов или под ними.\n*Нельзя ставить борт лодок параллельно идущей волне, так как она может опрокинуть судно.\n*.Поднимать пострадавшего из воды желательно с носа или кормы, иначе можно перевернуться.\n*.Не кататься в местах скопления людей на воде – в районах пляжей, переправ, водноспортивных соревнований.";
        }
    }
}
