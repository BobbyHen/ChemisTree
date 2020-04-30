using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chemistree_GUI_V1
{
    public partial class ElectronConfigScreen : Form
    {
        private DBConnection conn;

        public ElectronConfigScreen()
        {
            InitializeComponent();
            conn = new DBConnection();
            conn.ConnectToDB();
        }

        #region Public Methods

        // 
        // Formats the electron configuration appropriately by finding the numbers that need to be converted to superscripts.
        // Example of an electron configuration stored in the database: [Ar]3d^10,4s^2,4p^2
        //
        public static string formatElectronConfig(string config) {
            SmallNumber uni = new SmallNumber();

            // The configuration is split by each orbital. The "," separates each orbital.
            string[] splittedConfig = config.Split(',');
            string formattedConfig = "";

            // These help determine which numbers are coefficients and which numbers are superscripts.
            int splitMarker = 0;
            int superNum;
            string superStr;

            for (int i = 0; i < splittedConfig.Length; i++)
            {
                int configLen = splittedConfig[i].Length;

                // Loops through an individual orbital configuration 
                for (int x = 0; x < configLen; x++)
                {
                    // The "^" indicates that the following character is a superscript.
                    if (splittedConfig[i][x] == '^')
                    {
                        splitMarker = x;
                    }
                }

                // This loops through the individual orbital configuration at the point where a character is the superscript.
                // The positions of electrons in an orbital is from 1-14, so double digits can occur and confuse superscripts and coefficients.
                for (int y = splitMarker; y < configLen - 1; y++)
                {
                    int.TryParse(splittedConfig[i][y + 1].ToString(), out superNum);
                    superStr = uni.convertToSuperscript(superNum);

                    // This replaces the array element with the subscript number.
                    splittedConfig[i] = splittedConfig[i].Replace(splittedConfig[i][y].ToString(), superStr.ToString());
                }

                // This removes the last element of the array
                splittedConfig[i] = splittedConfig[i].Remove((configLen - 1), 1);
                formattedConfig += splittedConfig[i];

            }

            return formattedConfig;
        }

        #endregion

        #region Unhandled Events

        private void navigation_panal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void compound_choice_label_Click(object sender, EventArgs e)
        {

        }

        private void ElectronConfigScreen_Load(object sender, EventArgs e)
        {

        }

        private void result_panel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void lblOutput_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void result_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Handled Events

        private void nav_menu_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu s1 = new MainMenu();
            s1.Show();
        }

        private void nav_exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //
        // This checks for a particular element button featured on the periodic table and returns its electron configuration to the GUI.
        //
        private void btn_query(object sender, EventArgs e)
        {
            Button s = (System.Windows.Forms.Button)sender;

            // The button text is passed into the argument of queryDB();
            (bool result, Element el) = conn.queryDB(s.Text);
            if (result)
            {
                lblElemAbbr.Visible = true;
                lblElemAbbr.Text = $"{el.abbr}";
                string elemInfo = $"Protons {el.atomicNumber} \nElectrons: {el.atomicNumber} \nPeriod: {el.periodicPeriod} \nGroup: {el.periodicGroup}";

                // Must convert the superscripts in the unformatted electron configuration to unicode.
                el.electronConfiguration = formatElectronConfig(el.electronConfiguration);
                lblElectronConfig.Text = $"{el.electronConfiguration}";

                lblElemName.Text = $"{el.name}";
                lblOutput.Text = elemInfo;
            }
            else
            {
                lblOutput.Text = "Error! Element not found in table";
            }
        }

        #endregion
    }
}
