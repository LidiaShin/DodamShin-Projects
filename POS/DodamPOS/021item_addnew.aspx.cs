﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DodamPOS
{
    public partial class _021item_addnew : System.Web.UI.Page
    {

        DataTable CategoryTable { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //CsProvince ProvinceList = new CsProvincelist(ProvinceTable);
                CsItemCat ItemCat = new CsItemCat(CategoryTable);

                try
                {
                    ConnectionClass.GetItemCat(ItemCat);
                    CategoryList.DataSource = ItemCat.itemCategory;
                    CategoryList.DataTextField = ItemCat.itemCategory.Columns["categoryName"].ToString();
                    CategoryList.DataValueField = ItemCat.itemCategory.Columns["categoryCode"].ToString();
                    CategoryList.DataBind();
                   
                }
                finally
                {

                }

                for (int i = 0; i <= 100; i++)
                {
                    qtyList.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }


            }
        }

        const int maxImageSize = 307200;


        // IMAGE UPLOAD TO BLOB
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            var blobName = imageUpload.FileName;

            // LIMIT FILE SIZE + FILE TYPE
            if (imageUpload.PostedFile.ContentLength < maxImageSize &&
                (imageUpload.PostedFile.ContentType == "image/x-png" ||
                imageUpload.PostedFile.ContentType == "image/pjpeg" ||
                imageUpload.PostedFile.ContentType == "image/jpeg" ||
                imageUpload.PostedFile.ContentType == "image/bmp" ||
                imageUpload.PostedFile.ContentType == "image/png" ||
                imageUpload.PostedFile.ContentType == "image/gif")
                ) {

                // APP SETTING TRUE/FALSE
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["useazureblob"].ToString()))
                {
                    // AZURE BLOB ACCESSKEY + CONTAINER NAME
                    string accesskey = ConfigurationManager.AppSettings["azurekey"].ToString();
                    string containerName = "dodamimg".ToLower();


                    // IF NO CONTAINER, CREATE IT!
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accesskey);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                    container.CreateIfNotExists();

                    container.SetPermissions(
                        new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });


                    // UPLOAD...

                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);

                    blockBlob.Properties.ContentType = imageUpload.PostedFile.ContentType;
                    blockBlob.UploadFromStream(imageUpload.FileContent);

                    this.testimg.ImageUrl = $"https://dodamblob.blob.core.windows.net/dodamimg/{blobName}";
                    //this.

                    this.imgurl.Text= $"https://dodamblob.blob.core.windows.net/dodamimg/{blobName}";
                }

                //IF APPSETTING IS FALSE, UPLOAD TO LOCAL..
                else
                {

                }
            }

            // FILETYPE CHECK
            else if(imageUpload.PostedFile.ContentLength < maxImageSize &&
                (imageUpload.PostedFile.ContentType != "image/x-png" ||
                imageUpload.PostedFile.ContentType != "image/pjpeg" ||
                imageUpload.PostedFile.ContentType != "image/jpeg" ||
                imageUpload.PostedFile.ContentType != "image/bmp" ||
                imageUpload.PostedFile.ContentType != "image/png" ||
                imageUpload.PostedFile.ContentType != "image/gif"))
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('Please upload image ');</script>");
            }

            else if(imageUpload.PostedFile.ContentLength >= maxImageSize &&
                (imageUpload.PostedFile.ContentType == "image/x-png" ||
                imageUpload.PostedFile.ContentType == "image/pjpeg" ||
                imageUpload.PostedFile.ContentType == "image/jpeg" ||
                imageUpload.PostedFile.ContentType == "image/bmp" ||
                imageUpload.PostedFile.ContentType == "image/png" ||
                imageUpload.PostedFile.ContentType == "image/gif"))
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('Please upload image less than 300k ');</script>");
            }

            else
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('Please upload appropriate file ');</script>");

            }
        }

        string itemnum { get; set; }
        string itemcat { get; set; }
        string itemname { get; set; }
        string itemqty { get; set; }

        string itempprice { get; set; }
        string itemrprice { get; set; }

        string itemimg { get; set; }
       
        string itemnote { get; set; }
        string itemdate { get; set; }


        // IMAGE URL UPLOAD TO DB
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //itemcat = CategoryList.SelectedValue.ToString();
            itemcat = CategoryList.SelectedItem.Text;
            itemname = iNameBox.Text;

            itemqty = qtyList.SelectedValue.ToString();

            itempprice = pPriceBox.Text;
            itemrprice = rPriceBox.Text;

            itemnote = notebox.Text;

            itemimg = imgurl.Text;
          

            CsItem nitem = new CsItem(itemnum, itemcat, itemname, itempprice, itemrprice, itemnote, itemimg, itemqty,itemdate);

            try
            {
                ConnectionClass.RegisterItem(nitem);
                ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('New Item Registered!');</script>");
                ClearTextBoxes(Page);
                CategoryList.ClearSelection();
                testimg.ImageUrl = "";
            }

            catch
            {
                ClientScript.RegisterStartupScript(GetType(), "message", "<script>alert('Error!');</script>");
            }
        }




        protected void ClearTextBoxes(Control p1)
        {
            foreach (Control ctrl in p1.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox t = ctrl as TextBox;
                    if (t != null)
                    {t.Text = String.Empty;}
                }

                else if (ctrl is DropDownList)
                {
                    DropDownList d = ctrl as DropDownList;
                    if (d != null)
                    {d.ClearSelection();}
                }
                else
                {
                    if (ctrl.Controls.Count > 0)
                    {ClearTextBoxes(ctrl);}
                }
            }
        }





    }
}