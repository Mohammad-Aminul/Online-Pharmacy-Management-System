using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using MySql.Data.MySqlClient;
using System.Data;
namespace wpftest
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        public Admin()
        {
            InitializeComponent();
            timer_function();
          
        }

        void timer_function()
        {
            System.Windows.Threading.DispatcherTimer dispathertimer = new System.Windows.Threading.DispatcherTimer();
            dispathertimer.Tick += new EventHandler(timer_Tick);
            dispathertimer.Interval = new TimeSpan(0, 0, 1);
            dispathertimer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_reenter_new_password.Password))
            {
                if (this.txt_new_password.Password == this.txt_reenter_new_password.Password)
                {
                    //tbl_match.Foreground = new SolidColorBrush(Colors.Green);
                    tbl_match.Foreground = Brushes.Green;
                    tbl_match.Content = "match";
                }
                else
                {
                    tbl_match.Foreground = new SolidColorBrush(Colors.Red);
                    tbl_match.Content = "Not match";
                }
            }
        }
        private void Btn_clear_Click_1(object sender, RoutedEventArgs e) //clear button
        {
            txt_current_username.Clear();
            txt_new_username.Clear();
            txt_current_password.Clear();
            txt_new_password.Clear();
            txt_reenter_new_password.Clear();
        }

       public void insert_into_tbl_admin()
        {
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
            mysqlcon.Open();
            string query = "insert into tbl_admin values('" + this.txt_new_username.Text + "','" + this.txt_new_password.Password + "')";
            MySqlCommand mysqlcommd = new MySqlCommand(query, mysqlcon);
            mysqlcommd.ExecuteNonQuery();
            mysqlcon.Close();
            MessageBox.Show("Your username and password has been changed successfully.");
            this.Close();
        }

       public void delete_from_tbl_admin()
       {
           MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
           mysqlcon.Open();
           string query = "DELETE FROM tbl_admin WHERE username='"+this.txt_current_username.Text+"'";
           MySqlCommand mysqlcommd = new MySqlCommand(query, mysqlcon);
           mysqlcommd.ExecuteNonQuery();
           mysqlcon.Close();
       }

        private void btn_submit_click2(object sender, RoutedEventArgs e)
        {
            try
            {

                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_admin where username='" + this.txt_current_username.Text + "' and password='" + this.txt_current_password.Password + "'";
                MySqlCommand mysqlcommd = new MySqlCommand(query, mysqlcon);
                mysqlcommd.ExecuteNonQuery();
                MySqlDataReader datareader = mysqlcommd.ExecuteReader();

                if (datareader.Read())
                {
                    string un = datareader.GetString(0);
                    string pass = datareader.GetString(1);
                    delete_from_tbl_admin();
                    insert_into_tbl_admin();
                    //MessageBox.Show(un + pass);
                    mysqlcon.Close();
                }
                else
                    MessageBox.Show("Current username or password is not currect");
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
         
        }

    }
}
