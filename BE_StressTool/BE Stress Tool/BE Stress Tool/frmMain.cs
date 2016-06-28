using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BE_Stress_Tool
{
    public partial class frmMain : Form
    {
        // global variable
        static ConfigFile objUserConfig = new ConfigFile();
        static ConfigFile objSettingConfig = new ConfigFile();
        static Struct objStruct = new Struct();
        static public Form frmMainRef;
        List<RunScenarioWorkerThread> listConnection = new List<RunScenarioWorkerThread>();
        List<string> listStatus = new List<string>();

        public frmMain()
        {
            InitializeComponent();

            try
            {
                // Set reference to main form
                frmMainRef = this;

                // Load user config file
                objUserConfig.LoadFile("User.config");

                // Load setting config file
                objSettingConfig.LoadFile("Setting.config");

                // Generate command structure
                objStruct.GenerateAllCommand(objSettingConfig.allItems);

                // Display in GUI
                //   Timer value
                tbarConnectionTxt.Text = objUserConfig.allItems["OptStepTimer"];
                //   Loop value
                tbarLoopTxt.Text = objUserConfig.allItems["OptLoop"];
                //   List of file
                foreach (KeyValuePair<string, string> pair in objUserConfig.allItems)
                {
                    if (pair.Key.Contains("File"))
                    {
                        // Get file name and file path
                        string filePath = pair.Value;
                        string fileName = Path.GetFileName(filePath);
                        // Add to list view
                        ComboboxItem item = new ComboboxItem();
                        item.FileName = fileName;
                        item.FilePath = filePath;
                        listFile.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to read config file\nError =  " + ex.Message);
                Application.Exit();
            }
}

        // Handle action: User click Start button
        private void tbarStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (listFile.SelectedItem == null)
                {
                    MessageBox.Show("Select 1 file in list box to continue !");
                    return;
                }

                // Get connection option
                int iCon = ComFunc.StringToInt32(tbarConnectionTxt.Text);
                int iLoop = ComFunc.StringToInt32(tbarLoopTxt.Text);
                string stIPAdd = objSettingConfig.allItems["SocketIP"];
                int iPort = ComFunc.StringToInt32(objSettingConfig.allItems["SocketPort"]);
                string stAES = objSettingConfig.allItems["SocketAES"];
                string st3DES = objSettingConfig.allItems["Socket3DES"];
                int iTimeout = ComFunc.StringToInt32(objSettingConfig.allItems["CONNECTTimeout"]);
                string stUser = objSettingConfig.allItems["CONNECTUsername"];
                string stPass = objSettingConfig.allItems["CONNECTPassword"];
                txtStatus.Text = "";
                listStatus.Clear();
                listConnection.Clear();

                // Get scenario step from GUI
                List<string> listScenarioStep = new List<string>();                
                for (int i=0; i < gridFileStep.RowCount; i++)
                {
                    if (gridFileStep.Rows[i].Cells[2].Value != null)
                    {
                        string stStep = gridFileStep.Rows[i].Cells[2].Value.ToString();
                        if (stStep != "")
                        {
                            listScenarioStep.Add(stStep);
                        }
                    }                    
                }

                // Create connection
                for (int i=0; i < iCon; i++ )
                {
                    // Set parameter for worker thread
                    RunScenarioWorkerThread con = new RunScenarioWorkerThread();
                    con.ThreadID = i;
                    con.IPAdd = stIPAdd;
                    con.Port = iPort;
                    con.Loop = iLoop;
                    con.EncryptAES = stAES;
                    con.Encrypt3DES = st3DES;
                    con.Timeout = iTimeout;
                    con.objSettingConfig = objSettingConfig;
                    con.listScenario = listScenarioStep;
                    con.User = stUser;
                    con.Pass = stPass;

                    // Add event listener
                    con.EventHappned += OnComNotification;

                    // Add connection to list
                    listConnection.Add(con);

                    // Add list status
                    string status = "";
                    listStatus.Add(status);

                    // Start thread
                    listConnection[i].Run();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to create connection !\nError =  " + ex.Message);
            }
            
        }

        // Handle progress report event from communication thread
        private void OnComNotification(object sender, NoticeArg e)
        {
            // Cross thread
            if (frmMainRef.InvokeRequired)
            {
                frmMainRef.Invoke(new Action<object, NoticeArg>(OnComNotification), new object[] { sender, e});
                return;
            }

            // Update status string
            if (e.ThreadID < listStatus.Count)
            {
                // Add message to list status
                string status = "[Thread " + e.ThreadID.ToString() + " - Loop " + e.Loop.ToString() + " ] " + e.Message + " \r\n";
                listStatus[e.ThreadID] += status;

                // Display all status in text box
                txtStatus.Text = "";
                for (int i = 0; i < listStatus.Count; i++)
                {
                    txtStatus.Text += listStatus[i];
                    txtStatus.Text += "\n";
                }
            }            
        }

        // Handle action: User click Add button
        private void tbarAdd_Click(object sender, EventArgs e)
        {
            // Show Select File Dialog
            // Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Scenario Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK == DialogResult.OK)
            {
                // Add the files
                foreach (String path in openFileDialog1.FileNames)
                {
                    string fileName = Path.GetFileName(path);
                    ComboboxItem item = new ComboboxItem();
                    item.FileName = fileName;
                    item.FilePath = path;

                    listFile.Items.Add(item);
                }

                // Select first items
                if (listFile.Text == "")
                {
                    listFile.SelectedIndex = 0;
                }
            }
        }

        // Handle action: User click Remove button
        private void tbarRemove_Click(object sender, EventArgs e)
        {
            // Remove selected item
            if (listFile.Items.Count > 0 && listFile.Text != "")
            {
                int iSaveSelectIndex = listFile.SelectedIndex;
                listFile.Items.RemoveAt(listFile.SelectedIndex);

                // Set selected item to next
                if (listFile.Items.Count > iSaveSelectIndex)
                {
                    listFile.SelectedIndex = iSaveSelectIndex;
                }
                else if (listFile.Items.Count > 0)
                {
                    listFile.SelectedIndex = listFile.Items.Count - 1;
                }
                else
                {
                    // Clear combo box text
                    listFile.Text = "";
                }
            }
        }

        // Handle action: User change index of list file
        private void listFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFile();
        }

        // Handle action: user double click on list file
        private void listFile_DoubleClick(object sender, EventArgs e)
        {
            LoadFile();
        }

        // Load scenario file into grid view
        private void LoadFile()
        {
            try
            {
                // Clear grid view
                gridFileStep.Rows.Clear();

                // Get file path
                if (listFile.Items.Count > 0 && listFile.Text != "" && listFile.SelectedIndex >= 0)
                {
                    // Get file path
                    ComboboxItem objCombo = (ComboboxItem)listFile.SelectedItem;
                    string filePath = objCombo.GetFilePath();

                    // Load file path
                    ScenarioFile objSceFile = new ScenarioFile();
                    objSceFile.OpenFile(filePath);
                    objSceFile.ReadStep();

                    // Add step to GUI
                    for (int i=0; i< objSceFile.lstStep.Count(); i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)gridFileStep.Rows[0].Clone();
                        ScenarioStep stepCurrent = objSceFile.lstStep[i];
                        row.Cells[0].Value = i;
                        row.Cells[1].Value = stepCurrent.command;
                        row.Cells[2].Value = stepCurrent.data;
                        gridFileStep.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to read scenario file\nError =  " + ex.Message);
            }            
        }

        private void tbarSetting_Click(object sender, EventArgs e)
        {
            // Show Setting dialog
            frmSetting objSettingDlg = new frmSetting();
            objSettingDlg.ShowDialog();

            // Reload data
            if (objSettingDlg.isChange == true)
            {
                objSettingConfig = objSettingDlg.objSettingConfig;
                objStruct.GenerateAllCommand(objSettingConfig.allItems);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                // Save GUI to dictionary
                Dictionary<string, string> dicOptValue = new Dictionary<string, string>();
                //    Timer value
                dicOptValue.Add("OptStepTimer", tbarConnectionTxt.Text);
                dicOptValue.Add("OptLoop", tbarLoopTxt.Text);
                //    list of File
                int file_num = 0;
                foreach (ComboboxItem listBoxItem in listFile.Items)
                {
                    file_num++;
                    dicOptValue.Add("File" + file_num.ToString(), listBoxItem.FilePath);
                }

                // Write dictionary to file
                objUserConfig.WriteFile(dicOptValue, "User.config");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to write user config file !\nError = " + ex.Message);
            }
        }

        private void tbarTest_Click(object sender, EventArgs e)
        {
            // Get connection option
            int iCon = ComFunc.StringToInt32(tbarConnectionTxt.Text);
            int iLoop = ComFunc.StringToInt32(tbarLoopTxt.Text);
            string stIPAdd = objSettingConfig.allItems["SocketIP"];
            int iPort = ComFunc.StringToInt32(objSettingConfig.allItems["SocketPort"]);
            string stAES = objSettingConfig.allItems["SocketAES"];
            string st3DES = objSettingConfig.allItems["Socket3DES"];
            int iTimeout = ComFunc.StringToInt32(objSettingConfig.allItems["CONNECTTimeout"]);
            string stUser = objSettingConfig.allItems["CONNECTUsername"];
            string stPass = objSettingConfig.allItems["CONNECTPassword"];
            txtStatus.Text = "";
            listStatus.Clear();
            listConnection.Clear();

            // Get scenario step from GUI
            List<string> listScenarioStep = new List<string>();
            for (int i = 0; i < gridFileStep.RowCount; i++)
            {
                if (gridFileStep.Rows[i].Cells[2].Value != null)
                {
                    string stStep = gridFileStep.Rows[i].Cells[2].Value.ToString();
                    if (stStep != "")
                    {
                        listScenarioStep.Add(stStep);
                    }
                }
            }

            // Test to run script
            // Set parameter for worker thread
            RunScenarioWorkerThread con = new RunScenarioWorkerThread();
            con.ThreadID = 0;
            con.IPAdd = stIPAdd;
            con.Port = iPort;
            con.Loop = iLoop;
            con.EncryptAES = stAES;
            con.Encrypt3DES = st3DES;
            con.Timeout = iTimeout;
            con.objSettingConfig = objSettingConfig;
            con.listScenario = listScenarioStep;
            con.User = stUser;
            con.Pass = stPass;

            // Add event listener
            con.EventHappned += OnComNotification;

            // Add connection to list
            listConnection.Add(con);

            // Add list status
            string status = "";
            listStatus.Add(status);

            // Start thread
            listConnection[0].Run();
        }
    }
}
