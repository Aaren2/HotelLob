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
    /// Логика взаимодействия для RegistrationWindow1.xaml
    /// </summary>
    public partial class RegistrationWindow1 : Window
    {
        public RegistrationWindow1()
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.SelectedIndex = 0;
            CmbGender.DisplayMemberPath = "Gender1";
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AutorizationWindow authorizationWindow = new AutorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private void TextBlock_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow2 regpersWindow = new RegistrationWindow2();
            regpersWindow.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbLogin.Text))
            {
                MessageBox.Show("Логин не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPassword1.Password))
            {
                MessageBox.Show("Пароль не может быть пустым");
                return;
            }
            if (TbPassword1.Password != TbPassword2.Password)
            {
                MessageBox.Show("Пароли должны быть одинаковыми");
                return;
            }

            if (string.IsNullOrWhiteSpace(TbFirstName.Text))
            {
                MessageBox.Show("Фамилия не может быть пустым");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbLastName.Text))
            {
                MessageBox.Show("Имя не может быть пустым");
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
            var authUser = context.Login.ToList().Where(i => i.Login1 == TbLogin.Text).FirstOrDefault();
            if (authUser != null)
            {
                MessageBox.Show("Такой логин уже используется");
                return;
            }
            else
            {
                DB.Client client1 = new DB.Client();
                client1.FirstName = TbFirstName.Text;
                client1.LastName = TbLastName.Text;
                client1.MiddleName = TbMiddleName.Text;
                client1.Phone = TbPhone.Text;
                client1.Email = TbEmail.Text;

                client1.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;
                context.Client.Add(client1);

                context.SaveChanges();

                DB.Login authorization = new DB.Login();
                authorization.Login1 = TbLogin.Text;
                authorization.Password = TbPassword1.Password;
                authorization.IdClient = client1.IdClient;
                context.Login.Add(authorization);

                context.SaveChanges();
            }
        }
    }
}
