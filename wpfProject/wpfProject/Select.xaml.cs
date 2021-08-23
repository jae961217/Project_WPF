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

namespace wpfProject
{
    /// <summary>
    /// Select.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Select : Page
    {
        public Select()
        {
            InitializeComponent();
        }


        private void toMain(object sender, RoutedEventArgs e)////대한민국 지도 화면으로 이동
        {
            MainWindow main = new MainWindow();
            this.NavigationService.Navigate(main);
        }

        private void toGraph(object sender, RoutedEventArgs e)///그래프 화면으로 이동
        {
            Graph tograph = new Graph();
            this.NavigationService.Navigate(tograph);
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
        private void progExit(object sender, RoutedEventArgs e)//대한민국 지도 화면으로 이동
        {
            Application.Current.Shutdown();
        }
    }
}
