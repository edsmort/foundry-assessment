using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace foundry_assessment
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClientsAndBind();
            }
        } 
        public void GetClientsAndBind()
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/clients");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = result.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine(jsonResponse);
                    var convertedData = JsonConvert.DeserializeObject<List<ClientModel>>(jsonResponse);
                    gvClients.DataSource = convertedData;
                    gvClients.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvClients.DataSource = dt;
                    gvClients.DataBind();
                }
            }
        }

        public void AddClient(string newName)
        {
            using (var hc = new HttpClient())
            {
                var client = new
                {
                    name = newName
                };
                string updated = JsonConvert.SerializeObject(client);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                var response = hc.PostAsync("http://localhost:5000/clients/", payload);
                response.Wait();
            }
        }

        public void EditClient(string id, string newName)
        {
            using (var hc = new HttpClient())
            {
                var client = new
                {
                    name = newName
                };
                string updated = JsonConvert.SerializeObject(client);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                var response = hc.PutAsync("http://localhost:5000/clients/" + id, payload);
                response.Wait();
            }
        }

        public void DeleteClient(string id)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.DeleteAsync("http://localhost:5000/clients/" + id);
                response.Wait();
            }
        }

        protected void gvClients_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClients.EditIndex = e.NewEditIndex;
            GetClientsAndBind();
        }

        protected void gvClients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvClients.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            DeleteClient(id);
            GetClientsAndBind();
        }

        protected void gvClients_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvClients.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            TextBox txtName = (TextBox)row.Cells[1].Controls[0];
            string name = txtName.Text;
            EditClient(id, name);
            gvClients.EditIndex = -1;
            GetClientsAndBind();
        }

        protected void gvClients_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvClients.EditIndex = -1;
            GetClientsAndBind();
        }

        protected void btnAddClient_Click(object sender, EventArgs e)
        {
            AddClient(addName.Text);
            addName.Text = string.Empty;
            GetClientsAndBind();
        }

        private DataTable GetClients()
        {
            DataTable dt = new DataTable();
            if (gvClients.HeaderRow != null)
            {
                for (int i = 0; i <gvClients.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(gvClients.HeaderRow.Cells[i].Text);
                }
            }

            foreach (GridViewRow row in gvClients.Rows)
            {
                DataRow dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private void SearchText()
        {
            GetClientsAndBind();
            DataTable dt = GetClients();
            DataView dv = new DataView(dt);
            string SearchExpression = null;

            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                SearchExpression = String.Format("{0} '%{1}%'", gvClients.SortExpression, txtSearch.Text);
                System.Diagnostics.Debug.WriteLine(SearchExpression);
                dv.RowFilter = "Name like" + SearchExpression;
                gvClients.DataSource = dv;
                gvClients.DataBind();
            } else
            {
                GetClientsAndBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }
    }
}