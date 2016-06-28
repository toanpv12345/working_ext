using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BE_Stress_Tool
{
    public partial class frmSetting : Form
    {
        // public variable
        public ConfigFile objSettingConfig = new ConfigFile();
        public bool isChange = false;

        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            // Load config file
            objSettingConfig.LoadFile("Setting.config");

            // Display config value to GUI
            foreach (TabPage page in tabAll.TabPages)
            {
                // Get gridview in page
                DataGridView gridData = new DataGridView();
                foreach (var control in page.Controls)
                {
                    if (control.GetType() == typeof(DataGridView))
                    {
                        gridData = (DataGridView)control;
                    }
                }

                // Search all config value
                foreach (KeyValuePair<string, string> pair in objSettingConfig.allItems)
                {
                    // If config name contain tab name
                    if (pair.Key.Contains(page.Text))
                    {
                        // Add to grid view
                        DataGridViewRow row = (DataGridViewRow)gridData.Rows[0].Clone();
                        row.Cells[0].Value = pair.Key;
                        row.Cells[1].Value = pair.Value;
                        gridData.Rows.Add(row);
                    }
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                // Set flag to reload data in main GUI
                isChange = true;

                // Collect value from GUI
                Dictionary<string, string> dicOptValue = new Dictionary<string, string>();
                foreach (TabPage page in tabAll.TabPages)
                {
                    // Get gridview in page
                    DataGridView gridData = new DataGridView();
                    foreach (var control in page.Controls)
                    {
                        if (control.GetType() == typeof(DataGridView))
                        {
                            gridData = (DataGridView)control;
                        }
                    }

                    // Add value to dictionary
                    foreach (DataGridViewRow row in gridData.Rows)
                    {
                        if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                        {
                            dicOptValue.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString());
                        }
                    }

                    // Write dictionary to config file
                    objSettingConfig.WriteFile(dicOptValue, "Setting.config");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to write config file !\nError = " + ex.ToString());
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
