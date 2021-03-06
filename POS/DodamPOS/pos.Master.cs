﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DodamPOS
{
    public partial class pos : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

      
        protected void linkhome_Click(object sender, EventArgs e)
        {
            ChangeMenuColor(linkhome);
        }
        protected void linkpos_Click(object sender, EventArgs e)
        {
            linkpos.Style.Add("color", "#fde5eb");
            linkpos.Style.Add("font-weight", "700");
            linkpos.Style.Add("background", "linear-gradient(180deg, rgba(77,133,102,0.6) 0%,rgba(77,80,133,0.8) 100%)");
            
        }

   
        protected void linkitem_Click(object sender, EventArgs e)
        {
            ChangeMenuColor(linkitem);
           
        }

        protected void linkcustomer_Click(object sender, EventArgs e)
        {
            ChangeMenuColor(linkcustomer);
        }

        protected void linkreport_Click(object sender, EventArgs e)
        {
            ChangeMenuColor(linkreport);
        }

        protected void linkaboutme_Click(object sender, EventArgs e)
        {
            ChangeMenuColor(linkaboutme);
        }

        protected void ChangeMenuColor(LinkButton mbtn)
        {       
            mbtn.Style.Add("color", "#fde5eb");
            mbtn.Style.Add("font-weight", "700");
            mbtn.Style.Add("background", "linear-gradient(180deg, rgba(77,133,102,0.6) 0%,rgba(77,80,133,0.8) 100%)"); 
            
        }

        /*Button btn = new Button();
        public void menubtncolor()
        {
            
            btn.ForeColor = System.Drawing.Color.Red;
            btn.Style.Add("background", "linear-gradient(180deg, rgba(77,133,102,0.6) 0%,rgba(77,80,133,0.8) 100%)");
        }*/
    }
}