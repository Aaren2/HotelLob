using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
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
        private Thread thread;

        public ClientPage(DB.Login authorization, MenuWindows menuWindows)
        {
            InitializeComponent();
            this.authorization = authorization;
            this.menuWindows = menuWindows;
            thread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        DateTime dateTimeStart = Convert.ToDateTime(context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin)).Last().DateStart.Hour + ":" + context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin)).Last().DateStart.Minute + ":" + context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin)).Last().DateStart.Second);
                        DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
                        string date = dateTimeNow - dateTimeStart + "";
                        Dispatcher.Invoke(() => MITimer.Header = date.Substring(0, 8));
                        Dispatcher.Invoke(() => MI.Header = DateTime.Now.ToString());
                        Thread.Sleep(1000);
                    }
                    catch (System.Threading.Tasks.TaskCanceledException)
                    {
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

        private void MenuItemWindow_Click(object sender, RoutedEventArgs e)
        {
            trackingBool = false;
            SearchOrder.Visibility = Visibility.Collapsed;
            SearchOrderRoom.Visibility = Visibility.Collapsed;
            SearchRoom.Visibility = Visibility.Collapsed;
            SearchTypeRoom.Visibility = Visibility.Collapsed;
            SearchEmployee.Visibility = Visibility.Collapsed;
            SearchClient.Visibility = Visibility.Collapsed;
            SearchCategory.Visibility = Visibility.Collapsed;
            SearchEmployeePost.Visibility = Visibility.Collapsed;
            SearchPost.Visibility = Visibility.Collapsed;
            SearchGender.Visibility = Visibility.Collapsed;
            SearchTracking.Visibility = Visibility.Collapsed;
            MenuItem menuItem = (MenuItem)sender;
            Header = "" + menuItem.Header;
            switch (menuItem.Header)
            {
                case "EmployeeWindow":
                    dataGrid1.ItemsSource = context.Employee.ToList();
                    SearchEmployee.Visibility = Visibility.Visible;
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
                    break;
                case "EmployeePostWindow":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList();
                    SearchEmployeePost.Visibility = Visibility.Visible;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[4].Visibility = Visibility.Collapsed;                   
                    break;
                case "PostWindow":
                    dataGrid1.ItemsSource = context.Post.ToList();
                    SearchPost.Visibility = Visibility.Visible;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;                    
                    break;
                case "CategoryWindow":
                    dataGrid1.ItemsSource = context.Category.ToList();
                    SearchCategory.Visibility = Visibility.Visible;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;                    
                    break;
                case "GenderWindow":
                    dataGrid1.ItemsSource = context.Gender.ToList();
                    SearchGender.Visibility = Visibility.Visible;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;                   
                    break;
                case "TrackingWindow":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i=>i.IdLogin.Equals(authorization.IdLogin));
                    SearchTracking.Visibility = Visibility.Visible;
                    dataGrid1.Columns[0].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;
                    trackingBool = true;
                    break;
                case "ClientWindow":
                    dataGrid1.ItemsSource = context.Client.ToList();
                    SearchClient.Visibility = Visibility.Visible;
                    dataGrid1.Columns[7].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[8].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[9].Visibility = Visibility.Collapsed;                   
                    break;
                case "OrderWindow":
                    dataGrid1.ItemsSource = context.Order.ToList();
                    SearchOrder.Visibility = Visibility.Visible;
                    dataGrid1.Columns[6].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[7].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[8].Visibility = Visibility.Collapsed;                   
                    break;
                case "OrderRoomWindow":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList();
                    SearchOrderRoom.Visibility = Visibility.Visible;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[4].Visibility = Visibility.Collapsed;                    
                    break;
                case "RoomWindow":
                    dataGrid1.ItemsSource = context.Room.ToList();
                    SearchRoom.Visibility = Visibility.Visible;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;                    
                    break;
                case "TypeRoomWindow":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList();
                    SearchTypeRoom.Visibility = Visibility.Visible;
                    dataGrid1.Columns[3].Visibility = Visibility.Collapsed;
                    dataGrid1.Columns[2].Visibility = Visibility.Collapsed;                    
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

        private void MenuItemSearch_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            switch (menuItem.Name)
            {
                //Order
                case "IdOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderSearch.Text)));
                    break;
                case "DateOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateOrder.Equals(Convert.ToDateTime(TbDateOrderSearch.Text)));
                    break;
                case "IdEmployeeOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdEmployee.Equals(Convert.ToInt32(TbIdEmployeeOrderSearch.Text)));
                    break;
                case "IdClientOrder":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.IdClient.Equals(Convert.ToInt32(TbIdClientOrderSearch.Text)));
                    break;
                case "DateStart":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateStart.Equals(Convert.ToDateTime(TbDateStartSearch.Text)));
                    break;
                case "DateEnd":
                    dataGrid1.ItemsSource = context.Order.ToList().Where(i => i.DateEnd.Equals(Convert.ToDateTime(TbDateEndSearch.Text)));
                    break;
                //OrderRoom
                case "IdOrderRoom":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdOrder.Equals(Convert.ToInt32(TbIdOrderRoomSearch.Text)));
                    break;
                case "IdRoomRoom":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList().Where(i => i.IdRoom.Equals(Convert.ToInt32(TbIdRoomRoomSearch.Text)));
                    break;
                //Room
                case "IdRoom":
                    dataGrid1.ItemsSource = context.Room.ToList().Where(i => i.IdRoom.Equals(Convert.ToInt32(TbIdRoomSearch.Text)));
                    break;
                case "IdTypeRoomRoom":
                    dataGrid1.ItemsSource = context.Room.ToList().Where(i => i.IdTypeRoom.Equals(Convert.ToInt32(TbIdTypeRoomRoomSearch.Text)));
                    break;
                //TypeRoom
                case "IdTypeRoom":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList().Where(i => i.IdTypeRoom.Equals(Convert.ToInt32(TbIdTypeRoomSearch.Text)));
                    break;
                case "Count":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList().Where(i => i.Count.Equals(Convert.ToInt32(TbCountSearch.Text)));
                    break;
                //Client
                case "IdClient":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.IdClient.Equals(Convert.ToInt32(TbIdClientSearch.Text)));
                    break;
                case "ClientFirstName":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.FirstName.Equals(TbClientFirstNameSearch.Text));
                    break;
                case "ClientLastName":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.LastName.Equals(TbClientLastNameSearch.Text));
                    break;
                case "ClientMiddleName":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.MiddleName.Equals(TbClientMiddleNameSearch.Text));
                    break;
                case "ClientPhone":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.Phone.Equals(Convert.ToInt32(TbClientPhoneSearch.Text)));
                    break;
                case "ClientEmail":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.Email.Equals(TbClientEmailSearch.Text));
                    break;
                case "IdGenderClient":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.IdGender.Equals(Convert.ToInt32(TbIdGenderClientSearch.Text)));
                    break;
                //Client
                case "IdEmployee":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.IdEmployee.Equals(Convert.ToInt32(TbIdEmployeeSearch.Text)));
                    break;
                case "EmployeeFirstName":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.FirstName.Equals(TbEmployeeFirstNameSearch.Text));
                    break;
                case "EmployeeLastName":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.LastName.Equals(TbEmployeeLastNameSearch.Text));
                    break;
                case "EmployeeMiddleName":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.MiddleName.Equals(TbEmployeeMiddleNameSearch.Text));
                    break;
                case "IdGenderEmployee":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.IdGender.Equals(TbIdGenderEmployeeSearch.Text));
                    break;
                case "DateOfBirthday":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.DateOfBirthday.Equals(Convert.ToDateTime(TbDateOfBirthdaySearch.Text)));
                    break;
                case "IdCategoryEmployee":
                    dataGrid1.ItemsSource = context.Employee.ToList().Where(i => i.IdCategory.Equals(Convert.ToInt32(TbIdCategoryEmployeeSearch.Text)));
                    break;
                //Gender
                case "IdGender":
                    dataGrid1.ItemsSource = context.Gender.ToList().Where(i => i.IdGender.Equals(Convert.ToInt32(TbIdGenderSearch.Text)));
                    break;
                case "Gender":
                    dataGrid1.ItemsSource = context.Gender.ToList().Where(i => i.Gender1.Equals(TbGenderSearch.Text));
                    break;
                //Category
                case "IdCategory":
                    dataGrid1.ItemsSource = context.Category.ToList().Where(i => i.IdCategory.Equals(Convert.ToInt32(TbIdGenderSearch.Text)));
                    break;
                case "NameCategory":
                    dataGrid1.ItemsSource = context.Category.ToList().Where(i => i.NameCategory.Equals(TbNameCategorySearch.Text));
                    break;
                case "Coefficient":
                    dataGrid1.ItemsSource = context.Category.ToList().Where(i => i.Coefficient.Equals(Convert.ToDecimal(TbCoefficientSearch.Text)));
                    break;
                //EmployeePost
                case "IdPosrPost":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList().Where(i => i.IdPost.Equals(Convert.ToInt32(TbIdPostPostSearch.Text)));
                    break;
                case "IdEmployeePost":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList().Where(i => i.IdEmployee.Equals(Convert.ToInt32(TbIdEmployeePostSearch.Text)));
                    break;
                //Post
                case "IdPost":
                    dataGrid1.ItemsSource = context.Post.ToList().Where(i => i.IdPost.Equals(Convert.ToInt32(TbIdPostSearch.Text)));
                    break;
                case "NamePost":
                    dataGrid1.ItemsSource = context.Post.ToList().Where(i => i.NamePost.Equals(TbNamePostSearch.Text));
                    break;
                case "CodePost":
                    dataGrid1.ItemsSource = context.Post.ToList().Where(i => i.CodePost.Equals(Convert.ToDecimal(TbCodePostSearch.Text)));
                    break;
                //Tracking
                case "IdTracking":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.IdTracking.Equals(Convert.ToInt32(TbIdTrackingSearch.Text)));
                    break;
                case "DateStartT":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.DateStart.Equals(Convert.ToDateTime(TbDateStartTSearch.Text)));
                    break;
                case "DateEndT":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.DateEnd.Equals(Convert.ToDateTime(TbDateEndTSearch.Text)));
                    break;
                case "Error":
                    dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.Error.Equals(TbErrorSearch.Text));
                    break;
                default:
                    break;
            }
        }
    }
}

