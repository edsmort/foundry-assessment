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

namespace foundry_assessment
{
    public partial class Engagements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEngagementsAndBind();
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
    }
}