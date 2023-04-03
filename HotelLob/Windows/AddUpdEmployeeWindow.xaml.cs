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
using System.Security.Policy;
using System.Reflection.Emit;

namespace HotelLob.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUpdEmployeeWindow.xaml
    /// </summary>
    public partial class AddUpdEmployeeWindow : Window
    {
        private bool True;
        private DB.Login authorization;
        private int IdEmployee;
        private Employee EmployeeCheck = new Employee();
        private MenuWindows menuWindows;
        public AddUpdEmployeeWindow(DB.Login authorization, int IdEmployee,bool True,  MenuWindows menuWindows)
        {
            InitializeComponent();
            CmbGender.ItemsSource = context.Gender.ToList();
            CmbGender.DisplayMemberPath = "Gender1";
            CmbGender.SelectedIndex = 0;
            CmbCategory.ItemsSource = context.Category.ToList();
            CmbCategory.DisplayMemberPath = "NameCategory";
            this.authorization = authorization;
            this.True = True;
            this.IdEmployee = IdEmployee;
            this.menuWindows =  menuWindows;

            if (True)
            {
                BtnLogin.Content = "Обновить";
                TbFirstName.Text = context.Employee.ToList().Where(i=>i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().FirstName;
                TbLastName.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().LastName;
                TbMidlleName.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().MiddleName;
                TbPhone.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().Phone;
                TbEmail.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().Email; 
                if(context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().IdGender=="м"){ 
                    CmbGender.SelectedIndex = 1;
                }
                else if (context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().IdGender == "ж")
                {
                    CmbGender.SelectedIndex = 0;
                }
                TbPassportCode.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().PassportCode;              
                TbPassportSeries.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().PassportSeries;
                TbSalary.Text = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().Salary+"";
                DPB.SelectedDate = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().DateOfBirthday;
                CmbCategory.SelectedIndex = context.Employee.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().IdCategory-1;
                TbLogin.Text= context.Login.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().Login1;
                TbPassword1.Text = context.Login.ToList().Where(i => i.IdEmployee.Equals(IdEmployee)).FirstOrDefault().Password;
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
                if (authUser1 != null && authUser1.IdEmployee != IdEmployee)
                {
                    MessageBox.Show("Такой логин занят");
                    return;
                }



                else
                {
                    Employee employee = new Employee();
                    employee.IdEmployee = this.IdEmployee;
                    employee.FirstName = TbFirstName.Text;
                    employee.LastName = TbLastName.Text;
                    employee.MiddleName = TbMidlleName.Text;
                    employee.Phone = TbPhone.Text;
                    employee.Email = TbEmail.Text;
                    employee.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;
                    employee.PassportCode = TbPassportCode.Text;
                    employee.PassportSeries = TbPassportSeries.Text;
                    employee.Salary = Convert.ToDecimal(TbSalary.Text);
                    employee.DateOfBirthday = DPB.SelectedDate.Value;
                    employee.IdCategory = (CmbCategory.SelectedItem as DB.Category).IdCategory;
                    context.SaveChanges();
                    Login login = new Login();
                    login.IdEmployee = this.IdEmployee;
                    login.Login1=TbLogin.Text;
                    login.Password=TbPassword1.Text;
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
                    Employee employee = new Employee();
                    employee.FirstName = TbFirstName.Text;
                    employee.LastName = TbLastName.Text;
                    employee.MiddleName = TbMidlleName.Text;
                    employee.Phone = TbPhone.Text;
                    employee.Email = TbEmail.Text;
                    employee.IdGender = (CmbGender.SelectedItem as DB.Gender).IdGender;
                    employee.PassportCode = TbPassportCode.Text;
                    employee.PassportSeries = TbPassportSeries.Text;
                    employee.Salary = Convert.ToDecimal(TbSalary.Text);
                    employee.DateOfBirthday = DPB.SelectedDate.Value;
                    employee.IdCategory = (CmbCategory.SelectedItem as DB.Category).IdCategory;
                    context.Employee.Add(employee);
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
