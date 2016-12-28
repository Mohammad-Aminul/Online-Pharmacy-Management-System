using System;
using System.Windows;
using System.Data;
using MySql.Data.MySqlClient;
using wpftest;

namespace wpftest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
        public MainWindow()
        {
            InitializeComponent();
        }

            
        private void btnlogin_Click(object sender, RoutedEventArgs e)
        {

            int count = 0;
            MySqlConnection mysql_connection = new MySqlConnection(ConnectionString);
           
            try
            {
                mysql_connection.Open();
                string query = "select * from tbl_admin where username='" + this.txtuname.Text + "' and password='" + this.txt_pass.Password + "' ";
                MySqlCommand mysql_command = new MySqlCommand(query, mysql_connection);
                mysql_command.ExecuteNonQuery();
                MySqlDataReader datareader = mysql_command.ExecuteReader();
                while(datareader.Read())
                {
                       count++;
                }
                if(count==1)
                {
                    windowsales winpur = new windowsales();
                        winpur.Show();
                        this.Close();
                }
                else
                {
                    MessageBox.Show("Username or Password is not correct.\nPlease Enter correct username and password.","Message",MessageBoxButton.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Server not found. Please Check your internet connection.","Message",MessageBoxButton.OK);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btn_quit_application_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
      
}
