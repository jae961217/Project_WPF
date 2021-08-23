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
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Page
    {
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";

        public MainWindow()
        {
            
            InitializeComponent();
            initdata();

        }
        private void toSearch(object sender, RoutedEventArgs e)//서칭 페이지로 이동
        {
            Search tosearch = new Search();
            this.NavigationService.Navigate(tosearch);
        }
        private void toGraph(object sender, RoutedEventArgs e)// 그래프 페이지로 이동
        {

            Graph tograph = new Graph();
            this.NavigationService.Navigate(tograph);

        }
        private void toMain(object sender, RoutedEventArgs e)//대한민국 지도 화면으로 이동
        {
            MainWindow main = new MainWindow();
            this.NavigationService.Navigate(main);
        }


        private void toHome(object sender, RoutedEventArgs e)// 그래프 페이지로 이동
        {

            Select tosel = new Select();
            this.NavigationService.Navigate(tosel);

        }
        private void progExit(object sender, RoutedEventArgs e)//대한민국 지도 화면으로 이동
        {
            Application.Current.Shutdown();
        }
        public class bridge// 교량 전체정보 데이터 타입
        {
            public ObjectId _id;
            public string number;
            public string facilityName;
            public string address;
            public string bridgeFormat;
            public string bridgeLength;
            public string roadNum;
            public string spanNum;
            public string maxSpanNum;
            public string city;
            public string province;
            public string bridgeType;
        }
        public class bridgedata//
        {
            public string 도시{ get; set; }
            public string 교량개수 { get; set; }
        }
        [BsonIgnoreExtraElements]
        public class provdata//도 별 데이터 구조
        {
            [BsonId]
            ObjectId _id { get; set; }
            public string provName { get; set; }
            public string brNum { get; set; }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)//경기도 세부로 이동
        {
            //Gyeonggi gyeonggi = new Gyeonggi();
            //this.NavigationService.Navigate(gyeonggi); ;

            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/경기도.png,경기도");
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)//강원도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/강원도.png,강원도");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//충청남도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/충남.png,충청남도");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//충청북도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/충북.png,충청북도");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)//경상북도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/경북.png,경상북도");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//경상남도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/경남.png,경상남도");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//전라북도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/전라북도.png,전라북도");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)//전라남도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/전라남도.png,전라남도");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)//제주도 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/제주도.png,제주특별자치도");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)//서울시 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/서울.png,서울특별시");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)//인천 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/인천.png,인천광역시");
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)//광주 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/광주.png,광주광역시");
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)//부산 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/부산.png,부산광역시");
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)//울산 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/울산.png,울산광역시");
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)//대구 세부로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/대구.png,대구광역시");
        }

        

        private void toSejong(object sender, RoutedEventArgs e)//세종시로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/세종.png,세종특별자치시");
        }

        private void toDajeon(object sender, RoutedEventArgs e)//대전시로 이동
        {
            Region region = new Region();
            region.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(region, "/image/대전.png,대전광역시");
        }

        

        private void initdata()//데이터베이스에서 도별 교량 개수 불러와서 리스트로 넘겨주기
        {
            var client = new MongoClient(MongoDBConnectionString);
            var db = client.GetDatabase("wpf");
            var collection = db.GetCollection<provdata>("provinceData");

            List<provdata> provlist = new List<provdata>();
            var document = collection.Find(_ => true).ToList();

            foreach(var data in document)
            {
                provlist.Add(new provdata() { provName = data.provName, brNum = data.brNum });
            }

            korea.ItemsSource = provlist;

        }

        
    }
}
