using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_6
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            Assignment_6.Service1 obj = new Assignment_6.Service1();
            double distance = obj.distance(TextBox1.Text, TextBox2.Text);
            Label1.Text = Math.Round(distance, 2).ToString();
        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Assignment_6.Service1 obj = new Assignment_6.Service1();
            double lat = Convert.ToDouble(TextBox3.Text);
            double lon = Convert.ToDouble(TextBox4.Text);
            String[] around = obj.aroundYou(lat, lon);
            Label2.Text = around[0];
            Label3.Text = around[1];
            Label4.Text = around[2];
            Label5.Text = around[3];
            Label6.Text = around[4];
        }

        protected void next_Click(object sender, EventArgs e)
        {

        }

        protected void cmdNext_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void cmdPrev_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void cmdNext_Click1(object sender, EventArgs e)
        {

        }
    }
}