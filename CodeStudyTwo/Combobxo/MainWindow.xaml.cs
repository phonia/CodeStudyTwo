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

namespace Combobxo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<City> cs = new List<City>() { 
            new City(){Id=1,Name="衡阳"},
            new City(){Id=2,Name="长沙"},
            new City(){Id=3,Name="密西西比"},
            new City(){Id=4,Name="明斯特"},
            new City(){Id=5,Name="柏林"},
            new City(){Id=6,Name="慕尼黑"}
        };

        public List<City> MyCity
        {
            get { return cs; }
            set { cs = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

    }

    public class City
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

}
