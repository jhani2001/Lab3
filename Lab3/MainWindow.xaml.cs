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
using System.Windows;
using System.Data.SqlClient;
using System.Data;

namespace Lab3
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("Data Source=HUGO-PC\\SQLEXPRESS;Initial Catalog=CAVV_MonitoreoViolencia;Integrated Security=True;");
        public MainWindow()
        {
            InitializeComponent();
        }

      
         private void btnNuevo_click(object sender, RoutedEventArgs e)

        {


        }

        private void btnConsultar_Click(object sender, RoutedEventArgs e)
        {
            SqlParameter param = null;
            SqlCommand command = null;
            List<Person> persons = null;
            try
            {
                connection.Open();

                //Hago mi consulta
                command = new SqlCommand("BuscarPersonaNombre", connection);
                command.CommandType = CommandType.StoredProcedure;

                param = new SqlParameter();
                param.ParameterName = "@FirsName";
                param.SqlDbType = SqlDbType.VarChar;
                param.Value = txtNombre.Text;

                command.Parameters.Add(param);

                SqlDataReader reader = command.ExecuteReader();
                persons = new List<Person>();


                while (reader.Read())
                {

                    Person Person = new Person();
                    Person.PersonId = reader["PersonID"].ToString();
                    Person.FirstName = reader["FirstName"].ToString();
                    Person.FullName = reader["FullName"].ToString();
                    Person.Age = (int)reader["Age"];
                    persons.Add(Person);

                }

                connection.Close();

                //Muestro la información
                dgvPeople.ItemsSource = persons;
            }
            catch (Exception ex)
            {
                connection.Close();
                throw;
            }

            
        }
    }
}
