using Resturant_POS.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant_POS.View
{
    public partial class frmCategoriesView : SampleView
    {
        public frmCategoriesView()
        {
            InitializeComponent();
        }

        public void GetData()
        {
            string qry = "Select * From category where catName like '%" + txtSearch.Text + "%'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvName);

            mainClass.LoadData(qry, gunaDataGridView1, lb);
        }

        private void frmCategoriesView_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            frmCategoriesAdd frm = new frmCategoriesAdd();
            frm.ShowDialog();
            GetData();
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gunaDataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmCategoriesAdd frm = new frmCategoriesAdd();
                frm.id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = Convert.ToString(gunaDataGridView1.CurrentRow.Cells["dgvName"].Value);
                frm.ShowDialog();
                GetData();
            }

            if (gunaDataGridView1.CurrentCell.OwningColumn.Name == "dgvdel")
            {

                string messageDel = "Are you sure you want to delete it?";
                string title = "Delete Category";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(messageDel, title, buttons);

                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(gunaDataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "DELETE FROM Category WHERE catID = :id";
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
