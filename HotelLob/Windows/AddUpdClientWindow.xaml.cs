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
    /// Логика взаимодействия для AddUpdClientWindow.xaml
    /// </summary>
    public partial class AddUpdClientWindow : Window
    {
        private bool True;
        private DB.Login authorization;
        private int IdClient;
        private Client ClientCheck = new Client();
        private MenuWindows menuWindows;
 
        public AddUpdClientWindow(DB.Login authorization, int IdClient,bool True, MenuWindows menuWimdows)
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.DisplayMemberPath = "Gender1";

            this.authorization = authorization;
            this.True = True;
            this.IdClient = IdClient;
            this.menuWindows= menuWimdows;

            if (True)
            {
                BtnLogin.Content = "Обновить";
                TbFirstName.Text = context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().FirstName;
                TbLastName.Text = context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().LastName;
                TbMiddleName.Text = context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().MiddleName;
                TbPhone.Text = context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().Phone;
                TbEmail.Text = context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().Email;
                if (context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().IdGender == "м")
                {
                    CmbGender.SelectedIndex = 1;
                }
                else if (context.Client.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().IdGender == "ж")
                {
                    CmbGender.SelectedIndex = 0;
                }
                TbLogin.Text = context.Login.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().Login1;
                TbPassword1.Text = context.Login.ToList().Where(i => i.IdClient.Equals(IdClient)).FirstOrDefault().Password;

            }

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
                if (string.IsNullOrWhiteSpace(TbFirstName.Text))
                {
                    MessageBox.Show("Имя не может быть пустым");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbLastName.Text))
                {
                    MessageBox.Show("Фамилия не может быть пустой");
                    return;
                }

                if (string.IsNullOrWhiteSpace(TbPhone.Text))
                {
                    MessageBox.Show("Телефон не может быть пустым");
                    return;
                }
                bool result = Int64.TryParse(TbPhone.Text, out var number);
                if (result != true)
                {
                    MessageBox.Show("Телефон должен быть заполнен числами");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbEmail.Text))
                {
                    MessageBox.Show("Почта не может быть пустой");
                    return;
                }


               

                if (string.IsNullOrWhiteSpace(TbLogin.Text))
                {
                    MessageBox.Show("Логин не может быть пустым");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbPassword1.Text))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return;
                }
                if (TbPassword1.Text != TbPassword2.Password)
                {
                    MessageBox.Show("Пароли должны быть одинаковыми");
                    return;
                }

                var authUser1 = context.Login.ToList().Where(i => i.Login1 == TbLogin.Text).FirstOrDefault();
                if (authUser1 != null && authUser1.IdClient != IdClient)
                {
                    MessageBox.Show("Такой логин занят");
                    return;
                }



                else
                {
                    Client Client = new Client();
                    Client.IdClient = this.IdClient;
                    Client.FirstName = TbFirstName.Text;
                    Client.LastName = TbLastName.Text;
                    Client.MiddleName = TbMiddleName.Text;
                    Client.Phone = TbPhone.Text;
                    Client.Email = TbEmail.Text;
                    Client.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;
                    context.SaveChanges();
                    Login login = new Login();
                    login.IdClient = this.IdClient;
                    login.Login1 = TbLogin.Text;
                    context.SaveChanges();
                    MenuWindows menuWindows = new MenuWindows(authorization);
                    menuWindows.Show();
                    this.Close();
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TbFirstName.Text))
                {
                    MessageBox.Show("Имя не может быть пустым");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbLastName.Text))
                {
                    MessageBox.Show("Фамилия не может быть пустой");
                    return;
                }

                if (string.IsNullOrWhiteSpace(TbPhone.Text))
                {
                    MessageBox.Show("Телефон не может быть пустым");
                    return;
                }
                bool result = Int64.TryParse(TbPhone.Text, out var number);
                if (result != true)
                {
                    MessageBox.Show("Телефон должен быть заполнен числами");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbEmail.Text))
                {
                    MessageBox.Show("Почта не может быть пустой");
                    return;
                }             
                if (string.IsNullOrWhiteSpace(TbLogin.Text))
                {
                    MessageBox.Show("Логин не может быть пустым");
                    return;
                }
                if (string.IsNullOrWhiteSpace(TbPassword1.Text))
                {
                    MessageBox.Show("Пароль не может быть пустым");
                    return;
                }
                if (TbPassword1.Text != TbPassword2.Password)
                {
                    MessageBox.Show("Пароли должны быть одинаковыми");
                    return;
                }

                var authUser1 = context.Login.ToList().Where(i => i.Login1 == TbLogin.Text).FirstOrDefault();
                if (authUser1 != null)
                {
                    MessageBox.Show("Такой логин занят");
                    return;
                }



                else
                {
                    Client Client = new Client();
                    Client.FirstName = TbFirstName.Text;
                    Client.LastName = TbLastName.Text;
                    Client.MiddleName = TbMiddleName.Text;
                    Client.Phone = TbPhone.Text;
                    Client.Email = TbEmail.Text;
                    Client.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;                  
                    context.Client.Add(Client);
                    context.SaveChanges();
                    Login login = new Login();
                    login.Login1 = TbLogin.Text;
                    login.Password = TbPassword1.Text;
                    context.Login.Add(login);
                    context.SaveChanges();
                    MenuWindows menuWindows = new MenuWindows(authorization);
                    menuWindows.Show();
                    this.Close();
                }
            }
        }
    }
}