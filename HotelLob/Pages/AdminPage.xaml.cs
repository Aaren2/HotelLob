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
                    break;
                case "EmployeePostWindow":
                    dataGrid1.ItemsSource = context.EmployeePost.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    break;
                case "PostWindow":
                    dataGrid1.ItemsSource = context.Post.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "CategoryWindow":
                    dataGrid1.ItemsSource = context.Category.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "GenderWindow":
                    dataGrid1.ItemsSource = context.Gender.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "LoginWindow":
                    dataGrid1.ItemsSource = context.Login.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "TrackingWindow":
                    dataGrid1.ItemsSource = context.Tracking.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "ClientWindow":
                    dataGrid1.ItemsSource = context.Client.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    break;
                case "OrderWindow":
                    dataGrid1.ItemsSource = context.Order.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    break;
                case "OrderRoomWindow":
                    dataGrid1.ItemsSource = context.OrderRoom.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = true;
                    BtnDel.IsEnabled = true;
                    break;
                case "RoomWindow":
                    dataGrid1.ItemsSource = context.Room.ToList();
                    BtnAdd.IsEnabled = true;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
                    break;
                case "TypeRoomWindow":
                    dataGrid1.ItemsSource = context.TypeRoom.ToList();
                    BtnAdd.IsEnabled = false;
                    BtnUpd.IsEnabled = false;
                    BtnDel.IsEnabled = false;
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
                    AddUpdEmployeeWindow addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, false);
                    addUpdEmployeeWindow.Show();
                    menuWindows.Close();
                    break;
                case "EmployeePostWindow":
                    addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, false);
                    addUpdEmployeeWindow.Show();
                    menuWindows.Close();
                    break;
                case "ClientWindow":
                    AddUpdClientWindow addUpdClientWindow = new AddUpdClientWindow(authorization, Id, false);
                    addUpdClientWindow.Show();
                    menuWindows.Close();
                    break;
                case "OrderWindow":
                    AddUpdOrderWindow addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, false);
                    addUpdOrderWindow.Show();
                    menuWindows.Close();
                    break;
                case "OrderRoomWindow":
                    addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, false);
                    addUpdOrderWindow.Show();
                    menuWindows.Close();
                    break;
                case "RoomWindow":
                    AddUpdRoomWindow addUpdRoomWindow = new AddUpdRoomWindow(authorization, Id, false);
                    addUpdRoomWindow.Show();
                    menuWindows.Close();
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
                        AddUpdEmployeeWindow addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, true);
                        addUpdEmployeeWindow.Show();
                        menuWindows.Close();
                        break;
                    case "EmployeePostWindow":
                        addUpdEmployeeWindow = new AddUpdEmployeeWindow(authorization, Id, true);
                        addUpdEmployeeWindow.Show();
                        menuWindows.Close();
                        break;
                    case "ClientWindow":
                        AddUpdClientWindow addUpdClientWindow = new AddUpdClientWindow(authorization, Id, true);
                        addUpdClientWindow.Show();
                        menuWindows.Close();
                        break;
                    case "OrderWindow":
                        AddUpdOrderWindow addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, true);
                        addUpdOrderWindow.Show();
                        menuWindows.Close();
                        break;
                    case "OrderRoomWindow":
                        addUpdOrderWindow = new AddUpdOrderWindow(authorization, Id, true);
                        addUpdOrderWindow.Show();
                        menuWindows.Close();
                        break;
                    case "RoomWindow":
                        AddUpdRoomWindow addUpdRoomWindow = new AddUpdRoomWindow(authorization, Id, true);
                        addUpdRoomWindow.Show();
                        menuWindows.Close();
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
    }
}
