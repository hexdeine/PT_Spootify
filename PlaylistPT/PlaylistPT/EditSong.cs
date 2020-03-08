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
    public partial class EditSong : Form
    {
        public string[] elements { get; set; }

        int songNoLocal = 0;

        public EditSong()
        {
            InitializeComponent();
        }

        private void EditSong_Load(object sender, EventArgs e)
        {
            //connect to server
            SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = SpootifyDB;Integrated Security = True;MultipleActiveResultSets=true");
            connection.Open();
            //execute sql command
            string queryString1 = "SELECT SongNo FROM tblPlaylist WHERE SongName = @SongName AND Artist = @Artist;";
            SqlCommand command1 = new SqlCommand(queryString1, connection);
            SqlParameter parame = new SqlParameter("@SongName", textBox1.Text);
            SqlParameter parame1 = new SqlParameter("@Artist", textBox2.Text);
            command1.Parameters.Add(parame);
            command1.Parameters.Add(parame1);
            var songNoSQL = command1.ExecuteReader();

            if (songNoSQL.HasRows)
            {
                while (songNoSQL.Read())
                {
                    songNoLocal = songNoSQL.GetInt32(0);
                }
            }

            SongNumber.Text = elements[0];
            textBox1.Text = elements[1];
            textBox2.Text = elements[2];
            textBox3.Text = elements[3];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //connect to server
            SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = SpootifyDB;Integrated Security = True;MultipleActiveResultSets=true");
            connection.Open();
            //execute sql command
            string queryString1 = "SELECT SongNo FROM tblPlaylist WHERE SongName = @SongName OR Artist = @Artist;";
            SqlCommand command1 = new SqlCommand(queryString1, connection);
            SqlParameter parame = new SqlParameter("@SongName", textBox1.Text);
            SqlParameter parame1 = new SqlParameter("@Artist", textBox2.Text);
            command1.Parameters.Add(parame);
            command1.Parameters.Add(parame1);
            var songNoSQL = command1.ExecuteReader();

            if (songNoSQL.HasRows)
            {
                while (songNoSQL.Read())
                {
                    songNoLocal = songNoSQL.GetInt32(0);
                }
            }

            string queryString = "UPDATE tblPlaylist SET SongName = @SongName, Artist = @Artist, Album = @Album WHERE SongNo = @SongNo";

            SqlParameter param1 = new SqlParameter("@SongNo", songNoLocal.ToString());
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
                command.Parameters.Add(param1);
                command.Parameters.Add(param2);
                command.Parameters.Add(param3);
                command.Parameters.Add(param4);
                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Song updated successfully!", "Success!", MessageBoxButtons.OK);
                textBox1.Text = textBox2.Text = textBox3.Text = String.Empty;
            }
            Form1 bruh = new Form1();
            bruh.Show();
            this.Close();
        }
    }
}
