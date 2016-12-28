using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
namespace wpftest
{
    class SALES_LOGIC
    {



        public int counter = 0;
        public string[] medicine_list = new string[200];
        //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
        string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";

        public void get_value_into_SearchCombo(string medicine_type)   //showing the medicine name in the search combobox by Medicine type
        {

            counter = 0;
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select Medicine_name from tbl_purchase where Medicine_type='" + medicine_type + "'";
                MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
                mysqlcommand.ExecuteNonQuery();
                MySqlDataReader data = mysqlcommand.ExecuteReader();
                while (data.Read())
                {
                    string name = data.GetString(0);
                    medicine_list[counter] = name;
                    counter++;
                }
                mysqlcon.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }

        //public void get_value_into_SearchCombo_all()   //showing the all medicine name in the  combobox
        //{
        //    counter = 0;
        //    try
        //    {
        //        MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
        //        mysqlcon.Open();
        //        string query = "select Medicine_name from tbl_purchase";
        //        MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
        //        mysqlcommand.ExecuteNonQuery();
        //        MySqlDataReader data = mysqlcommand.ExecuteReader();
        //        while (data.Read())
        //        {
        //            string name = data.GetString(0);
        //            medicine_list[counter] = name;
        //            counter++;
        //        }
        //        mysqlcon.Close();
        //    }
        //    catch (Exception er)
        //    {
        //        MessageBox.Show(er.Message);
        //    }
        //}


        /// <summary>
        /// update stored medicine information after sales 
        /// </summary>
        public void update_purchase_table(double updated_available_quantity, double updated_total_price, string medicine_type, string medicine_name)
        {

            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "update tbl_purchase set Available_quantity='" + updated_available_quantity + "',Total_price='" + updated_total_price + "' where Medicine_type='" + medicine_type + "' and Medicine_name='" + medicine_name + "'";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
                mysqlcon.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }

        }


        /// <summary>
        /// after selling the medicine, the history will be saved in the tbl_sales
        /// </summary>
        public void insert_into_sales_table(string medicine_name, string medicine_type, string required_quantity, string unit_price, string total_price, string current_date, string company_name)
        {
            try
            {
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "insert into tbl_sales(Medicine_name,Medicine_type,Sales_quantity,unit_price,total_price,sell_date,Company_name) values('" + medicine_name + "','" + medicine_type + "','" + required_quantity + "','" + unit_price + "','" + total_price + "','" + current_date + "','" + company_name + "')";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
                mysqlcon.Close();
                MessageBox.Show("Sell successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"error is here");
            }
        }


        /// <summary>
        ///after closing the medicine_name comboBox, 
        ///the medicine information will be displayed into textBoxs 
        /// </summary>

        public string available_medicine, unit_price, medicine_position, company_name;
        public double current_available_quantity;
        public void get_medicine_info_into_textbox(string medicine_name, string medicine_type)
        {
            if (!string.IsNullOrEmpty(medicine_type))
            {
                try
                {
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                    mysqlcon.Open();
                    string query = "select * from tbl_purchase where medicine_name='" + medicine_name + "' and Medicine_type='" + medicine_type + "'";
                    MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
                    mysqlcommand.ExecuteNonQuery();
                    MySqlDataReader data = mysqlcommand.ExecuteReader();
                    while (data.Read())
                    {

                        available_medicine = data.GetString(4);
                        unit_price = data.GetString(5);
                        medicine_position = data.GetString(8);
                        company_name = data.GetString(9);

                        current_available_quantity = Convert.ToDouble(data.GetString(4));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                    mysqlcon.Open();
                    string query = "select * from tbl_purchase where medicine_name='" + medicine_name + "'";
                    MySqlCommand mysqlcommand = new MySqlCommand(query, mysqlcon);
                    mysqlcommand.ExecuteNonQuery();
                    MySqlDataReader data = mysqlcommand.ExecuteReader();
                    while (data.Read())
                    {
                        medicine_type = data.GetString(1);
                        available_medicine = data.GetString(4);
                        unit_price = data.GetString(5);
                        medicine_position = data.GetString(8);
                        company_name = data.GetString(9);

                        current_available_quantity = Convert.ToDouble(data.GetString(4));

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
