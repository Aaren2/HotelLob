using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
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

using HotelLob.ClassHelper;
using HotelLob.DB;
using HotelLob.Windows;
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для TrackingPage.xaml
    /// </summary>
    public partial class TrackingPage : Page
    {
        private DB.Login authorization;
        private MenuWindows menuWindows;
        public TrackingPage(DB.Login authorization,MenuWindows menuWindows)
        {
            InitializeComponent();
            dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin));
            this.authorization = authorization;
            this.menuWindows = menuWindows;
            if (authorization.IdEmployee.Equals(null)) {
                MIEmployee.Visibility=Visibility.Collapsed;
                Exit.Visibility = Visibility.Visible;
            }
            Thread thread = new Thread(() =>
            {
                while (true)
                {             
                    Dispatcher.Invoke(() => MITimer.Header = (DateTime.Now - context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin)).Last().DateStart));
                    Dispatcher.Invoke(() => MI.Header = DateTime.Now.ToString());
                    Thread.Sleep(1000);
                }
            });
            thread.Start();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.Close();
        }
        private void MenuItemEmployeeWindow_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.OpenEmployeePage(authorization);

        }
    }
}
