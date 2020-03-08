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
    public partial class DeleteSong : Form
    {
        public string songNumberProperty { get; set; }
        public string songNameProperty { get; set; }
        public string artistProperty { get; set; }
        public string albumProperty { get; set; }

        public DeleteSong()
        {
            InitializeComponent();
        }

        private void DeleteSong_Load(object sender, EventArgs e)
        {
            SongNumber.Text = songNumberProperty;
            textBox1.Text = songNameProperty;
            textBox2.Text = artistProperty;
            textBox3.Text = albumProperty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult diag;
            diag = MessageBox.Show("Are you sure?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (diag == DialogResult.Yes)
            {
                //connect to server
                SqlConnection connection = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = SpootifyDB;Integrated Security = True;MultipleActiveResultSets=true");
                connection.Open();
                //execute sql command
                string queryString1 = "SELECT SongNo FROM tblPlaylist WHERE SongName = @SongName";
                SqlCommand command1 = new SqlCommand(queryString1, connection);
                SqlParameter parame = new SqlParameter("@SongName", textBox1.Text);
                command1.Parameters.Add(parame);
                var songNoSQL = command1.ExecuteReader();
                int songNoLocal = 0;
                if (songNoSQL.HasRows)
                {
                    while(songNoSQL.Read())
                    {
                        songNoLocal = songNoSQL.GetInt32(0);
                    }
                }

                string queryString = "DELETE FROM tblPlaylist WHERE SongNo = @SongNo";
                SqlCommand command = new SqlCommand(queryString, connection);

                SqlParameter param = new SqlParameter("@SongNo", songNoLocal.ToString());
                command.Parameters.Add(param);

                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Song successfully deleted!", "Success", MessageBoxButtons.OK);
                this.Close();

                Form1 list = new Form1();
                list.Show();
            }

            else 
            {
                this.Close();
            }
        }
    }
}
