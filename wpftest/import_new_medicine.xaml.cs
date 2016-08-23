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
using wpftest;
namespace wpftest
{
    /// <summary>
    /// Interaction logic for import_new_medicine.xaml
    /// </summary>
    public partial class import_new_medicine : Window
    {
        public string current_date;
        public int search_id;
        public double purchase_quantity, unit_price = 0, availale_quantity, pre_total_price = 0, cur_total_price = 0;
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        public import_new_medicine()
        {
            InitializeComponent();
            get_grid_value();
            date_picker.SelectedDate = DateTime.Today;//showing current date into the DatePicker control
        }
        void get_grid_value() //show the tbl_purchase in the DataGrid
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_purchase";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
                MySqlDataAdapter data_adpa = new MySqlDataAdapter(mysqlcomm);
                DataTable dt = new DataTable("tbl_purchase");
                data_adpa.Fill(dt);
                grid_purchase_table.ItemsSource = dt.DefaultView;
                mysqlcon.Close();
            }
            catch (Exception)
            {
            }
        }


        void func_clear()
        {
            txt_medicine_name.Clear();
            txt_medicine_position.Clear();
            txt_purchase_quantity.Clear();
            txt_totalprice.Clear();
            txt_unit_price.Clear();
            txt_available_quantity.Clear();
            txt_company.Clear();
            lbl_show_purchase_date.Content = "";
        }
        private void date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)// getting date from DatePicker control
        {
            DateTime ida = Convert.ToDateTime(date_picker.SelectedDate);
            current_date = ida.ToString("dd/MM/yyyy");
        }
        void insert_value_into_purchase_history_table()
        {
            MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
            mysqlcon.Open();
            string query = "INSERT INTO `tbl_purchase_history`(`Medicine_type`, `Medicine_name`, `Last_Purchase_quantity`, `Unit_price`, `Total_price`, `Comapany_name`, `Medicine_position`, `Availble_quantity`, `Purchase_date`)values('" + this.cb_medicine_type.Text + "','" + this.txt_medicine_name.Text + "','" + this.txt_purchase_quantity.Text + "','" + this.txt_unit_price.Text + "','" + this.txt_totalprice.Text + "','" + this.txt_company.Text + "','" + this.txt_medicine_position.Text + "','" + this.txt_available_quantity.Text + "','" + current_date + "')";
            MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
            mysqlcommand.ExecuteNonQuery();
            mysqlcon.Close();
        }
        private void btn_purchase_click(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrWhiteSpace(txt_medicine_name.Text)) && (!string.IsNullOrWhiteSpace(txt_purchase_quantity.Text)) && (!string.IsNullOrWhiteSpace(txt_medicine_position.Text)) && (!string.IsNullOrWhiteSpace(txt_unit_price.Text)) && (!string.IsNullOrWhiteSpace(txt_totalprice.Text)) && (!string.IsNullOrWhiteSpace(current_date)) && (!string.IsNullOrWhiteSpace(cb_medicine_type.Text)))
            {
                try
                {
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                    mysqlcon.Open();
                    string query = "INSERT INTO `tbl_purchase`( `Medicine_name`, `Medicine_type`, `Purchase_quantity`, `Available_quantity`, `Purchase_date`, `Unit_price`, `Total_price`, `Medicine_position`, `Company`) VALUES ('" + this.txt_medicine_name.Text + "','" + this.cb_medicine_type.Text + "','" + this.txt_purchase_quantity.Text + "','" + this.txt_purchase_quantity.Text + "','" + current_date + "','" + this.txt_unit_price.Text + "','" + this.txt_totalprice.Text + "','" + this.txt_medicine_position.Text + "','" + this.txt_company.Text + "')";
                    MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                    mysqlcomm.ExecuteNonQuery();
                    mysqlcon.Close();
                    get_grid_value();
                    insert_value_into_purchase_history_table();
                    MessageBox.Show("Save successfully.");
                    func_clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " ");
                }
            }
            else
                MessageBox.Show("Please Enter the medicine info.");
        }

        private void txt_purchase_quantity_LostFocus(object sender, RoutedEventArgs e)
        {
           
            if (!string.IsNullOrWhiteSpace(txt_purchase_quantity.Text))
            {
                purchase_quantity = Convert.ToDouble(txt_purchase_quantity.Text);
                
            }
            

        }

        private void txt_unit_price_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txt_unit_price.Text))
            {
            unit_price = Convert.ToDouble(txt_unit_price.Text);
            txt_totalprice.Text = Convert.ToString(purchase_quantity * unit_price);
            txt_available_quantity.Text = purchase_quantity.ToString();
           
            }
        }

        private void btn_exit_click(object sender, RoutedEventArgs e)
        {  
           
            this.Close();
        }
    }
}
