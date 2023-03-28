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
using static HotelLob.ClassHelper.EFClass;

namespace HotelLob.Pages
{
    /// <summary>
    /// Логика взаимодействия для TrackingPage.xaml
    /// </summary>
    public partial class TrackingPage : Page
    {
        private DB.Login authorization;
        public TrackingPage(DB.Login authorization)
        {
            InitializeComponent();
            dataGrid1.ItemsSource = context.Tracking.ToList().Where(i => i.IdLogin.Equals(authorization.IdLogin));
            this.authorization = authorization;
        }
    }
}
