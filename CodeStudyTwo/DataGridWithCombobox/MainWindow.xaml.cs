using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGridWithCombobox
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Nationality> ns = new List<Nationality>() { 
                new Nationality(){Id=1,Ch="中",En="CH"},
                new Nationality(){Id=2,Ch="美",En="Am"},
                new Nationality(){Id=3,Ch="德",En="Ge"}
            };

        private List<City> cs = new List<City>() { 
            new City(){Id=1,NationalityId=1,Name="衡阳"},
            new City(){Id=2,NationalityId=1,Name="长沙"},
            new City(){Id=3,NationalityId=2,Name="密西西比"},
            new City(){Id=4,NationalityId=2,Name="明斯特"},
            new City(){Id=5,NationalityId=3,Name="柏林"},
            new City(){Id=6,NationalityId=3,Name="慕尼黑"}
        };

        private List<Nation> _nations = new List<Nation>() { 
            new Nation(){Id=1,Name="汉"},
            new Nation(){Id=2,Name="其他"}
        };

        public List<City> MyCity
        {
            get { return cs; }
            set { cs = value; }
        }

        public List<Nationality> MyNationality
        {
            get { return ns; }
            set { ns = value; }
        }

        private List<Person> list = null;


        public MainWindow()
        {
            //预加载静态资源
            this.Resources.Add("MyNation", _nations);
            //使用xaml方式加载的的静态资源 无法被修改 只能预置在代码中

            InitializeComponent();
            Init();
        }

        void Init()
        {
            list = new List<Person>() { 
                new Person() { Id=2, Name = "Ha", Sex = Sex.Female, Nationality =new Nationality(){Id=1,Ch="b",En="b" },
                    City=new City(){Id=1,NationalityId=1,Name="衡阳"},Nation=_nations[1] },
                new Person() { Id=2, Name = "Hb", Sex = Sex.Male, Nationality = ns[1] ,City=new City(){Id=3,NationalityId=2,Name="密西西比"},Nation=_nations[1]} ,
                new Person() { Id=3, Name = "Hc", Sex = Sex.Male, Nationality = ns[2] ,City=new City(){Id=5,NationalityId=3,Name="柏林"},Nation=_nations[1]} 
            };
            this.PersonDataGrid.ItemsSource = list;
        }

        private void Tt_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            Int32 nId = ((sender as ComboBox).SelectedItem as City).NationalityId;
            (sender as ComboBox).Items.Filter = it =>
            {
                City temp = it as City;
                if (temp.NationalityId == nId) return true;
                else return false;
            };
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            (sender as ComboBox).Items.Filter = null;
            City selectedCity = (sender as ComboBox).SelectedItem as City;
            Person selectedPerson = PersonDataGrid.SelectedItem as Person;
            selectedPerson.City = selectedCity;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PersonDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Person
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public Sex Sex { get; set; }
        public Nation Nation { get; set; }
        public Nationality Nationality { get; set; }
        public City City { get; set; }
    }

    public enum Sex
    {
        Male,Female
    }

    public class Nation
    {
        public Int32 Id{get;set;}
        public String Name{get;set;}
    }

    public class Nationality
    {
        public Int32 Id { get; set; }
        public String Ch { get; set; }
        public String En { get; set; }
    }

    public class City
    {
        public Int32 NationalityId { get; set; }
        public Int32 Id { get; set; }
        public String Name { get; set; }
    }
}
