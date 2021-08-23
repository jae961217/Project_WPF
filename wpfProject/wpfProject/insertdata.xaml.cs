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
    /// insertdata.xaml에 대한 상호 작용 논리
    /// </summary>
    

    public partial class insertdata : Page
    {
        const string MongoDBConnectionString = "mongodb://humba:1234@13.124.99.108:27017/wpf?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
        public insertdata()
        {
            InitializeComponent();
            var client = new MongoClient(MongoDBConnectionString);
            var db = client.GetDatabase("wpf");
            var collection = db.GetCollection<bridge>("bridgebyType");

            var document = collection.Find(_ => true).ToList();


            List<bridgetypedata> bridges = new List<bridgetypedata>();
            int cnt = 0;
            string s = "";
            foreach (var data in document)
            {
                if (s == "")
                {
                    s = data.ToString();
                    cnt = 1;
                }
                else if (s == data.ToString())
                {
                    cnt++;
                }
                else
                {
                    bridges.Add(new bridgetypedata() { type = s, brNum = cnt });
                    Console.WriteLine(s);
                    cnt = 1;
                    s = data.ToString();
                }
            }


            
          

            var insertcollection = db.GetCollection<bridgetypedata>("bridgeTypeData");
            foreach(var data in bridges)
            {
                insertcollection.InsertOne(new bridgetypedata() { type=data.type,brNum=data.brNum});
            }

        }

        public class provincedata
        {
            public string provName { get; set; }
            public string brNum { get; set; }
        }
        public class citydata
        {
            public string cityName { get; set; }
            public string provName { get; set; }
            public int brNum { get; set; }
        }
        public class bridgetypedata
        {
            public string type { get; set; }
            public int brNum { get; set; }
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
                return bridgeType;
            }

        }
    }
}
