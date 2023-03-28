using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

using HotelLob.ClassHelper;
using HotelLob.DB;
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private DB.Login authorization;
        private OrderRoom orderRoomCheck = new OrderRoom();

        public AddOrderWindow(DB.Login authorization)
        {
            InitializeComponent();
            CmbClient.ItemsSource = context.Client.ToList();
            CmbClient.SelectedIndex = 0;
            CmbClient.DisplayMemberPath = "IdClient";
            CmbRoom.ItemsSource = context.Room.ToList();
            CmbRoom.DisplayMemberPath = "IdRoom";
            this.authorization = authorization;
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuWindows menuWindows = new MenuWindows(authorization);
            menuWindows.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (DPDS.SelectedDate.Value < DateTime.Now)
            {
                MessageBox.Show("Дата начала отдыха указана не верно");
                return;
            }
            if (DPDS.SelectedDate.Value > DPDE.SelectedDate.Value)
            {
                MessageBox.Show("Дата окончания отдыха указана не верно");
                return;
            }
            if (DPDS.SelectedDate.Value < context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateEnd)
            {
                MessageBox.Show("Дата начала отдыха указана не верно");
                return;
            }
            if (DPDE.SelectedDate.Value < context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateEnd)
            {
                MessageBox.Show("Дата окончания отдыха указана не верно");
                return;
            }


            else
            {
                Order order = new Order();
                order.DateOrder = DateTime.Now;
                order.IdEmployee = authorization.IdEmployee;
                order.IdClient = (CmbClient.SelectedItem as DB.Client).IdClient;
                order.DateStart = DPDS.SelectedDate.Value;
                order.DateEnd = DPDE.SelectedDate.Value;
                context.Order.Add(order);

                context.SaveChanges();

                OrderRoom orderRoom = new OrderRoom();
                orderRoom.IdOrder = order.IdOrder;
                orderRoom.IdRoom = (CmbRoom.SelectedItem as DB.Room).IdRoom;
                context.OrderRoom.Add(orderRoom);

                context.SaveChanges();

                MenuWindows menuWindows = new MenuWindows(authorization);
                menuWindows.Show();
                this.Close();
            }
        }

        private void CmbRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var authUser = context.OrderRoom.ToList().Where(i => i.IdRoom == (CmbRoom.SelectedItem as DB.Room).IdRoom).Last();
                orderRoomCheck.IdOrder = context.OrderRoom.ToList().Where(i => i.IdRoom == (CmbRoom.SelectedItem as DB.Room).IdRoom).Last().IdOrder;
                TbDate.Text = Convert.ToString(context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateStart) + "--" + Convert.ToString(context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateEnd);
            }
            catch (InvalidOperationException) { TbDate.Text = "Заказов нет"; }
        }
    }
}
