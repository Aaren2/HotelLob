
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using HotelLob.Pages;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;



namespace HotelLob.Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuWindows.xaml
    /// </summary>
    public partial class MenuWindows : Window
    {
        private DB.Login authorization;
        private Tracking tracking = new Tracking();
        public MenuWindows(DB.Login authorization)
        {
            InitializeComponent();
            this.authorization = authorization;
            tracking.IdLogin = authorization.IdLogin;
            tracking.DateStart = DateTime.Now;
            context.Tracking.Add(tracking);
            context.SaveChanges();
            if (!authorization.IdEmployee.Equals(null))
            {
                MenuFrame.Content = new AdminPage(authorization, this);
            }
            else
            {
                OpenTrakingPage(authorization);
            }
        }
        public void OpenTrakingPage(DB.Login authorization) {
            MenuFrame.Content = new TrackingPage(authorization,this);
        }
        public void OpenAdminPage(DB.Login authorization) {
            MenuFrame.Content = new AdminPage(authorization,this);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            tracking.DateEnd = DateTime.Now;
            context.SaveChanges();
        }

    }
}
