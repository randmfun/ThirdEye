using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace WebClient
{
    public partial class BatchList1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/BatchList.rdlc");
                BatchList dsCustomers = GetData("select * from aspnet_Users");
                ReportDataSource datasource = new ReportDataSource("BatchList", dsCustomers.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
        }

        private BatchList GetData(string query)
        {
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlCommand cmd = new SqlCommand(query);
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    sda.SelectCommand = cmd;
                    
                    using (BatchList dsCustomers = new BatchList())
                    {
                        sda.Fill(dsCustomers, "DataTable1");
                        return dsCustomers;
                    }
                }
            }
        }
    }
}