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
using Newtonsoft.Json.Linq;


namespace wpfProject
{
    /// <summary>
    /// Search_list.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Search_list : Page
    {

        string tmp;
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        public Search_list()
        {
            InitializeComponent();
        }
        
        public void SetLoadCompleted(NavigationService navigation)
        {

            navigation.LoadCompleted += new LoadCompletedEventHandler(NavigationService_LoadCompleted);

        }

        void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData != null)
            {
                tmp = (string)e.ExtraData;
                char sp = ':';
                string[] substr = tmp.Split(sp);
                string text = substr[0] + " " + substr[1] + "의 " + substr[2] + " 타입의 교량( 최소연장: " + substr[3] + " 최대연장: " + substr[4] + " )";
                Searchtitle.Content = text;

                var client = new MongoClient(MongoDBConnectionString);
                var db = client.GetDatabase("wpf");
                var collection = db.GetCollection<bridge>("bridgebyType");


                if (substr[1] == "")
                {
                   
                }
                else
                {
                    FilterDefinition<bridge> filter = Builders<bridge>.Filter.Eq("city", substr[1]) 
                        & Builders<bridge>.Filter.Eq("province", substr[0]) 
                        & Builders<bridge>.Filter.Eq("bridgeType", substr[2]);
                    var document = collection.Find(filter).ToList();
                     

                    foreach (var data in document)
                    {
                        
                        string datastring = data.ToString();
                        char pp = ',';
                        string[] sub = datastring.Split(pp);
                        string len = sub[3];
                        if ((Int32.Parse(substr[3]) <= Double.Parse(len))
                            && (Int32.Parse(substr[4]) >= Double.Parse(len)))
                        {
                            DockPanel dp = new DockPanel();
                            Label lb = new Label();
                            lb.Tag = 0;
                            lb.Content = "교량명 : " + sub[0] + " 주소 : " + sub[1] + " 교량형식 : " + sub[2] + 
                                " 교량 연장 : " + sub[3] + " 차로수 : " + sub[4] + " 경간수 : " + sub[5];

                            dp.Children.Add(lb);
                            stackpanelList.Children.Add(dp);
                        }




                    }
                }
                



               
            }
            this.NavigationService.LoadCompleted -= new LoadCompletedEventHandler(NavigationService_LoadCompleted);
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

        private void toSearch(object sender, RoutedEventArgs e)
        {
            Search tosearch = new Search();
            this.NavigationService.Navigate(tosearch);

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
                return facilityName + "," + address + "," + bridgeFormat + "," + bridgeLength + "," + roadNum + ","
                    + spanNum + "," + maxSpanNum + "," + city + "," + province + "," + bridgeType;
            }

        }
    }
}
