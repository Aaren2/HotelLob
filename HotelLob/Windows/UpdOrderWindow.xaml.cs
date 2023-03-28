using HotelLob.DB;
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
using System.Windows.Shapes;

using HotelLob.ClassHelper;
using HotelLob.DB;
using HotelLob.Windows;
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Windows
{
    /// <summary>
    /// Логика взаимодействия для UpdOrderWindow.xaml
    /// </summary>
    public partial class UpdOrderWindow : Window
    {
        private DB.Login authorization;
        private int IdOrder;
        private OrderRoom orderRoomCheck = new OrderRoom();
        public UpdOrderWindow()
        {
            InitializeComponent();
            CmbClient.ItemsSource = context.Client.ToList();
            CmbClient.DisplayMemberPath = "IdClient";
            CmbRoom.ItemsSource = context.Room.ToList();
            CmbRoom.DisplayMemberPath = "IdRoom";
            this.authorization = authorization;
            this.IdOrder = IdOrder;

            CmbClient.SelectedIndex = context.Order.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().IdClient - 1;
            CmbRoom.SelectedIndex = context.OrderRoom.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().IdRoom - 200;
            DPDS.SelectedDate = context.Order.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().DateStart;
            DPDE.SelectedDate = context.Order.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().DateEnd;

        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuWindows menuWindows = new MenuWindows(authorization);
            menuWindows.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
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
                order.IdOrder = this.IdOrder;
                order.DateOrder = context.Order.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().DateOrder;
                order.IdEmployee = context.Order.ToList().Where(i => i.IdOrder == this.IdOrder).FirstOrDefault().IdEmployee;
                order.IdClient = (CmbClient.SelectedItem as DB.Client).IdClient;
                order.DateStart = DPDS.SelectedDate.Value;
                order.DateEnd = DPDE.SelectedDate.Value;


                context.SaveChanges();

                OrderRoom orderRoom = new OrderRoom();
                orderRoom.IdOrder = order.IdOrder;
                orderRoom.IdRoom = (CmbRoom.SelectedItem as DB.Room).IdRoom;


                context.SaveChanges();

                MenuWindows menuWindows = new MenuWindows(authorization);
                menuWindows.Show();
                this.Close();
            }
        }

        private void CmbRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var authUser = context.OrderRoom.ToList().Where(i => i.IdRoom == (CmbRoom.SelectedItem as DB.Room).IdRoom).Last();
            if (authUser != null)
            {
                orderRoomCheck.IdOrder = context.OrderRoom.ToList().Where(i => i.IdRoom == (CmbRoom.SelectedItem as DB.Room).IdRoom).Last().IdOrder;
                TbDate.Text = Convert.ToString(context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateStart) + "--" + Convert.ToString(context.Order.ToList().Where(i => i.IdOrder == orderRoomCheck.IdOrder).Last().DateEnd);
            }
            else
            {
                TbDate.Text = "Заказов нет";
            }
        }
    }
}
