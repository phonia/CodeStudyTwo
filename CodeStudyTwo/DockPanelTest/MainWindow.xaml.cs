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

namespace DockPanelTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in dp.Children)
            {
                if (item.GetType().Equals(typeof(ListBox))) continue;
                if (Convert.ToInt32((item as Button).Content) <= Convert.ToInt32((sender as Button).Content))
                {
                    (sender as Button).SetValue(DockPanel.DockProperty, Dock.Top);
                    if (Convert.ToInt32((item as Button).Content) == Convert.ToInt32((sender as Button).Content))
                    {
                        //(sender as ListBox).SetValue(DockPanel.DockProperty, Dock.Top);
                        this.l1.SetValue(DockPanel.DockProperty, Dock.Top);
                    }
                }
                else
                {
                    (item as Button).SetValue(DockPanel.DockProperty, Dock.Top);
                }
            }
            //for (int i = dp.Children.Count - 1; i > 0; i--)
            //{
            //    var item = dp.Children[i];
            //    if (item.GetType().Equals(typeof(ListBox))) continue;
            //    if (Convert.ToInt32((item as Button).Content) <= Convert.ToInt32((sender as Button).Content))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        (item as Button).SetValue(DockPanel.DockProperty, Dock.Bottom);
            //    }
            //}
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
