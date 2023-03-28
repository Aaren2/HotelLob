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
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Windows
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
        }
        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationWindow1 regWindow = new RegistrationWindow1();
            regWindow.Show();
            this.Close();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var authUser = context.Login.ToList().Where(i => i.Password == TbPassword.Password && i.Login1 == TbLogin.Text).FirstOrDefault();
            DB.Login authorization = new DB.Login();
            authorization = (context.Login.ToList().Where(i => i.Password == TbPassword.Password && i.Login1 == TbLogin.Text).FirstOrDefault());

            if (authUser != null)
            {
                MenuWindows menuWindow = new MenuWindows(authorization);
                menuWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("no");
            }
        }
    }
}
