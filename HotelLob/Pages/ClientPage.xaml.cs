using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
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

using HotelLob.ClassHelper;
using HotelLob.DB;
using HotelLob.Windows;
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>

    public partial class ClientPage : Page
    {
        public int Id = -1;
        private int Index = -1;
        public string Header;
        private DB.Login authorization;
        private MenuWindows menuWindows;
        private bool trackingBool = false;
        
        public ClientPage(DB.Login authorization, MenuWindows menuWindows)
        {
            InitializeComponent();
            this.authorization = authorization;
            this.menuWindows = menuWindows;
            


        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.Close();
        }

        private void MenuItemWindow_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Header = "" + menuItem.Header;
            switch (menuItem.Header)
            {
                case "EmployeeWindow":
                    dataGrid1.ItemsSource = context.Employee.ToList();
                    dataGrid1.Columns[4].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[5].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[7].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[8].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[11].Visibility = Visibility.Collapsed;

                    dataGrid1.Columns[12].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[13].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[14].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[15].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[16].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "EmployeePostWindow":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList();
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[4].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "PostWindow":
                    dataGrid1.ItemsSource = context.Post.ToList();
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "CategoryWindow":
                    dataGrid1.ItemsSource = context.Category.ToList();
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "GenderWindow":
                    dataGrid1.ItemsSource = context.Gender.ToList();
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "TrackingWindow":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i=>i.IdLogin.Equals(authorization.IdLogin));
                    dataGrid1.Columns[0].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    trackingBool = true;
                    break;
                case "ClientWindow":
                    dataGrid1.ItemsSource = context.Client.ToList();
                    dataGrid1.Columns[7].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[8].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[9].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "OrderWindow":
                    dataGrid1.ItemsSource = context.Order.ToList();
                    dataGrid1.Columns[6].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[7].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[8].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "OrderRoomWindow":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList();
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[4].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "RoomWindow":
                    dataGrid1.ItemsSource = context.Room.ToList();
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                case "TypeRoomWindow":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList();
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    trackingBool = false;
                    break;
                default:
                    break;
            }
        }
        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dataGrid1.SelectedIndex.Equals(-1))
            {
                Index = dataGrid1.SelectedIndex;
                TextBlock x = dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[Index]) as TextBlock;
                Id = Convert.ToInt32(x?.Text);
            }
        }
        private void dataGrid1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (trackingBool)
            {
                if (e.Row.GetIndex() <= context.Tracking.Count() - 1)
                {
                    if (context.Tracking.ToList().ElementAtOrDefault(e.Row.GetIndex()).Error != "Closed")
                    {
                        e.Row.Background = new SolidColorBrush(Colors.Red);
                    }
                }
            }
            else
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
            }
        }
    }
}
