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
    /// Логика взаимодействия для RegistrationWindow2.xaml
    /// </summary>
    public partial class RegistrationWindow2 : Window
    {
        public RegistrationWindow2()
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.SelectedIndex = 0;
            CmbGender.DisplayMemberPath = "Gender1";
        }

        private void TextBlock_MouseLeftButtonUpAuth(object sender, MouseButtonEventArgs e)
        {
            AutorizationWindow authorizationWindow = new AutorizationWindow();
            authorizationWindow.Show();
            this.Close();

        }

        private void TextBlock_MouseLeftButtonUpReg(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow1 regWindow = new RegistrationWindow1();
            regWindow.Show();
            this.Close();
        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
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


            if (string.IsNullOrWhiteSpace(TbPassportCode.Text))
            {
                MessageBox.Show("Паспорт не может быть пустым");
                return;
            }
            bool result1 = Int64.TryParse(TbPassportCode.Text, out var number1);
            if (result != true)
            {
                MessageBox.Show("Паспорт должен быть заполнен числами");
                return;
            }
            if (string.IsNullOrWhiteSpace(TbPassportSeries.Text))
            {
                MessageBox.Show("Паспорт не может быть пустым");
                return;
            }
            bool result2 = Int64.TryParse(TbPassportSeries.Text, out var number2);
            if (result != true)
            {
                MessageBox.Show("Паспорт должен быть заполнен числами");
                return;
            }


            if (string.IsNullOrWhiteSpace(DPB.Text))
            {
                MessageBox.Show("Не введена дата рождения");
                return;
            }

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


            if (string.IsNullOrWhiteSpace(PbCode.Password))
            {
                MessageBox.Show("Код сотрудника не может быть пустым");
                return;
            }
            var authUser = context.Post.ToList().Where(i => i.CodePost == PbCode.Password).FirstOrDefault();
            if (authUser == null)
            {
                MessageBox.Show("Неверный код сотрудника");
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


                DB.Employee employee = new DB.Employee();
                employee.FirstName = TbFirstName.Text;
                employee.LastName = TbLastName.Text;
                employee.MiddleName = TbMidlleName.Text;
                employee.Phone = TbPhone.Text;
                employee.Email = TbEmail.Text;
                employee.PassportCode = TbPassportCode.Text;
                employee.PassportSeries = TbPassportSeries.Text;
                employee.DateOfBirthday = DPB.SelectedDate.Value;
                employee.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;
                context.Employee.Add(employee);

                context.SaveChanges();

                DB.EmployeePost employeePost = new DB.EmployeePost();
                employeePost.IdPost = (context.Post.ToList().Where(i => i.CodePost == PbCode.Password).FirstOrDefault()).IdPost;
                employeePost.IdEmployee = (context.Employee.ToList().Where(i => i.FirstName == TbFirstName.Text && i.LastName == TbLastName.Text && i.MiddleName == TbMidlleName.Text).Last()).IdEmployee;
                context.EmployeePost.Add(employeePost);

                context.SaveChanges();

                DB.Login authorization = new DB.Login();
                authorization.Login1 = TbLogin.Text;
                authorization.Password = TbPassword1.Password;
                authorization.IdEmployee = employee.IdEmployee;
                context.Login.Add(authorization);

                context.SaveChanges();
            }





        }

    }
}
