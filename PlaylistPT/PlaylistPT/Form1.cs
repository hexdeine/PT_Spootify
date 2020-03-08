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
    public partial class Form1 : Form
    {
        List<Playlist> playlist = new List<Playlist>();

        public Form1()
        {
            InitializeComponent();
        }

        private void fill(string queryString, string path)
        {
            //connect to server
            SqlConnection connection = new SqlConnection(path);
            connection.Open();
            //execute sql command
            SqlCommand command = new SqlCommand(queryString, connection);
            var reader = command.ExecuteReader();

            try
            {
                int counter = 1;

                while (reader.Read())
                {
                    Playlist oPlaylist = new Playlist();
                    string[] studentElement = { reader["SongNo"].ToString(), reader["SongName"].ToString(), reader["Artist"].ToString(), reader["Album"].ToString() };
                    oPlaylist.songNo = counter.ToString();
                    oPlaylist.songName = studentElement[1];
                    oPlaylist.artist = studentElement[2];
                    oPlaylist.album = studentElement[3];              
                    playlist.Add(oPlaylist);

                    ListViewItem listview = new ListViewItem(counter.ToString());
                    for(int i = 1; i < studentElement.Length; i++)
                    {
                        listview.SubItems.Add(studentElement[i]);
                    }
                    listView1.Items.Add(listview);
                    counter++;
                }
                
            }
            catch
            {
                AddSong newSong = new AddSong();
                newSong.ShowDialog();
            }
            connection.Close();
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fill("SELECT * FROM tblPlaylist;", "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = SpootifyDB;Integrated Security = True;");
            radioButton1.Checked = true;
        }
        private void editSelectedSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string[] items =
                {
                    listView1.SelectedItems[0].SubItems[0].Text,
                    listView1.SelectedItems[0].SubItems[1].Text,
                    listView1.SelectedItems[0].SubItems[2].Text,
                    listView1.SelectedItems[0].SubItems[3].Text                 
                };

                EditSong edit = new EditSong();
                edit.elements = items;
                edit.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("No row selected...", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addNewSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSong open = new AddSong();
            if(listView1.Items.Count == 0)
            {
                open.songNumberProperty = "1";
            }
            else
            {
                int songNoInt = listView1.Items.Count;
                songNoInt++;
                open.songNumberProperty = songNoInt.ToString(); ;
            }

            this.Hide();
            open.Show();
        }

        private void deleteSelectedSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteSong delete = new DeleteSong();
                int countp = listView1.Items.Count;
                countp++;
                delete.songNumberProperty = countp.ToString();
                delete.songNameProperty = listView1.SelectedItems[0].SubItems[1].Text;
                delete.artistProperty = listView1.SelectedItems[0].SubItems[2].Text;
                delete.albumProperty = listView1.SelectedItems[0].SubItems[3].Text;
                this.Hide();
                delete.Show();
            }
            catch
            {
                MessageBox.Show("No row selected...", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                listView1.Items.Clear();
                var familySort = from x in playlist orderby x.songName select x;
                foreach(Playlist item in familySort)
                {
                    ListViewItem list = new ListViewItem(item.songNo);
                    list.SubItems.Add(item.songName);
                    list.SubItems.Add(item.artist);
                    list.SubItems.Add(item.album);
                    listView1.Items.Add(list);
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            {
                listView1.Items.Clear();
                var familySort = from x in playlist orderby x.artist, x.songName select x;
                foreach (Playlist item in familySort)
                {
                    ListViewItem list = new ListViewItem(item.songNo);
                    list.SubItems.Add(item.songName);
                    list.SubItems.Add(item.artist);
                    list.SubItems.Add(item.album);
                    listView1.Items.Add(list);
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                listView1.Items.Clear();
                var familySort = from x in playlist orderby x.artist select x;
                foreach (Playlist item in familySort)
                {
                    ListViewItem list = new ListViewItem(item.songNo);
                    list.SubItems.Add(item.songName);
                    list.SubItems.Add(item.artist);
                    list.SubItems.Add(item.album);
                    listView1.Items.Add(list);
                }
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                listView1.Items.Clear();
                var familySort = from x in playlist orderby x.album select x;
                foreach (Playlist item in familySort)
                {
                    ListViewItem list = new ListViewItem(item.songNo);
                    list.SubItems.Add(item.songName);
                    list.SubItems.Add(item.artist);
                    list.SubItems.Add(item.album);
                    listView1.Items.Add(list);
                }
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                listView1.Items.Clear();
                var familySort = from x in playlist orderby x.artist, x.album, x.songName select x; 
                foreach (Playlist item in familySort)
                {
                    ListViewItem list = new ListViewItem(item.songNo);
                    list.SubItems.Add(item.songName);
                    list.SubItems.Add(item.artist);
                    list.SubItems.Add(item.album);
                    listView1.Items.Add(list);
                }
            }
        }
    }
}
