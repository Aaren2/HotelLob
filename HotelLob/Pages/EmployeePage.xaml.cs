
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
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

using static HotelLob.ClassHelper.EFClass;
using HotelLob.DB;
using HotelLob.Windows;
using HotelLob.ClassHelper;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        private  DB.Login authorization;
        public int IdOrder = -1;
        public  MenuWindows menuWindows;
        public static DataGrid DataGrid1s;
        
        public EmployeePage(DB.Login authorization1, MenuWindows menuWindows1)
        {
            
            InitializeComponent();
            dataGrid1.ItemsSource = context.Order.ToList();
            dataGrid2.ItemsSource = context.OrderRoom.ToList();
            authorization = authorization1;
            menuWindows = menuWindows1;

            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddUpdOrderWindow addOrderWindow = new AddUpdOrderWindow(authorization,IdOrder,false,menuWindows);
            addOrderWindow.Show();
            menuWindows.Visibility = Visibility.Hidden;
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (IdOrder != -1)
            {
                AddUpdOrderWindow updOrderWindow = new AddUpdOrderWindow(authorization, IdOrder,true,menuWindows);
                updOrderWindow.Show();
                menuWindows.Visibility = Visibility.Hidden;
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (IdOrder != -1)
            {
                Order order = context.Order.First(i => i.IdOrder.Equals(this.IdOrder));
                OrderRoom orderRoom = context.OrderRoom.First(i => i.IdOrder.Equals(this.IdOrder));
                context.Order.Remove(order);
                context.OrderRoom.Remove(orderRoom);
                context.SaveChanges();
                dataGrid1.ItemsSource = context.Order.ToList();
                dataGrid2.ItemsSource = context.OrderRoom.ToList();
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dataGrid1.SelectedIndex.Equals(-1)) { 
            TextBlock x = dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[dataGrid1.SelectedIndex]) as TextBlock;
            IdOrder = Convert.ToInt32(x?.Text);}
        }



        private void MenuItemTrackingWindow_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.OpenTrackingPage(authorization);
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           menuWindows.Close();
        }
      
        private void MenuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Name)
            {
                case "IdOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderSearch.Text)));
                    break;
                case "DateOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateOrder.Equals(Convert.ToDateTime(TbDateOrderSearch.Text)));
                    break;
                case "IdEmployee":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdEmployee.Equals(Convert.ToInt32(TbIdEmployeeSearch.Text)));
                    break;
                case "IdClient":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdClient.Equals(Convert.ToInt32(TbIdClientSearch.Text)));
                    break;
                case "DateStart":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateStart.Equals(Convert.ToDateTime(TbDateStartSearch.Text)));
                    break;
                case "DateEnd":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateEnd.Equals(Convert.ToDateTime(TbDateEndSearch.Text)));
                    break;
                case "IdOrderRoom":
                    dataGrid2.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderRoomSearch.Text)));
                    break;
                case "IdRoomRoom":
                    dataGrid2.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdRoom.Equals(Convert.ToInt32(TbIdRoomRoomSearch.Text)));
                    break;
                default:
                    break;
            }
        }     
    }
}

