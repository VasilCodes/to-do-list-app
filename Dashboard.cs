using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace To_Do_List_App
{
    public partial class Form1 : Form
    {
        private string settingsFolder = Path.Combine(Application.StartupPath, "settings");
        private string tasksFile;

        public Form1()
        {
            InitializeComponent();

            tasksFile = Path.Combine(settingsFolder, "tasks.txt");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasks();
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            SaveTasks();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTask.Text != "")
            {
                lstTasks.Items.Add(txtTask.Text);
                txtTask.Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                lstTasks.Items.RemoveAt(lstTasks.SelectedIndex);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex != -1)
            {
                int index = lstTasks.SelectedIndex;
                string task = lstTasks.Items[index].ToString();

                if (!task.StartsWith("✔ "))
                {
                    lstTasks.Items[index] = "✔ " + task;
                }
            }

            /*if (lstTasks.SelectedIndex != -1)
            {
                int index = lstTasks.SelectedIndex;
                lstTasks.Items[index] =
                    "✔ " + lstTasks.Items[index];
            }*/
        }
        private void SaveTasks()
        {
            EnsureSettingsFolder();

            StreamWriter sw = new StreamWriter(tasksFile);

            foreach (var item in lstTasks.Items)
            {
                string task = item.ToString();

                if (!task.StartsWith("✔ "))
                {
                    sw.WriteLine(task);
                }
            }

            sw.Close();
        }

        private void LoadTasks()
        {
            EnsureSettingsFolder();

            if (File.Exists(tasksFile))
            {
                string[] lines = File.ReadAllLines(tasksFile);

                foreach (string line in lines)
                {
                    lstTasks.Items.Add(line);
                }
            }
        }

        private void EnsureSettingsFolder()
        {
            if (!Directory.Exists(settingsFolder))
            {
                Directory.CreateDirectory(settingsFolder);
            }
        }
    }
}
