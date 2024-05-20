using Resturant_POS.Model;
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

namespace Resturant_POS.View
{
    public partial class frmStaffView : SampleView
    {
        public frmStaffView()
        {
            InitializeComponent();
        }
        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        public void GetData()
        {
            string qry = "Select * From staff where sName like '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvRole);
            

            mainClass.LoadData(qry, gunaDataGridView1, lb);
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmStaffAdd frm = new frmStaffAdd();
            frm.ShowDialog();
            GetData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }
        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            GetData();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gunaDataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {

                frmStaffAdd frm = new frmStaffAdd();
                frm.id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.txtPhone.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["dgvPhone"].Value);
             //   frm.cmbRole.SelectedIndex = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells["dgvRole"].Value);
                frm.ShowDialog();
                GetData();
            }

            if (gunaDataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {

                string messageDel = "Are you sure you want to delete it?";
                string title = "Delete Staff";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(messageDel, title, buttons);

                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "DELETE FROM Category WHERE staffID = :id";
                    Hashtable ht = new Hashtable();
                    ht.Add(":id", id);
                    mainClass.Oracle(qry, ht);
                    MessageBox.Show("Deleted Successfully");
                    GetData();
                }
                else
                {
                    //   this.Close();
                    foreach (Form frm in Application.OpenForms)
                    {
                        if (frm.Text == title)
                        {
                            frm.Close();
                            break;
                        }
                    }

                }
            }
        }
    }
}
