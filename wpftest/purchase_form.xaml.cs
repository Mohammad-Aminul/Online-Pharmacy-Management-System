using System;
using System.Collections.Generic;
using System.Linq;
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

using System.Data;
using MySql.Data.MySqlClient;
namespace wpftest
{
    /// <summary>
    /// Interaction logic for purchase_form.xaml
    /// </summary>
    public partial class purchase_form : Window
    {
        public string current_date;
        public int search_id;
        public double purchase_quantity, unit_price, pre_availale_quantity, cur_purchase_quantity, pre_total_price = 0, cur_total_price;
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        public purchase_form()
        {
            InitializeComponent();
            get_grid_value();
            get_value_into_SearchCombo();
            
            date_picker.SelectedDate =DateTime.Today;//showing current date into the DatePicker control
        }


        private void date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)// getting date from DatePicker control
        {
            DateTime ida = Convert.ToDateTime(date_picker.SelectedDate);
            current_date = ida.ToString("dd/MM/yyyy");
        }
        private void get_value_into_SearchCombo()   //showing the medicine name in the search combobox
        {
            try
            {
                cb_search.Items.Clear();
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select Medicine_name from tbl_purchase";
                MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
                mysqlcommand.ExecuteNonQuery();
                MySqlDataReader data = mysqlcommand.ExecuteReader();
                while (data.Read())
                {
                    string name = data.GetString(0);
                    cb_search.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void btn_purchase_click(object sender, RoutedEventArgs e) //For new medicine import which was not exist  in the pharmacy before
        {
            import_new_medicine inm = new import_new_medicine();
            inm.ShowDialog();
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


        private void btn_search_click(object sender, RoutedEventArgs e) //For seaching medicine by name
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_purchase where Medicine_name='" + this.cb_search.Text + "'";
                MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
                mysqlcommand.ExecuteNonQuery();
                MySqlDataReader data = mysqlcommand.ExecuteReader();
                if(data.Read())
                {
                    search_id = Convert.ToInt16(data.GetString(0)); //getting the searching's medicine id into the variable 'search_id'
                    txt_medicine_name.Text = data.GetString(1);
                    cb_medicine_type.Text = data.GetString(2);
                    txt_purchase_quantity.Text = data.GetString(3);
                    txt_available_quantity.Text = data.GetString(4);
                    txt_unit_price.Text = data.GetString(5);
                    txt_totalprice.Text = data.GetString(6);
                    lbl_show_purchase_date.Content = data.GetString(7);
                    txt_medicine_position.Text = data.GetString(8);
                    txt_company.Text = data.GetString(9);
                    pre_total_price = Convert.ToDouble(data.GetString(7));
                    pre_availale_quantity = Convert.ToDouble(data.GetString(4));
                }
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
            cb_medicine_type.Clear();
        }
        private void btn_clear(object sender, RoutedEventArgs e)
        {
            func_clear();

        }


        private void grid_purchase_table_SelectionChanged(object sender, SelectionChangedEventArgs e)//getting values into textbox from DataGrid
        {
            try
            {
                object item = grid_purchase_table.SelectedItem; 
                search_id = Convert.ToInt16((grid_purchase_table.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                txt_medicine_name.Text = (grid_purchase_table.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                cb_medicine_type.Text = (grid_purchase_table.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                txt_available_quantity.Text = (grid_purchase_table.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                txt_unit_price.Text = (grid_purchase_table.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                txt_totalprice.Text = (grid_purchase_table.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                lbl_show_purchase_date.Content = (grid_purchase_table.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                txt_medicine_position.Text = (grid_purchase_table.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text;
                txt_company.Text = (grid_purchase_table.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;

                pre_total_price = Convert.ToDouble((grid_purchase_table.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
                pre_availale_quantity = Convert.ToDouble((grid_purchase_table.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text);
            }
            catch (FormatException)
            {
            }
        }

        private void btn_update_click(object sender, RoutedEventArgs e) // updating the existing medicine information after purchase more medicine
        {
            if ((!string.IsNullOrWhiteSpace(txt_medicine_name.Text)) && (!string.IsNullOrWhiteSpace(txt_purchase_quantity.Text)) && (!string.IsNullOrWhiteSpace(txt_available_quantity.Text)) && (!string.IsNullOrWhiteSpace(txt_medicine_position.Text)) && (!string.IsNullOrWhiteSpace(txt_unit_price.Text)) && (!string.IsNullOrWhiteSpace(txt_totalprice.Text)) && (!string.IsNullOrWhiteSpace(current_date)) && (!string.IsNullOrWhiteSpace(cb_medicine_type.Text)))
            {
                try
                {
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                    mysqlcon.Open();
                    string query = "UPDATE `tbl_purchase` SET `Medicine_id`='" + this.search_id + "', `Medicine_name`='" + this.txt_medicine_name.Text + "',`Medicine_type`='" + this.cb_medicine_type.Text + "',`Purchase_quantity`='" + this.txt_purchase_quantity.Text + "',`Available_quantity`='" + this.txt_available_quantity.Text + "',`Purchase_date`='" + this.current_date + "',`Unit_price`='" + Convert.ToDecimal(this.txt_unit_price.Text) + "',`Total_price`='" + Convert.ToDecimal(this.txt_totalprice.Text) + "',`Medicine_position`='" + this.txt_medicine_position.Text + "',`Company`='" + this.txt_company.Text + "' WHERE Medicine_id='" + this.search_id + "'";
                    MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                    mysqlcomm.ExecuteNonQuery();
                    mysqlcon.Close();
                    get_grid_value();
                    get_value_into_SearchCombo();
                    insert_value_into_purchase_history_table();
                    MessageBox.Show("Update successfully");
                    func_clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update fail" + ex.Message);
                }
            }
            else
                MessageBox.Show("Please Enter the medicine info.");

        }

        void delete_medicine()
        {
            if ((!string.IsNullOrWhiteSpace(txt_medicine_name.Text)) && (!string.IsNullOrWhiteSpace(txt_purchase_quantity.Text)) && (!string.IsNullOrWhiteSpace(txt_available_quantity.Text)) && (!string.IsNullOrWhiteSpace(txt_medicine_position.Text)) && (!string.IsNullOrWhiteSpace(txt_unit_price.Text)) && (!string.IsNullOrWhiteSpace(txt_totalprice.Text)) && (!string.IsNullOrWhiteSpace(current_date)) && (!string.IsNullOrWhiteSpace(cb_medicine_type.Text)))
            {
                try
                {
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                    mysqlcon.Open();
                    string query = " DELETE FROM `tbl_purchase` WHERE Medicine_id='" + this.search_id + "'";
                    MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                    mysqlcomm.ExecuteNonQuery();
                    func_clear();
                    mysqlcon.Close();
                    get_grid_value();
                    MessageBox.Show("Delete successfully");
                    get_value_into_SearchCombo();
                    func_clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Delete failed");
                }
            }
            else
                MessageBox.Show("Please Enter the medicine info.");
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e) //for deleting selected medicine
        {
            if(MessageBox.Show("Are you sure to delete "+txt_medicine_name.Text+"?","Message",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
            delete_medicine();
            }
        }

        private void btn_exit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }




        private void txt_purchase_quantity_LostFocus(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txt_purchase_quantity.Text))
            {
                cur_purchase_quantity = Convert.ToDouble(txt_purchase_quantity.Text);
            }

            if (!string.IsNullOrWhiteSpace(txt_unit_price.Text))
            {
                unit_price = Convert.ToDouble(txt_unit_price.Text);

            }
            cur_total_price = cur_purchase_quantity * unit_price;
            txt_totalprice.Text = Convert.ToString(pre_total_price + cur_total_price);
            txt_available_quantity.Text = Convert.ToString(pre_availale_quantity + cur_purchase_quantity);

        }

        private void txt_unit_price_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_unit_price.Text))
            {
                unit_price = Convert.ToDouble(txt_unit_price.Text);

            }
            cur_total_price = purchase_quantity * unit_price;
            txt_totalprice.Text = Convert.ToString(pre_total_price + cur_total_price);

        }

        private void btn_grid_refresh_click(object sender, RoutedEventArgs e)
        {
            get_grid_value();
        }


    }
}
