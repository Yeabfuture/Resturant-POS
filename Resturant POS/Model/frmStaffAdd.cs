using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant_POS.Model
{
    public partial class frmStaffAdd : SampleAdd
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }

        public int id = 0;

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            string qry = "";
            Hashtable ht = new Hashtable();

            if (id == 0)
            {
                //   qry = "INSERT INTO staff (sName, sRole, sPhone) VALUES (:Name, :Role, :Phone)";
                qry = "CALL StaffAdd(:Name, :Role, :Phone)";
                ht.Add(":Name", txtName.Text);
                ht.Add(":Role", cmbRole.Text);
                ht.Add(":Phone", txtPhone.Text);
            }
            else
            {
               
                qry = "UPDATE staff SET sName = :Name, sRole = :Role, sPhone = :Phone WHERE staffID = :id";
                ht.Add(":Name", txtName.Text);
                ht.Add(":Role", cmbRole.Text);
                ht.Add(":Phone", txtPhone.Text);
                ht.Add(":id", id);
            }

            if (mainClass.Oracle(qry, ht) > 0)
            {
                MessageBox.Show("Saved Successfully");
                id = 0;
                txtName.Text = "";
                cmbRole.SelectedIndex = -1;
                txtPhone.Text = "";
                txtName.Focus();
            }
        }
    }
}
