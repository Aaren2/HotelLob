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

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private DB.Login authorization;
        public int IdOrder = -1;
        public MenuWindows menuWindows;
        public AdminPage(DB.Login authorization, MenuWindows menuWindows)
        {
            InitializeComponent();
            dataGrid1.ItemsSource = context.Order.ToList();
            dataGrid2.ItemsSource = context.OrderRoom.ToList();
            this.authorization = authorization;
            this.menuWindows = menuWindows;
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
                context.Order.Remove(order);
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
            IdOrder = Convert.ToInt32(x?.Text);
        }

        private void MenuItemDateOrderOld_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderBy(i => i.DateOrder);
        }
        private void MenuItemDateOrderNew_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.ItemsSource = context.Order.ToList().OrderByDescending(i => i.DateOrder);
        }
        private void MenuItemDateStartrOld_Click(object sender, RoutedEventArgs e)
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
    }
}

