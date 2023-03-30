using HotelLob.DB;
using HotelLob.Windows;
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
using HotelLob.Windows;
using System.Net;

using HotelLob.ClassHelper;
using HotelLob.DB;
using System.Threading;
using System.Runtime.InteropServices;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private  DB.Login authorization;
        public int IdOrder = -1;
        public  MenuWindows menuWindows;
        public static DataGrid DataGrid1s;
        
        public AdminPage(DB.Login authorization1, MenuWindows menuWindows1)
        {
            
            InitializeComponent();
            dataGrid1.ItemsSource = context.Order.ToList();
            dataGrid2.ItemsSource = context.OrderRoom.ToList();
            authorization = authorization1;
            menuWindows = menuWindows1;

            
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow(authorization);
            addOrderWindow.Show();
            menuWindows.Close();
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (IdOrder != -1)
            {
                UpdOrderWindow updOrderWindow = new UpdOrderWindow(authorization, IdOrder);
                updOrderWindow.Show();
                menuWindows.Close();
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
            TextBlock x = dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[dataGrid1.SelectedIndex]) as TextBlock;
            IdOrder = Convert.ToInt32(x.Text);
        }



        private void MenuItemTrackingWindow_Click(object sender, RoutedEventArgs e)
        {
            menuWindows.OpenTrakingPage(authorization);
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
           menuWindows.Close();
        }



        private void MenuItemDateOrderOld_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderBy(i => i.DateOrder);
        }
        private void MenuItemDateOrderNew_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderByDescending(i => i.DateOrder);
        }

        
        private void MenuItemDateStartOld_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderBy(i => i.DateStart);
        }
        private void MenuItemDateStartNew_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderByDescending(i => i.DateStart);
        }


        private void MenuItemDateEndOld_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderBy(i => i.DateEnd);
        }
        private void MenuItemDateEndNew_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderByDescending(i => i.DateEnd);
        }
        private void MenuItemIdOrderSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderSerch.Text)));
            
        }
        private void MenuItemDateOrderSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateOrder.Equals(Convert.ToDateTime(TbDateOrderSerch.Text)));
            
        }

        private void MenuItemIdEmployeeSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdEmployee.Equals(Convert.ToInt32(TbIdEmployeeSerch.Text)));
        }


        private void MenuItemIdClientSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdClient.Equals(Convert.ToInt32(TbIdClientSerch.Text)));
            
        }

        private void MenuItemDateStartSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateStart.Equals(Convert.ToDateTime(TbDateStartSerch.Text)));
            
        }

        private void MenuItemDateEndSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateEnd.Equals(Convert.ToDateTime(TbDateEndSerch.Text)));
        }

        private void MenuItemIdOrderRoomSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid2.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderRoomSerch.Text)));
        }

        private void MenuItemIdRoomRoomSerch_Click(object sender, RoutedEventArgs e)
        {
            dataGrid2.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdRoom.Equals(Convert.ToInt32(TbIdRoomRoomSerch.Text)));
        }
    }
}

