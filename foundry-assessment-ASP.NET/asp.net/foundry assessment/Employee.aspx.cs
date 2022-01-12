using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using foundry_assessment.Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace foundry_assessment
{
    public partial class Employee : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            using (HttpResponseMessage response = await ApiHelper.hc.GetAsync("employees"))
            {
                if (response.IsSuccessStatusCode)
                {
                    foundry_assessment.Models.Employee employee = await response.Content.ReadAsAsync<foundry_assessment.Models.Employee>();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                }
            }


        }
    }
}