using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Resturant_POS
{
    internal class mainClass
    {
        private static readonly string con_string = "DATA SOURCE=localhost:1521/XE;DBA PRIVILEGE=SYSDBA;TNS_ADMIN=C:\\Users\\hp\\Oracle\\network\\admin;USER ID=SYS; Password=#Yeabfuture26875499";
        public static OracleConnection con = new OracleConnection(con_string);

        public static bool IsValidUSer(string user, string pass)
        {
            bool isValid = false;

            string qry = @"Select * from managers where username = '" + user + "' and upass = '" + pass + "' ";
            OracleCommand cmd = new OracleCommand(qry, con);
            DataTable dt = new DataTable();
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            }

            return isValid;
        }


        // creating a property for username

        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }

        // Adding Methods to operate the CRUD

        public static int Oracle(string qry, Hashtable ht)
        {
            int res = 0;

            //try
            //{
            OracleCommand cmd = new OracleCommand(qry, con);
            cmd.CommandType = CommandType.Text;

            foreach (DictionaryEntry item in ht)
            {
                OracleParameter param = new OracleParameter(item.Key.ToString(), item.Value);
                cmd.Parameters.Add(param);
            }
            if (con.State == ConnectionState.Closed) { con.Open(); }
            res = cmd.ExecuteNonQuery();
            if (con.State == ConnectionState.Open) { con.Close(); }
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    con.Close();
            //}

            return res;
        }

        // Loading data from db

        public static void LoadData(String qry, DataGridView gv,ListBox lb)
        {

            // Showing the Serial no in grid view

            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);



            //try
            //{
            OracleCommand cmd = new OracleCommand(qry, con);
            cmd.CommandType = CommandType.Text;
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < lb.Items.Count; i++)
            {
                string ColNam1 = ((DataGridViewColumn)lb.Items[i]).Name;
                gv.Columns[ColNam1].DataPropertyName = dt.Columns[i].ToString();
            }

            gv.DataSource = dt;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    con.Close();
            //}
        }

        private static void gv_CellFormatting(Object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI.WinForms.GunaDataGridView gv = (Guna.UI.WinForms.GunaDataGridView)sender;
            int count = 0;

            foreach (DataGridViewRow row in gv.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }
    }


}