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
using System.Text;

namespace foundry_assessment
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetEmployeesAndBind();
            }

        }

        public void GetEmployeesAndBind()
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

        public void AddEmployee(string newName)
        {
            using (var hc = new HttpClient())
            {
                var employee = new
                {
                    name = newName
                };
                string updated = JsonConvert.SerializeObject(employee);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                var response = hc.PostAsync("http://localhost:5000/employees/", payload);
                response.Wait();
                var result = response.Result.Content.ReadAsStringAsync().Result;
            }
        }

        public void EditEmployee(string id, string newName)
        {
            using (var hc = new HttpClient())
            {
                var employee = new
                {
                    name = newName
                };
                string updated = JsonConvert.SerializeObject(employee);
                HttpContent payload = new StringContent(updated, Encoding.UTF8, "application/json");
                var response = hc.PutAsync("http://localhost:5000/employees/" + id, payload);
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

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployee(addName.Text);
            addName.Text = string.Empty;
            GetEmployeesAndBind();
        }

        protected void gvEmployees_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmployees.EditIndex = e.NewEditIndex;
            GetEmployeesAndBind();
        }

        protected void gvEmployees_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmployees.EditIndex = -1;
            GetEmployeesAndBind();
        }

        protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvEmployees.Rows[e.RowIndex].Cells[0].Text;
            DeleteEmployee(id);
            GetEmployeesAndBind();
        }

        protected void gvEmployees_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvEmployees.Rows[e.RowIndex];
            string id = row.Cells[0].Text;
            TextBox txtName = (TextBox)row.Cells[1].Controls[0];
            string name = txtName.Text;
            EditEmployee(id, name);
            gvEmployees.EditIndex = -1;
            GetEmployeesAndBind();
        }

        private DataTable GetEmployees()
        {
            DataTable dt = new DataTable();
            if (gvEmployees.HeaderRow != null)
            {
                for (int i = 0; i < gvEmployees.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(gvEmployees.HeaderRow.Cells[i].Text);
                }
            }

            foreach (GridViewRow row in gvEmployees.Rows)
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
            GetEmployeesAndBind(); 
            DataTable dt = GetEmployees();
            DataView dv = new DataView(dt);
            string SearchExpression = null;

            if (!String.IsNullOrEmpty(txtSearch.Text))
            {
                SearchExpression = String.Format("{0} '%{1}%'", gvEmployees.SortExpression, txtSearch.Text);
                System.Diagnostics.Debug.WriteLine(SearchExpression);
                dv.RowFilter = "Name like" + SearchExpression;
                gvEmployees.DataSource = dv;
                gvEmployees.DataBind();
            }
            else
            {
                GetEmployeesAndBind();
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchText();
        }
    }
}