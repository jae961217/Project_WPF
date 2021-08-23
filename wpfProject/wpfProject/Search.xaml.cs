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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Shared;
using MongoDB.Bson.Serialization.Attributes;

namespace wpfProject
{
    /// <summary>
    /// Search.xaml에 대한 상호 작용 논리
    /// </summary>
     
    public partial class Search : Page
    {
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        public Search()
        {
            InitializeComponent();
        }

        private void toRegion(object sender, RoutedEventArgs e)//지도 화면으로 이동
        {
            MainWindow main = new MainWindow();
            this.NavigationService.Navigate(main);
        }

        private void toGraph(object sender, RoutedEventArgs e)//그래프 화면으로 이동
        {
            Graph graph = new Graph();
            this.NavigationService.Navigate(graph);
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
        private void getProvince(object sender, SelectionChangedEventArgs e)
        {
            if (provCombo.SelectedIndex >= 0)
            {
                var item =(ComboBoxItem) provCombo.SelectedValue;
                string selected = item.Content.ToString();
                Console.WriteLine(selected);

                var client = new MongoClient(MongoDBConnectionString);
                var db = client.GetDatabase("wpf");
                var collection = db.GetCollection<provdata>(selected);
                var str_len = selected.Length;
                char a = selected[str_len - 1];


                cityCombo.Items.Clear();
                if (a == '도')
                {
                    var document = collection.Find(_ => true).ToList();
                    foreach(var data in document)
                    {
                        Console.WriteLine(data.cityName);
                        ComboBoxItem tmpItem = new ComboBoxItem();
                        tmpItem.Content = data.cityName;
                        cityCombo.Items.Add(tmpItem);
                    }


                }
            }
            //int sel = provCombo.SelectedIndex;
            //// string txt = provCombo.SelectedValue.ToString();
            //string txt = provCombo.Text;
            //string k = provCombo.Items[sel].ToString();
            //Console.WriteLine(txt);
            //ComboBoxItem a = new ComboBoxItem();
            //a.Content = k;
            ////cityCombo.Tag = 0;
            //cityCombo.Items.Add(a);
        }

        private void findBridge(object sender, RoutedEventArgs e)//교량 검색버튼 클릭시
        {
            string prov = provCombo.Text;
            string city = cityCombo.Text;
            string type = typeOfBr.Text;
           int minNum;
            if (min.Text == "")
            {
                minNum = 0;
            }
            else
            {
                minNum = int.Parse(min.Text);
            }
            int maxNum;
            if (max.Text == "")
            {
                maxNum = 11000;
            }
            else
            {
                maxNum = int.Parse(max.Text);
            }

            string s = prov + ":" + city + ":" + type + ":" + minNum.ToString() + ":" + maxNum.ToString();


            //MessageBox.Show(prov + ":" + city + ":" + type + ":" + minNum.ToString() + ":" + maxNum.ToString());
            Search_list tosearch = new Search_list();
            tosearch.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(tosearch, s);
        }
    }
    [BsonIgnoreExtraElements]
    class provdata
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string cityName { get; set; }
    }




    [BsonIgnoreExtraElements]
    public class bridge
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string number { get; set; }
        public string facilityName { get; set; }
        public string address { get; set; }
        public string bridgeFormat { get; set; }
        public string bridgeLength { get; set; }
        public string roadNum { get; set; }
        public string spanNum { get; set; }
        public string maxSpanNum { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string bridgeType { get; set; }

        public override string ToString()
        {
            //return er + ":" + facilityName + " :" + address + " " + bridgeFormat + " " + bridgeLength + " " + roadNum + " "
            //    + numbspanNum + ":" + maxSpanNum + " " + city + " " + province + " " + bridgeType;
            return city;
        }

    }
}
