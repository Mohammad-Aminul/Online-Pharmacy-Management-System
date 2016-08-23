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
    /// Interaction logic for sales_history.xaml
    /// </summary>
    public partial class sales_history : Window
    {
        string ConnectionString = @"server=localhost;Database=dbpharmacy;username='root';password=''";
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        string current_date;
        public sales_history()
        {
            InitializeComponent();
            current_date = DateTime.Today.ToString("dd/MM/yyyy");
            get_grid_value();
            txt_search.Text = current_date;
        }
        double total_price = 0;
        void get_bydate_grid_value() //show the tbl_purchase in the DataGrid
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_sales where sell_date='"+this.txt_search.Text+"'";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
            
                MySqlDataAdapter data_adpa = new MySqlDataAdapter(mysqlcomm);
                DataTable dt = new DataTable("tbl_sales");
                data_adpa.Fill(dt);
                dg_sales_history.ItemsSource = dt.DefaultView;
                MySqlDataReader data = mysqlcomm.ExecuteReader();
                while (data.Read())
                {
                    total_price = total_price + Convert.ToDouble(data.GetString(4));

                }
                mysqlcon.Close();
            }
            catch (Exception)
            {
            }
        }
        void get_grid_value() //show the tbl_purchase in the DataGrid
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_sales";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
                MySqlDataAdapter data_adpa = new MySqlDataAdapter(mysqlcomm);
                DataTable dt = new DataTable("tbl_sales");
                data_adpa.Fill(dt);
                dg_sales_history2.ItemsSource = dt.DefaultView;
                mysqlcon.Close();
            }
            catch (Exception)
            {
            }
        }
       
        private void btn_search_Click_1(object sender, RoutedEventArgs e)
        {
            get_bydate_grid_value();
            lbl_date.Content = txt_search.Text;
            lbl_total_sales.Content = total_price;
            total_price = 0;
            
        }
    }
}
