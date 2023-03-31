using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
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
        private Thread thread;

        public TrackingPage(DB.Login authorization,MenuWindows menuWindows)
        {
            InitializeComponent();
            dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin));
            this.authorization = authorization;
            this.menuWindows = menuWindows;
            if (!authorization.IdEmployee.Equals(null))
            {
                List<EmployeePost> employees = new List<EmployeePost>();
                for (int i = 0; i < context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(authorization.IdEmployee)).Count(); i++)
                {
                    employees.Add(context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(authorization.IdEmployee)).ElementAtOrDefault(i));
                    if (employees[i].IdPost.Equals(3))
                    {
                        MIEmployee.Visibility = Visibility.Visible;
                        MIClient.Visibility = Visibility.Collapsed;
                    }
                }
            }
            thread = new Thread(() =>
            {
                while (true)
                {
                    try { 
                    Dispatcher.Invoke(() => MITimer.Header = (DateTime.Now - context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin)).Last().DateStart));
                    Dispatcher.Invoke(() => MI.Header = DateTime.Now.ToString());
                    Thread.Sleep(1000);}
                    catch(System.Threading.Tasks.TaskCanceledException) {
                        thread.Abort();
                    }
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
        private void MenuItemAdminWindow_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.OpenAdminPage(authorization);
        }
        private void MenuItemClientWindow_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.GetIndex() <= context.Tracking.Count() - 1) {
                if (context.Tracking.ToList().Where(i=>i.Login.Equals(authorization.Login1)).ElementAtOrDefault(e.Row.GetIndex()).Error != "Closed") {  
                    e.Row.Background= new SolidColorBrush(Colors.Red);}}
        }
    }
    
}
