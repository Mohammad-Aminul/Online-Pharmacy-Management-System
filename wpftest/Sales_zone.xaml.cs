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

using MySql.Data.MySqlClient;
using System.Data;



namespace wpftest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class windowsales : Window
    {
        SALES_LOGIC sl_obj = new SALES_LOGIC();
        
        public double total_price, quantity;
        public string  m_type, current_date, c_date;

        public windowsales()
        {
            InitializeComponent();
            
            c_date = DateTime.Today.ToString("dd/MM/yyyy");
            date_picker.SelectedDate = DateTime.Today;//display today's date in the datePicker
            get_grid_value();
            get_date_time_in_voucher();
            
        }

        void get_date_time_in_voucher()
        {
            voucher_date_time.Content = DateTime.Now.ToString("dd/MM/yyyy h:mm:ss tt");
        }
        /// <summary>
        ///this methods shows the medicine names in the  combobox according to  Medicine type
        /// </summary>
        private void get_value_into_SearchCombo() 
        {
            int i = 0;

            sl_obj.get_value_into_SearchCombo(this.cb_medicine_type.Text);

            while (sl_obj.counter != i)
            {
                cmbox_medicine_name.Items.Add(sl_obj.medicine_list[i]);
                i++;
            }
           
        }

        /// <summary>
        ///this method shows the Today's sales history in the DataGrid
        /// </summary>
        void get_grid_value()
        {
            try
            {
                string ConnectionString = @"server=localhost;database=dbpharmacy;username='root';password=''";
                //string ConnectionString = @"server=db4free.net;  DATABASE=dbpharmacy;    port=3306;  userid=projectcse; PASSWORD=adms11";
                MySqlConnection mysqlcon = new MySqlConnection(ConnectionString);
                mysqlcon.Open();
                string query = "select * from tbl_sales where sell_date='" + c_date + "'";
                MySqlCommand mysqlcomm = new MySqlCommand(query, mysqlcon);
                mysqlcomm.ExecuteNonQuery();
                MySqlDataAdapter data_adpa = new MySqlDataAdapter(mysqlcomm);
                DataTable dt = new DataTable("tbl_sales");
                data_adpa.Fill(dt);
                dg_sales_view.ItemsSource = dt.DefaultView;
                mysqlcon.Close();
            }
            catch (MySqlException mse)
            {
                MessageBox.Show(mse.Message);
            }
    
        }

        private void menu_purchase(object sender, RoutedEventArgs e)
        {
            purchase_form window_purchase = new purchase_form();
            window_purchase.ShowDialog();
        }

        private void menu_sales_history(object sender, RoutedEventArgs e)
        {
            sales_history sh = new sales_history();
            sh.Show();
        }

        private void menu_purchase_history(object sender, RoutedEventArgs e)
        {
            Purchase_history ph = new Purchase_history();
            ph.Show();
        }

        private void menu_orders(object sender, RoutedEventArgs e)
        {
            customer_order cd = new customer_order();
            cd.Show();
        }

        private void menu_logout(object sender, RoutedEventArgs e)
        {
            MainWindow sh = new MainWindow();
            sh.Show();
            this.Close();
        }

        private void menu_password_change(object sender, RoutedEventArgs e)
        {
            Admin ad = new Admin();
            ad.ShowDialog();
        }

        void func_txt_clear()
        {
            txt_company_name.Clear();
            txt_mecicine_position.Clear();
            txt_required_quantity.Clear();
            txt_total_price.Clear();
            txt_unit_price.Clear();
            txtavailable.Clear();

        }
        
/// <summary>
/// after selling the medicine, the medicine information will be updated and the history will be
/// saved in tbl_sales table
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btn_sell_click(object sender, RoutedEventArgs e)
        {
            if (r_quantity <= current_available_quantity)
            {
                if (!string.IsNullOrWhiteSpace(this.txt_required_quantity.Text))
                {
                    sl_obj.update_purchase_table(updated_available_quantity, updated_total_price, this.cb_medicine_type.Text, this.cmbox_medicine_name.Text);

                    sl_obj.insert_into_sales_table(this.cmbox_medicine_name.Text, this.cb_medicine_type.Text, this.txt_required_quantity.Text, this.txt_unit_price.Text, this.txt_total_price.Text, current_date, this.txt_company_name.Text);
                    get_grid_value();
                    create_voucher();
                    func_txt_clear();
                   
                }
                else
                    MessageBox.Show("Please Enter Required Quantity.");
            }
            else
                MessageBox.Show("Required Quantity is not available.");
        }



        private void cb_medicine_type_DropDownClosed(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(cb_medicine_type.Text))
            {
                cmbox_medicine_name.Items.Clear();
                get_value_into_SearchCombo();
                m_type = cb_medicine_type.Text;
            }
            else if (string.IsNullOrWhiteSpace(cb_medicine_type.Text))
            {
               
                cmbox_medicine_name.Items.Clear();
              
              
            }

        }
        public double updated_available_quantity, current_available_quantity, updated_total_price;

        /// <summary>
        ///after closing the medicine_name comboBox, 
        ///the medicine information will be displayed into textBoxs 
        /// </summary>
        private void cmbox_medicine_name_DropDownClosed(object sender, EventArgs e)
        {
            sl_obj.get_medicine_info_into_textbox(this.cmbox_medicine_name.Text, m_type);
           
            txtavailable.Text =sl_obj.available_medicine;
            txt_unit_price.Text = sl_obj.unit_price;
            txt_mecicine_position.Text = sl_obj.medicine_position;
            txt_company_name.Text = sl_obj.company_name;
            current_available_quantity = Convert.ToDouble(sl_obj.current_available_quantity);
        }

        private void purchase_MouseEnter(object sender, MouseEventArgs e)
        {
            purchase.Background = Brushes.White;
        }

        private void purchase_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            purchase.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");
        }

        private void sales_MouseEnter(object sender, MouseEventArgs e)
        {
            sales.Background = Brushes.White;
        }

        private void sales_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            sales.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");
        }

        private void purchase_history_m_MouseEnter(object sender, MouseEventArgs e)
        {
            purchase_history_m.Background = Brushes.White;
        }

        private void purchase_history_m_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            purchase_history_m.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");
        }

        private void menu_orders_MouseEnter(object sender, MouseEventArgs e)
        {
            orders.Background = Brushes.White;
        }

        private void orders_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            orders.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");
        }

        private void admin_MouseEnter(object sender, MouseEventArgs e)
        {
            admin.Background = Brushes.White;
        }

        private void admin_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            admin.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");
        }

        private void date_picker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime ida = Convert.ToDateTime(date_picker.SelectedDate);
            current_date = ida.ToString("dd/MM/yyyy");
        }
        public double r_quantity, unit_price;
        /// <summary>
        /// when user will input the required quantity into the 'ruquired TextBox',
        /// the total price of the medicine will be shown in the 'total price TextBox'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///

        private void txt_required_quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                r_quantity = Convert.ToDouble(txt_required_quantity.Text);
                unit_price = Convert.ToDouble(txt_unit_price.Text);
                txt_total_price.Text = Convert.ToString(r_quantity * unit_price);
                updated_available_quantity = current_available_quantity - r_quantity;
                updated_total_price = updated_available_quantity * unit_price;
            }
            catch (Exception)
            {

            }
            
        }
        //private void txt_required_quantity_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //        try
        //    {
        //        r_quantity = Convert.ToDouble(txt_required_quantity.Text);
        //        unit_price = Convert.ToDouble(txt_unit_price.Text);
        //        txt_total_price.Text = Convert.ToString(r_quantity * unit_price);
        //        updated_available_quantity = current_available_quantity - r_quantity;
        //        updated_total_price = updated_available_quantity * unit_price;
        //    }
        //    catch (Exception)
        //    {

        //    }
        //        create_voucher();
        //}

        private void btn_clear_clear(object sender, RoutedEventArgs e)
        {
            func_txt_clear();
          
        }

        /// <summary>
        /// when user will input the unit price into the 'Unit price TextBox',
        /// the total price of the medicine will be shown in the 'total price TextBox'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_unit_price_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                r_quantity = Convert.ToDouble(txt_required_quantity.Text);
                unit_price = Convert.ToDouble(txt_unit_price.Text);
                txt_total_price.Text = Convert.ToString(r_quantity * unit_price);
                updated_available_quantity = current_available_quantity - r_quantity;
                updated_total_price = updated_available_quantity * unit_price;
            }
            catch (Exception)
            {
                MessageBox.Show("Please Enter Unit Price");

            }
        }

        private void about_MouseEnter(object sender, MouseEventArgs e)
        {
            about.Background = Brushes.White;
        }

        private void about_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            about.Background = (Brush)bc.ConvertFrom("#ffe5e5e5");
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {                          
            about ab = new about();
            ab.Show();
        }


        /// <summary>
        /// this method will create the voucher for cutomers
        /// </summary>
        public void create_voucher()
        {
   
                   //row1
                if (lbl1.Content.ToString() == cmbox_medicine_name.Text && lbl2.Content.ToString() == cb_medicine_type.Text)
                {
                    //double old_quantity, old_total;
                    //old_quantity = Convert.ToDouble(lbl3.Content);
                    //old_total = Convert.ToDouble(lbl5.Content);
                    lbl1.Content = cmbox_medicine_name.Text;
                    lbl2.Content = cb_medicine_type.Text;
                    lbl3.Content =Convert.ToInt16( txt_required_quantity.Text)+Convert.ToInt16(lbl3.Content);
                    lbl4.Content = txt_unit_price.Text ;
                    lbl5.Content =Convert.ToDouble( txt_total_price.Text) + Convert.ToDouble(lbl5.Content);
                }

             else if (string.IsNullOrWhiteSpace(lbl1.Content.ToString()))
            {

                lbl1.Content = cmbox_medicine_name.Text;
                lbl2.Content = cb_medicine_type.Text;
                lbl3.Content = txt_required_quantity.Text;
                lbl4.Content = txt_unit_price.Text;
                lbl5.Content = txt_total_price.Text;
                
            }
                    
                    //row2

                else if (lbl6.Content.ToString() == cmbox_medicine_name.Text && lbl7.Content.ToString() == cb_medicine_type.Text)
                {
                    lbl6.Content = cmbox_medicine_name.Text;
                    lbl7.Content = cb_medicine_type.Text;
                    lbl8.Content = Convert.ToInt16(txt_required_quantity.Text) + Convert.ToInt16(lbl8.Content);
                    lbl9.Content = txt_unit_price.Text;
                    lbl10.Content = Convert.ToDouble(txt_total_price.Text) + Convert.ToDouble(lbl10.Content);
                }

           else if (string.IsNullOrWhiteSpace(lbl6.Content.ToString()))
            {
                lbl6.Content = cmbox_medicine_name.Text;
                lbl7.Content = cb_medicine_type.Text;
                lbl8.Content = txt_required_quantity.Text;
                lbl9.Content = txt_unit_price.Text;
                lbl10.Content = txt_total_price.Text;
            }

                    //row3
                else if (lbl11.Content.ToString() == cmbox_medicine_name.Text && lbl12.Content.ToString() == cb_medicine_type.Text)
            {
                lbl11.Content = cmbox_medicine_name.Text;
                lbl12.Content = cb_medicine_type.Text;
                lbl13.Content = Convert.ToInt16(txt_required_quantity.Text) + Convert.ToInt16(lbl13.Content);
                lbl14.Content = txt_unit_price.Text;
                lbl15.Content = Convert.ToDouble(txt_total_price.Text) + Convert.ToDouble(lbl15.Content);
            }
                else if (string.IsNullOrWhiteSpace(lbl11.Content.ToString()))
                {
                    lbl11.Content = cmbox_medicine_name.Text;
                    lbl12.Content = cb_medicine_type.Text;
                    lbl13.Content = txt_required_quantity.Text;
                    lbl14.Content = txt_unit_price.Text;
                    lbl15.Content = txt_total_price.Text;
                }
                    //row4
                else if (lbl16.Content.ToString() == cmbox_medicine_name.Text && lbl17.Content.ToString() == cb_medicine_type.Text)
                {
                    lbl16.Content = cmbox_medicine_name.Text;
                    lbl17.Content = cb_medicine_type.Text;
                    lbl18.Content = Convert.ToInt16(txt_required_quantity.Text) + Convert.ToInt16(lbl18.Content);
                    lbl19.Content = txt_unit_price.Text;
                    lbl20.Content = Convert.ToDouble(txt_total_price.Text) + Convert.ToDouble(lbl10.Content);
                }
            else if (string.IsNullOrWhiteSpace(lbl16.Content.ToString()))
            {
                lbl16.Content = cmbox_medicine_name.Text;
                lbl17.Content = cb_medicine_type.Text;
                lbl18.Content = txt_required_quantity.Text;
                lbl19.Content = txt_unit_price.Text;
                lbl20.Content = txt_total_price.Text;
            }
                    //row5
                else if (lbl21.Content.ToString() == cmbox_medicine_name.Text && lbl22.Content.ToString() == cb_medicine_type.Text)
            {
                lbl21.Content = cmbox_medicine_name.Text;
                lbl22.Content = cb_medicine_type.Text;
                lbl23.Content = Convert.ToInt16(txt_required_quantity.Text) + Convert.ToInt16(lbl23.Content);
                lbl24.Content = txt_unit_price.Text;
                lbl25.Content = Convert.ToDouble(txt_total_price.Text) + Convert.ToDouble(lbl25.Content);
            }
                else if (string.IsNullOrWhiteSpace(lbl21.Content.ToString()))
                {
                    lbl21.Content = cmbox_medicine_name.Text;
                    lbl22.Content = cb_medicine_type.Text;
                    lbl23.Content = txt_required_quantity.Text;
                    lbl24.Content = txt_unit_price.Text;
                    lbl25.Content = txt_total_price.Text;
                }
       
            double voucher_total=0;

            
            //total money calculation for showing in the textbox of the voucher
            if (!string.IsNullOrWhiteSpace(lbl5.Content.ToString()))
            {
                voucher_total =voucher_total+Convert.ToDouble(lbl5.Content);
            }

            if (!string.IsNullOrWhiteSpace(lbl10.Content.ToString()))
            {
                voucher_total =voucher_total+ Convert.ToDouble(lbl10.Content);
            }

            if (!string.IsNullOrWhiteSpace(lbl15.Content.ToString()))
            {
                voucher_total = voucher_total + Convert.ToDouble(lbl15.Content);
            }
            if (!string.IsNullOrWhiteSpace(lbl20.Content.ToString()))
            {
                voucher_total = voucher_total + Convert.ToDouble(lbl20.Content);
            }
            if (!string.IsNullOrWhiteSpace(lbl25.Content.ToString()))
            {
                voucher_total = voucher_total + Convert.ToDouble(lbl25.Content);
            }
            txt_voucher_total.Text = voucher_total.ToString();


        }
            /// <summary>
            /// Clear all the label in the voucher after getting money
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void btn_voucher_label_clear_click(object sender, RoutedEventArgs e)
        {
            voucher_label_clear();

        }

        /// <summary>
        /// for clearing the voucher label
        /// </summary>
        void voucher_label_clear()
        {
            txt_voucher_total.Clear();
            lbl1.Content = "";
            lbl2.Content = "";
            lbl3.Content = "";
            lbl4.Content = "";
            lbl5.Content = "";
            lbl6.Content = "";
            lbl7.Content = "";
            lbl8.Content = "";
            lbl9.Content = "";
            lbl10.Content = "";
            lbl11.Content = "";
            lbl12.Content = "";
            lbl13.Content = "";
            lbl14.Content = "";
            lbl15.Content = "";
            lbl16.Content = "";
            lbl17.Content = "";
            lbl18.Content = "";
            lbl19.Content = "";
            lbl20.Content = "";
            lbl21.Content = "";
            lbl22.Content = "";
            lbl23.Content = "";
            lbl24.Content = "";
            lbl25.Content = "";
            get_date_time_in_voucher();
        }

        /// <summary>
        /// after clicking this button the voucher will be printed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            pd.PrintVisual(voucher, "Voucher");
            voucher_label_clear();
            get_date_time_in_voucher();
        }

    }
}
