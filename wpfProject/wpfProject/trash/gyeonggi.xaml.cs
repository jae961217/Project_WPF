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
    /// gyeonggi.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Gyeonggi : Page
    {
        public Gyeonggi()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("경기도");
        }

        private void toMain(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.NavigationService.Navigate(main);
        }
    }
}
