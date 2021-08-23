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
using System.Collections.ObjectModel;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Shared;
using MongoDB.Bson.Serialization.Attributes;
namespace wpfProject
{
    /// <summary>
    /// kind.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Graph : Page
    {
        public Graph()
        {
            InitializeComponent();
            bridgeCollection a= new bridgeCollection();
            numOfType.ItemsSource = a.brList;
        }

        private void toRegion(object sender, RoutedEventArgs e)
        {
            MainWindow tomain = new MainWindow();
            this.NavigationService.Navigate(tomain);
        }

        private void toGraph(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();
            this.NavigationService.Navigate(graph);
        }
        private void toSearch(object sender, RoutedEventArgs e)//서칭 화면으로 이동
        {
            Search search = new Search();
            this.NavigationService.Navigate(search);
        }

        private void toHome(object sender, RoutedEventArgs e)// 그래프 페이지로 이동
        {

            Select tosel = new Select();
            this.NavigationService.Navigate(tosel);

        }
        

        private void progExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void numOfType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var cellInfo = numOfCity.SelectedCells[0];
            //var content = cellInfo.Column.GetCellContent(cellInfo.Item);

            //Console.WriteLine(content.ToString());
            bridgedata dat = numOfType.SelectedItem as bridgedata;
            string s = dat.type;


            Graph_list tographlist = new Graph_list();
            tographlist.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(tographlist, s);
        }
        public class doBridge
        {
            public string brType { get; set; }
            public Int16 brNum { get; set; }
        }
    }
    class bridgeCollection : System.Collections.ObjectModel.Collection<bridgedata>
    {
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        public List<bridgedata> brList = new List<bridgedata>();
        public bridgeCollection()
        {
            init();
        }
        public void init()
        {
            var client = new MongoClient(MongoDBConnectionString);
            var db = client.GetDatabase("wpf");
            var collection = db.GetCollection<bridgedata>("bridgeTypeData");
           

            //FilterDefinition<bridgedata> filter = Builders<bridgedata>.Filter.Eq("fdsafdsa","fsdf");//도 개수
            var document = collection.Find(_=> true).ToList();
          
          
            foreach (var data in document)
            {
                Console.WriteLine(data.type);
                brList.Add(new bridgedata() { type = data.type, brNum = data.brNum });
            }
            foreach (var data in brList)
            {
                string tmp = " (" + data.brNum + ")";
                Add(new bridgedata { type = data.type+tmp, brNum = data.brNum });
            }

        }
    }


    class numdata
    {
        public string Name { get; set; }
        public int Number { get; set; }
    }



    //[BsonIgnoreExtraElements]
    //public class bridge
    //{
    //    [BsonId]
    //    public ObjectId _id { get; set; }
    //    public string number { get; set; }
    //    public string facilityName { get; set; }
    //    public string address { get; set; }
    //    public string bridgeFormat { get; set; }
    //    public string bridgeLength { get; set; }
    //    public string roadNum { get; set; }
    //    public string spanNum { get; set; }
    //    public string maxSpanNum { get; set; }
    //    public string city { get; set; }
    //    public string province { get; set; }
    //    public string bridgeType { get; set; }

    //    public override string ToString()
    //    {
    //        //return er + ":" + facilityName + " :" + address + " " + bridgeFormat + " " + bridgeLength + " " + roadNum + " "
    //        //    + numbspanNum + ":" + maxSpanNum + " " + city + " " + province + " " + bridgeType;
    //        return bridgeType;
    //    }

    //}


    [BsonIgnoreExtraElements]
    public class bridgedata
    {
        public string type { get; set; }
        public int brNum { get; set; }
    }

    //private void toBack(object sender, RoutedEventArgs e)
    //{
    //    MainWindow back = new MainWindow();
    //    this.NavigationService.Navigate(back);
    //}

    //private void toDodsee(object sender, RoutedEventArgs e)
    //{
    //    MessageBox.Show("hi");
    //}



}
