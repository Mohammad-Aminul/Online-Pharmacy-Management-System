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

using System.Data;
using MySql.Data.MySqlClient;
namespace wpftest
{
    /// <summary>
    /// Interaction logic for customer_order.xaml
    /// </summary>
    public partial class customer_order : Window
    {
        int order_no,  quantity;
        string m_name, m_type, o_date, d_date;
        string current_date;
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
        public customer_order()
        {
            InitializeComponent();
            current_date = DateTime.Now.ToString("M/d/yyyy");
            get_today_order_into_DataGrid();
            get_all_order_into_DataGrid();
        }

            public void get_today_order_into_DataGrid()
        {
           
            try
            {
              
                MySqlConnection mysql_connection = new MySqlConnection(ConnectionString);
                mysql_connection.Open();
                string query = "Select * from tbl_order where dely_date='"+current_date+"'";
                
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysql_connection);
                mysqlcomm.ExecuteNonQuery();
                MySqlDataAdapter mysqldadpt = new MySqlDataAdapter(mysqlcomm);
                DataTable data = new DataTable();
                mysqldadpt.Fill(data);
                dg_today_order.ItemsSource = data.DefaultView;
            }

            catch (Exception)
            {
            
            }
        }

        void get_all_order_into_DataGrid()
        {
            try
            {
                MySqlConnection mysql_connection = new MySqlConnection(ConnectionString);
                mysql_connection.Open();
                string query = "Select * from tbl_order";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysql_connection);
                mysqlcomm.ExecuteNonQuery();
                MySqlDataAdapter mysqldadpt = new MySqlDataAdapter(mysqlcomm);
                DataTable data = new DataTable();
                mysqldadpt.Fill(data);
                dg_all_orders.ItemsSource = data.DefaultView;
            }

            catch (Exception)
            {

            }
        }

        /// <summary>
        /// this method will update the order status of the database
        /// </summary>
        void manage_order()
        {
            MySqlConnection mysql_connection = new MySqlConnection(ConnectionString);
            mysql_connection.Open();
            string query = "UPDATE `tbl_order` SET  `client_no`='" + this.txt_client_id.Text+ "',`medicine_name`='" + m_name + "',`medicine_type`='" + m_type + "',`quantity`='" + quantity + "',`order_date`='" + o_date + "',`dely_date`='" + d_date + "',`order_status`='" + this.cb_order_status.Text + "' where client_no='" + this.txt_client_id.Text + "' and  order_no='"+order_no+"'";
            MySqlCommand mysqlcomm = new MySqlCommand(query, mysql_connection);
            mysqlcomm.ExecuteNonQuery();
            mysql_connection.Close();
            MessageBox.Show("Successfull");
        }
        private void btn_manageorder_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.txt_client_id.Text))
                {
                    manage_order();
                    get_today_order_into_DataGrid();
                    get_all_order_into_DataGrid();
                }
                else
               MessageBox.Show("Please Select a Client Number", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in button");
            }
           
            
        }

        private void dg_today_order_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = dg_today_order.SelectedItem;
                order_no = Convert.ToInt16((dg_today_order.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                txt_client_id.Text = ((dg_today_order.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);
                m_name = (dg_today_order.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                m_type = (dg_today_order.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                quantity = Convert.ToInt16((dg_today_order.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                o_date = (dg_today_order.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                d_date = (dg_today_order.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                //status  = (dg_today_order.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;

            }
            catch (FormatException)
            {
            }
        }

        private void dg_all_orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = dg_all_orders.SelectedItem;
                order_no = Convert.ToInt16((dg_all_orders.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                txt_client_id.Text = ((dg_all_orders.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);
                m_name = (dg_all_orders.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                m_type = (dg_all_orders.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                quantity = Convert.ToInt16((dg_all_orders.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
                o_date = (dg_all_orders.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                d_date = (dg_all_orders.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                //status  = (dg_today_order.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;

            }
            catch (FormatException)
            {
            }
        }

    }
}
