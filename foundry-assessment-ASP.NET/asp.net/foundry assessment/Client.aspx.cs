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
                System.Diagnostics.Debug.WriteLine(updated);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(payload);
                var response = hc.PostAsync("http://localhost:5000/clients/", payload);
                response.Wait();
                var result = response.Result.Content.ReadAsStringAsync().Result;
                System.Diagnostics.Debug.WriteLine(result);
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
                System.Diagnostics.Debug.WriteLine(updated);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                System.Diagnostics.Debug.WriteLine(payload);
                var response = hc.PutAsync("http://localhost:5000/clients/" + id, payload);
                response.Wait();
                var result = response.Result.Content.ReadAsStringAsync().Result;
                System.Diagnostics.Debug.WriteLine(result);
            }
        }

        protected void gvClients_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvClients.EditIndex = e.NewEditIndex;
            GetClientsAndBind();
        }

        protected void gvClients_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvClients_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvClients.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            TextBox txtName = (TextBox)row.Cells[1].Controls[0];
            string name = txtName.Text;
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(name);
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
            GetClientsAndBind();
        }
    }
}