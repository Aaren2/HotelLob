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

using HotelLob.ClassHelper;
using HotelLob.DB;
using HotelLob.Windows;
using static HotelLob.ClassHelper.EFClass;
using System.Reflection;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public int Id = -1;
        private int Index=-1;
        public string Header;
        private DB.Login authorization;
        private MenuWindows menuWindows;
        private bool trackingBool=false;
        public AdminPage(DB.Login authorization, MenuWindows menuWindows)
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
            Header = ""+menuItem.Header;
            switch (menuItem.Header) {
                case "EmployeeWindow":
                    dataGrid1.ItemsSource = context.Employee.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    trackingBool = false;
                    break;
                case "EmployeePostWindow":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    trackingBool = false;
                    break;
                case "PostWindow":
                    dataGrid1.ItemsSource = context.Post.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                case "CategoryWindow":
                    dataGrid1.ItemsSource = context.Category.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                case "GenderWindow":
                    dataGrid1.ItemsSource = context.Gender.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                case "LoginWindow":
                    dataGrid1.ItemsSource = context.Login.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                case "TrackingWindow":
                    dataGrid1.ItemsSource = context.Tracking.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = true;
                    break;
                case "ClientWindow":
                    dataGrid1.ItemsSource = context.Client.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    trackingBool = false;
                    break;
                case "OrderWindow":
                    dataGrid1.ItemsSource = context.Order.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    trackingBool = false;
                    break;
                case "OrderRoomWindow":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    trackingBool = false;
                    break;
                case "RoomWindow":
                    dataGrid1.ItemsSource = context.Room.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                case "TypeRoomWindow":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    trackingBool = false;
                    break;
                default:
                    break;
            }


        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            switch (Header)
            {
                case "EmployeeWindow":
                    AddUpdEmployeeWindow addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, false,menuWindows);
                    addUpdEmployeeWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "EmployeePostWindow":
                    addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, false, menuWindows);
                    addUpdEmployeeWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "ClientWindow":
                    AddUpdClientWindow addUpdClientWindow = new AddUpdClientWindow(authorization, Id, false, menuWindows);
                    addUpdClientWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "OrderWindow":
                    AddUpdOrderWindow addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, false, menuWindows);
                    addUpdOrderWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "OrderRoomWindow":
                    addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, false, menuWindows);
                    addUpdOrderWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "RoomWindow":
                    AddUpdRoomWindow addUpdRoomWindow = new AddUpdRoomWindow(authorization, Id, false, menuWindows);
                    addUpdRoomWindow.Show();
                    menuWindows.Visibility=Visibility.Hidden;
                    break;
                case "TypeRoomWindow":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList();
                    break;
                default:
                    break;
            }
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (Id != -1)
            {
                switch (Header)
                {
                    case "EmployeeWindow":
                        AddUpdEmployeeWindow addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, true, menuWindows);
                        addUpdEmployeeWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    case "EmployeePostWindow":
                        addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, true, menuWindows);
                        addUpdEmployeeWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    case "ClientWindow":
                        AddUpdClientWindow addUpdClientWindow = new AddUpdClientWindow(authorization, Id, true, menuWindows);
                        addUpdClientWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    case "OrderWindow":
                        AddUpdOrderWindow addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, true, menuWindows);
                        addUpdOrderWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    case "OrderRoomWindow":
                        addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, true, menuWindows);
                        addUpdOrderWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    case "RoomWindow":
                        AddUpdRoomWindow addUpdRoomWindow = new AddUpdRoomWindow(authorization, Id, true, menuWindows);
                        addUpdRoomWindow.Show();
                        menuWindows.Visibility=Visibility.Hidden;
                        break;
                    default:
                        break;
                }
            
            }
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (Id != -1)
            {
                switch (Header)
                {
                    case "EmployeeWindow":
                        Employee employee = context.Employee.First(i => i.IdEmployee.Equals(this.Id));
                        context.Employee.Remove(employee);
                        EmployeePost[] employeePosts = new EmployeePost[] { };
                        for (int i = 0; i < context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(this.Id)).Count(); i++)
                        {
                            employeePosts[i] = context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(this.Id)).ElementAtOrDefault(i);
                            context.EmployeePost.Remove(employeePosts[i]);
                        }
                        Login login = context.Login.First(i => i.IdEmployee.Equals(this.Id));
                        context.Login.Remove(login);
                        context.SaveChanges();
                        dataGrid1.ItemsSource = context.Employee.ToList();
                        break;
                    case "EmployeePostWindow":
                        employeePosts = new EmployeePost[] { };
                        if (context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(this.Id)).Count() == 1)
                        {
                            employee = context.Employee.First(i => i.IdEmployee.Equals(this.Id));
                            context.Employee.Remove(employee);
                            for (int i = 0; i < context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(this.Id)).Count(); i++)
                            {
                                employeePosts[i] = context.EmployeePost.ToList().Where(j => j.IdEmployee.Equals(this.Id)).ElementAtOrDefault(i);
                                context.EmployeePost.Remove(employeePosts[i]);
                            }
                            login = context.Login.First(i => i.IdEmployee.Equals(this.Id));
                            context.Login.Remove(login);
                        }
                        else {
                            TextBlock x = dataGrid1.Columns[1].GetCellContent(dataGrid1.Items[Index]) as TextBlock;
                            EmployeePost employeePost = context.EmployeePost.First(j => j.IdEmployee.Equals(Convert.ToInt32(x.Text)));
                            context.EmployeePost.Remove(employeePost);
                        }
                        context.SaveChanges();
                        dataGrid1.ItemsSource = context.Employee.ToList();
                        break;                  
                    case "ClientWindow":
                        Client client = context.Client.First(i => i.IdClient.Equals(this.Id));
                        context.Client.Remove(client);
                        login = context.Login.First(i => i.IdClient.Equals(this.Id));
                        context.Login.Remove(login);
                        context.SaveChanges();
                        dataGrid1.ItemsSource = context.Client.ToList();
                        break;
                    case "OrderWindow":
                        Order order = context.Order.First(i => i.IdOrder.Equals(this.Id));
                        OrderRoom orderRoom = context.OrderRoom.First(i => i.IdOrder.Equals(this.Id));
                        context.Order.Remove(order);
                        context.OrderRoom.Remove(orderRoom);
                        context.SaveChanges();
                        dataGrid1.ItemsSource = context.Order.ToList();
                        break;
                    case "OrderRoomWindow":
                        order = context.Order.First(i => i.IdOrder.Equals(this.Id));
                        orderRoom = context.OrderRoom.First(i => i.IdOrder.Equals(this.Id));
                        context.Order.Remove(order);
                        context.OrderRoom.Remove(orderRoom);
                        context.SaveChanges();
                        dataGrid1.ItemsSource = context.OrderRoom.ToList();
                        break;                    
                    default:
                        break;
                }
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
            else {
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
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.FirstName.Equals(Convert.ToInt32(TbClientFirstNameSearch.Text)));
                    break;
                case "ClientLastName":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.LastName.Equals(Convert.ToInt32(TbClientLastNameSearch.Text)));
                    break;
                case "ClientMiddleName":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.MiddleName.Equals(Convert.ToInt32(TbClientMiddleNameSearch.Text)));
                    break;
                case "IdTypeRoom":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.IdTypeRoom.Equals(Convert.ToInt32(TbIdTypeRoomSearch.Text)));
                    break;
                case "Count":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.Count.Equals(Convert.ToInt32(TbCountSearch.Text)));
                    break;
                case "IdTypeRoom":
                    dataGrid1.ItemsSource = context.Client.ToList().Where(i => i.IdTypeRoom.Equals(Convert.ToInt32(TbIdTypeRoomSearch.Text)));
                    break;











                default:
                    break;
            }
        }
    }
}
