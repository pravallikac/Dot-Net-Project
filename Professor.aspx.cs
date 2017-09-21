﻿using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class Professor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["New"] != null)
        {
            ProfessorName.Text = Session["New"].ToString();
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

        if (!this.IsPostBack)
        {
            string constr = ConfigurationManager.ConnectionStrings["DeptConnections"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT courseID FROM usercourse where username='"+Session["New"]+"'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    ddlcourses.DataSource = cmd.ExecuteReader();
                    ddlcourses.DataTextField = "courseID";
                    ddlcourses.DataValueField = "courseID";
                    ddlcourses.DataBind();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT distinct(courseID) FROM usercourse where username='" + Session["New"] + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    ddlReport.DataSource = cmd.ExecuteReader();
                    ddlReport.DataTextField = "courseID";
                    ddlReport.DataValueField = "courseID";
                    ddlReport.DataBind();
                    con.Close();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT distinct(courseID) FROM usercourse where username='" + Session["New"] + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    ddlReportview.DataSource = cmd.ExecuteReader();
                    ddlReportview.DataTextField = "courseID";
                    ddlReportview.DataValueField = "courseID";
                    ddlReportview.DataBind();
                    con.Close();
                }
            }
            ddlcourses.Items.Insert(0, new ListItem("--Course Details--", "0"));
            ddlReport.Items.Insert(0, new ListItem("Create A Report", "0"));
            ddlReportview.Items.Insert(0, new ListItem("View Report", "0"));
        }

    }
    protected void ProfessorLogOut_Click1(object sender, EventArgs e)
    {
        Session["New"] = null;
        Response.Redirect("Default.aspx");
    }
   



    protected void coursebtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("coursedetail.aspx?Value=" + ddlcourses.SelectedValue);
    }
    protected void reportbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reportform.aspx?RValue=" + ddlReport.SelectedValue);
    }

    protected void reportviewbtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportView.aspx?RVValue=" + ddlReportview.SelectedValue);
    }
}