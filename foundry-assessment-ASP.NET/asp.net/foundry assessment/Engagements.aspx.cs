using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace foundry_assessment
{
    public partial class Engagements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEngagementsAndBind();
                GetEmployeesAndBindDdl();
                GetClientsAndBindDdl();
            }

        }

        public void GetEmployeesAndBindDdl()
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/employees");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = result.Content.ReadAsStringAsync().Result;
                    var convertedData = JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonResponse);
                    ddlEmployee.DataSource = convertedData;
                    ddlEmployee.DataTextField = "Name";
                    ddlEmployee.DataValueField = "Id";
                    ddlEmployee.DataBind();
                }
                else
                {
                    // figure out how to do placeholder text
                }
            }
        }

        public void GetClientsAndBindDdl()
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/clients");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = result.Content.ReadAsStringAsync().Result;
                    var convertedData = JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonResponse);
                    ddlClient.DataSource = convertedData;
                    ddlClient.DataTextField = "Name";
                    ddlClient.DataValueField = "Id";
                    ddlClient.DataBind();
                }
                else
                {
                    // figure out how to do placeholder text
                }
            }
        }

        public string GetEmployeeName(string id)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/employees/" + id);
                response.Wait();
                var jsonResponse = response.Result.Content.ReadAsStringAsync().Result;
                var convertedData = JsonConvert.DeserializeObject<EmployeeModel>(jsonResponse);
                return convertedData.Name;
            }
        }

        public string GetClientName(string id)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/clients/" + id);
                response.Wait();
                var jsonResponse = response.Result.Content.ReadAsStringAsync().Result;
                var convertedData = JsonConvert.DeserializeObject<EmployeeModel>(jsonResponse);
                return convertedData.Name;
            }
        }

        public void GetEngagementsAndBind()
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/engagements");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = result.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine(jsonResponse);
                    var convertedData = JsonConvert.DeserializeObject<List<EngagementModel>>(jsonResponse);
                    foreach (EngagementModel engagement in convertedData)
                    {
                        engagement.Employee = GetEmployeeName(engagement.Employee);
                        engagement.Client = GetClientName(engagement.Client);
                    }
                    gvEngagements.DataSource = convertedData;
                    gvEngagements.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvEngagements.DataSource = dt;
                    gvEngagements.DataBind();
                }
            }
        }

        public void AddEngagement(string name, string client, string employee, string description )
        {
            using (var hc = new HttpClient())
            {
                var engagement = new
                {
                    name = name,
                    client = client,
                    employee = employee,
                    description = description
                };
                string updated = JsonConvert.SerializeObject(engagement);
                HttpContent payload = new StringContent(updated, System.Text.Encoding.UTF8, "application/json");
                var response = hc.PostAsync("http://localhost:5000/engagements/", payload);
                response.Wait();
            }
        }

        public void EditEngagement(string id, string newName, string newDescription)
        {
            using (var hc = new HttpClient())
            {
                var engagement = new
                {
                    name = newName,
                    description = newDescription
                };
                string updated = JsonConvert.SerializeObject(engagement);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                var response = hc.PutAsync("http://localhost:5000/engagements/" + id, payload);
                response.Wait();
            }
        }

        public void DeleteEngagement(string id)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.DeleteAsync("http://localhost:5000/engagements/" + id);
                response.Wait();
            }
        }

        public void EndEngagement(string id)
        {
            using (var hc = new HttpClient())
            {
                System.Diagnostics.Debug.WriteLine(id);
                HttpContent payload = new StringContent("", Encoding.UTF8, "application/json");
                var response = hc.PutAsync("http://localhost:5000/engagements/" + id + "/end", payload);
                response.Wait();
                System.Diagnostics.Debug.WriteLine(response.Result);
            }
        }

        protected void btnAddEngagement_Click(object sender, EventArgs e)
        {
            AddEngagement(txtEngagement.Text, ddlClient.SelectedValue, ddlEmployee.SelectedValue, txtDescription.Text);
            txtEngagement.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlClient.SelectedIndex = 0;
            ddlEmployee.SelectedIndex = 0;
            GetEngagementsAndBind();
        }

        protected void gvEngagements_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEngagements.EditIndex = e.NewEditIndex;
            GetEngagementsAndBind();
        }

        protected void gvEngagements_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEngagements.EditIndex = -1;
            GetEngagementsAndBind();
        }

        protected void gvEngagements_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEngagements.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            TextBox txtName = (TextBox)row.Cells[1].Controls[0];
            string name = txtName.Text;
            TextBox txtDescription = (TextBox)row.Cells[4].Controls[0];
            string description = txtDescription.Text;
            EditEngagement(id, name, description);
            gvEngagements.EditIndex = -1;
            GetEngagementsAndBind();
        }

        protected void gvEngagements_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvEngagements.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            DeleteEngagement(id);
            GetEngagementsAndBind();
        }
        
        protected void EndEngagement(object sender, GridViewCommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("calling endengagement");
            int index = Convert.ToInt32(((GridViewRow)sender).RowIndex);
            GridViewRow row = gvEngagements.Rows[index];
            string id = row.Cells[0].Text;
            EndEngagement(id);
            GetEngagementsAndBind();
        }

        protected void gvEngagements_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="EndEngagement")
            {
                System.Diagnostics.Debug.WriteLine("calling endengagement");
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvEngagements.Rows[index];
                string id = row.Cells[0].Text;
                EndEngagement(id);
                GetEngagementsAndBind();
            }
        }
    }
}