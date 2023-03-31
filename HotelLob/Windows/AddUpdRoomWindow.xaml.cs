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
    /// Логика взаимодействия для AddUpdRoomWindow.xaml
    /// </summary>
    public partial class AddUpdRoomWindow : Window
    {
        private bool True;
        private DB.Login authorization;
        private int IdRoom;
        private Room RoomCheck = new Room();
        private MenuWindows menuWindows;

        public AddUpdRoomWindow(DB.Login authorization, int IdRoom,bool True, MenuWindows menuWindows)
        {
            InitializeComponent();
            CmbTypeRoom.ItemsSource = context.TypeRoom.ToList();
            CmbTypeRoom.DisplayMemberPath = "Count";

            this.authorization = authorization;
            this.True = True;
            this.IdRoom = IdRoom;
            this.menuWindows= menuWindows;
            if (True)
            {
                BtnLogin.Content = "Обновить";
                CmbTypeRoom.SelectedIndex = context.TypeRoom.ToList().Where(i => i.IdTypeRoom == this.IdRoom).FirstOrDefault().IdTypeRoom;

            }

            this.menuWindows = menuWindows;
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            menuWindows.Visibility = Visibility.Visible;
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (True)
            {
                Room Room = new Room();
                Room.IdRoom = this.IdRoom;
                Room.IdTypeRoom = (CmbTypeRoom.SelectedItem as DB.TypeRoom).IdTypeRoom;
                
                context.SaveChanges();
                
                MenuWindows menuWindows = new MenuWindows(authorization);
                menuWindows.Show();
                this.Close();               
            }
            else 
            {
                Room Room = new Room();
                Room.IdRoom = this.IdRoom;
                Room.IdTypeRoom = (CmbTypeRoom.SelectedItem as DB.TypeRoom).IdTypeRoom;
                context.Room.Add(Room);
                
                context.SaveChanges();

                MenuWindows menuWindows = new MenuWindows(authorization);
                menuWindows.Show();
                this.Close();
            }
        }
    }
}
