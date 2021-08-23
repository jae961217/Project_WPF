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
    /// Test.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Region : Page
    {
        string tmp;
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        public Region()
        {
            InitializeComponent();

        }
        private void toMain(object sender, RoutedEventArgs e)//대한민국 지도 화면으로 이동
        {
            MainWindow main = new MainWindow();
            this.NavigationService.Navigate(main);
        }

        private void toGraph(object sender, RoutedEventArgs e)//그래프 화면으로 이동
        {
            Graph tograph = new Graph();
            this.NavigationService.Navigate(tograph);
        }

        private void toSearch(object sender, RoutedEventArgs e)////서칭 페이지로 이동
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

        /// <summary>
        /// 네비게이션
        /// </summary>
        /// <param name="navigation"></param>
        public void SetLoadCompleted(NavigationService navigation)
        {
            navigation.LoadCompleted += new LoadCompletedEventHandler(NavigationService_LoadCompleted);
        }

        void NavigationService_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (e.ExtraData != null)//네비게이션에서 넘겨온 값 가져오기
            {
                tmp = (string)e.ExtraData;
                char sp = ',';
                string[] substr = tmp.Split(sp);
                Uri resource = new Uri(substr[0], UriKind.Relative);
                region.Source = new BitmapImage(resource);

                doc.Content = substr[1] + "의 교량 정보입니다. 더 자세한 정보는 우측의 리스트를 클릭하세요";

                var client = new MongoClient(MongoDBConnectionString);
                var db = client.GetDatabase("wpf");
                var collection= db.GetCollection<bridge>("bridge");
                var str_len = substr[1].Length;
                char a = substr[1][str_len - 1];


                
                if (a == '도')//도시일때
                {
                    List<doBridge> bridges = new List<doBridge>();
                    List<bridgedata> brList = new List<bridgedata> ();
                   // List<tempdata> tmpdata = new List<tempdata>();

                    FilterDefinition<bridge> filter = Builders<bridge>.Filter.Eq("province", substr[1]);//도 개수
                    var document = collection.Find(filter).ToList();
                   
                    int tmpNum = 0;
                    string cityName="";
                    foreach (bridge aparts in document)
                    {

                        if (cityName == "")
                        {
                            cityName = aparts.ToString();
                            tmpNum = 1;
                        }
                        else if (cityName == aparts.ToString())
                        {
                            tmpNum++;
                        }
                        else
                        {
                            brList.Add(new bridgedata() {cityname=cityName,brNum=tmpNum});
                            cityName = aparts.ToString();
                            tmpNum = 1;
                        }
                    }
                    brList.Add(new bridgedata() { cityname = cityName, brNum = tmpNum });


                    foreach(var data in brList)
                    {
                        //mpdata.Add(new tempdata() { cityName = data.cityname });
                        bridges.Add(new doBridge() { 도시=data.cityname,교량개수=data.brNum });
                    }


                    //var tmpconnection = db.GetCollection<tempdata>(substr[1]);
                    //foreach(var data in tmpdata)//데이터 입력 코드
                    //{
                    //    tmpconnection.InsertOne(new tempdata() { cityName = data.cityName });
                    //}

                    numOfCity.ItemsSource = bridges;
                }
                else//특별시 광역시 
                {
                    List<doBridge> bridges = new List<doBridge>();
                    FilterDefinition<bridge> filter = Builders<bridge>.Filter.Eq("city", substr[1]);//도 개수
                    var sidoc = collection.Find(filter).CountDocuments();

                    
                    bridges.Add(new doBridge() { 도시 = substr[1], 교량개수 = Int32.Parse(sidoc.ToString())});

                    numOfCity.ItemsSource =bridges;
                    
                }
               
            }
            this.NavigationService.LoadCompleted -= new LoadCompletedEventHandler(NavigationService_LoadCompleted);
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

      
        
        public class doBridge
        {
            public string 도시 { get; set; }
            public int 교량개수 { get; set; }
        }
        public class bridgedata
        {
            public string cityname { get; set; }
            public int brNum { get; set; }
        }
        //public class tempdata
        //{
        //    public string cityName { get; set; }
        //}

       

        private void numOfCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //var cellInfo = numOfCity.SelectedCells[0];
            //var content = cellInfo.Column.GetCellContent(cellInfo.Item);

            //Console.WriteLine(content.ToString());
            doBridge dat = numOfCity.SelectedItem as doBridge;
            string s = dat.도시;


            kind_list tokind = new kind_list();
            tokind.SetLoadCompleted(NavigationService);
            this.NavigationService.Navigate(tokind, s);
        }
       
    }
}
