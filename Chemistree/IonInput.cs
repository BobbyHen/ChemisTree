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
    public partial class IonInput : Form
    {
        private DBConnection conn;

        public IonInput()
        {
            InitializeComponent();


            conn = new DBConnection();
            conn.ConnectToDB();
        }

     
        private void btnAccessTable_Click(object sender, EventArgs e)
        {
            //this.Hide();
            ElectronConfigScreen s1 = new ElectronConfigScreen();
            s1.Show();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainMenu s2 = new MainMenu();
            s2.ShowDialog();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Ion i = new Ion();

            i.abbr = txtbxIonAbbr.Text;
            i.name = txtbxIonName.Text; 
            i.charge = Convert.ToInt32(txtbxIonCharge.Text);
            i.ionType = txtbxIonType.Text;

            if (conn.SubmitIonDB(i))
            {
                lblResult.Text = "Success";
                txtbxIonAbbr.Text = "";
                txtbxIonCharge.Text = "";
                txtbxIonName.Text = "";
                txtbxIonType.Text = "";
            }
            else
            {
                lblResult.Text = "Error";
            }

            //sending everything to the database with the SubmitDB that jorge created
            
        }

        private void btnClearTextBoxes_Click(object sender, EventArgs e)
        {
            ClearAllText(this);
            
        }


        //Function to clear the text in the form
        private void ClearAllText(Control con)
        {
            foreach (Control c in con.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
                else
                    ClearAllText(c);
                lblResult.Text = "";


            }
            
        }

      
    }


}
