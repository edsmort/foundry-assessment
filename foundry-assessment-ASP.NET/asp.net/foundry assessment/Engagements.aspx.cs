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

        public void AddEngagement(string name, string client, string employee, string description="" )
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

        public void DeleteEmployee(string id)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.DeleteAsync("http://localhost:5000/employees/" + id);
                response.Wait();
            }
        }

        protected void btnAddEngagement_Click(object sender, EventArgs e)
        {

        }
    }
}