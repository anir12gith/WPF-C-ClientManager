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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp6
{

    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("data source=DESKTOP-2EHS2ET\\MSSQLSERVER01;initial catalog=tabless;integrated security=true");
        DataTable dt = new DataTable(); 

        public void load()
        {
            
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from clients order by id_clients asc",con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            
            adp.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            
        }
        public void add_values()
        {
            string name = textbox1.Text.Trim();
            string adrs = textbox3.Text.Trim();
            int age = Convert.ToInt32(textbox2.Text);
            decimal slr = Convert.ToDecimal(textbox4.Text);
            SqlCommand cmd = new SqlCommand("insert into clients (nom_clients,adresse,age,salaire) values(@nm,@adr,@age,@sal)",con);
            cmd.Parameters.Add("@nm", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@age", SqlDbType.Int).Value = age;
            cmd.Parameters.Add("@adr", SqlDbType.VarChar).Value = adrs;
            cmd.Parameters.Add("@sal", SqlDbType.Decimal).Value = slr;
            cmd.ExecuteNonQuery();
            load();
            
        }
        public void delete()
        {
            int id = Convert.ToInt32(textbox6.Text);
            SqlCommand cmd = new SqlCommand("delete from clients where id_clients = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.ExecuteNonQuery();
            load();
        }
        public void search()
        {
            string srch = textbox5.Text.Trim();
            dt.Clear();
            SqlCommand cmd = new SqlCommand("select * from clients where nom_clients like '%'+@srch+'%'", con);
            cmd.Parameters.Add("@srch",SqlDbType.VarChar).Value = srch;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            datagrid.ItemsSource = dt.DefaultView;
            

        }
        public void update()
        {
            string name = textbox1.Text.Trim();
            string adrs = textbox3.Text.Trim();
            int age = Convert.ToInt32(textbox2.Text);
            decimal slr = Convert.ToDecimal(textbox4.Text);
            int id = Convert.ToInt32(textbox6.Text);
            SqlCommand cmd = new SqlCommand("update clients set nom_clients=@nom,adresse=@adr,age=@age,salaire=@sal where id_clients = @id", con);
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@nom", SqlDbType.VarChar).Value = name;
            cmd.Parameters.Add("@age", SqlDbType.Int).Value = age;
            cmd.Parameters.Add("@adr", SqlDbType.VarChar).Value = adrs;
            cmd.Parameters.Add("@sal", SqlDbType.Decimal).Value = slr;
            cmd.ExecuteNonQuery();
            load();
        }
        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            load();
            add_values();
            textbox1.Clear();
            textbox2.Clear();
            textbox3.Clear();
            textbox4.Clear();

        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            con.Open();
            load();
        }

        private void btn1_Copy_Click(object sender, RoutedEventArgs e)
        {
            delete();
        }

        private void textbox5_KeyUp(object sender, KeyEventArgs e)
        {
            load();
            search();
            srch.Visibility = Visibility.Collapsed;

        }

        private void textbox5_GotFocus(object sender, RoutedEventArgs e)
        {
        }


        private void Ms_Click(object sender, RoutedEventArgs e)
        {
            load();
            update();
        }
    }
}
