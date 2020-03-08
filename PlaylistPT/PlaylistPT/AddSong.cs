using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PlaylistPT
{
    public partial class AddSong : Form
    {
        public string songNumberProperty { get; set; }

        public AddSong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //connect to server
            SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = SpootifyDB;Integrated Security = True;");
            connection.Open();
            //execute sql command
            string queryString = "INSERT INTO tblPlaylist VALUES(@SongName, @Artist, @Album)";

            //SqlParameter param1 = new SqlParameter("@SongNo", SongNumber.Text);
            SqlParameter param2 = new SqlParameter("@SongName", textBox1.Text);
            SqlParameter param3 = new SqlParameter("@Artist", textBox2.Text);
            SqlParameter param4 = new SqlParameter("@Album", textBox3.Text);

            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty || textBox3.Text == String.Empty)
            {
                MessageBox.Show("Please fill all fields.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var command = new SqlCommand(queryString, connection);
                //command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Song added successfully!", "Success!", MessageBoxButtons.OK);
                textBox1.Text = textBox2.Text = textBox3.Text = String.Empty;
            }
            Form1 bruh = new Form1();
            this.Close();
            bruh.Show();
        }

        public void AddSong_Load(object sender, EventArgs e)
        {
            SongNumber.Text = songNumberProperty;
        }
    }
}
