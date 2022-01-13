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
using System.Threading.Tasks;

namespace foundry_assessment
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var hc = new HttpClient())
            {
                var response = hc.GetAsync("http://localhost:5000/employees");
                response.Wait();
                var result = response.Result;
                System.Diagnostics.Debug.WriteLine(result);
                if (result.IsSuccessStatusCode)
                {
                    var jsonResponse = result.Content.ReadAsStringAsync().Result;
                    System.Diagnostics.Debug.WriteLine(jsonResponse);
                    var convertedData = JsonConvert.DeserializeObject<List<EmployeeModel>>(jsonResponse);
                    System.Diagnostics.Debug.WriteLine(jsonResponse);
                    gvEmployees.DataSource = convertedData;
                    gvEmployees.DataBind();
                }
                else
                {
                    DataTable dt = new DataTable();
                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();
                }
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvEmployees_UpdateItem(int id)
        {

        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void gvEmployees_DeleteItem(int id)
        {

        }
    }
}