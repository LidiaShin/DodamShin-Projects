﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace DodamPOS
{
    public class ConnectionClass
    {
        private static SqlConnection cntString;
        private static SqlCommand cmdString;
        private static SqlCommand cmdStringA;
        private static SqlCommand cmdStringB;


        static ConnectionClass()
        {
            cntString = new SqlConnection(ConfigurationManager.ConnectionStrings["dodampos"].ConnectionString);
            //cntString = new SqlConnection(ConfigurationManager.ConnectionStrings["dodamposlocal"].ConnectionString);
        }

     public static bool SignIn(CsUserlist userinfo)
        {
            string aQuery = string.Format(@"SELECT count(username) from userlist where username=('{0}') and password=('{1}')",
            userinfo.username, userinfo.password);

            cmdString = new SqlCommand(aQuery, cntString);

            cntString.Open();

            try { 
                int count = (int)cmdString.ExecuteScalar();

                cntString.Close();

                return count > 0;
            }
            finally
            {
                cntString.Close();
            }
        }

        // Get Province list into dropdownlist 
        // 031 customer_addnew.aspx
        public static void GetPVlist(CsProvincelist pv)
        {
            string bQuery = string.Format(@"SELECT * FROM tblProvince ORDER BY provinceCode;");
            cmdString = new SqlCommand(bQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);

                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dt);
                ds.Tables.Add(dt);

                pv.Provinces = ds.Tables[0];
            }

            finally
            {
                cntString.Close();
            }
        }

        public static void RegistrationCustomer(CsCustomer customer)
        {
            string cQuery = string.Format(@"INSERT INTO tblCustomer 
            (customerID,fName,lName,Email,Phone,Address,City,Province,PostalCode,Note,RegisterDate)
            VALUES(NEXT VALUE FOR SQcustomer,(N'{0}'),(N'{1}'),('{2}'),('{3}'),(N'{4}'),(N'{5}'),('{6}'),('{7}'),(N'{8}'),GETDATE());",
            customer.cFirstName,customer.cLastName,customer.cEmail,customer.cPhone,customer.cAddress,customer.cCity,customer.cProvince,customer.cPostalCode,customer.cNote);

            cmdString = new SqlCommand(cQuery, cntString);

            try
            {
                cntString.Open();
                cmdString.ExecuteNonQuery();
            }

            finally
            {
                cntString.Close();
            }
        }


        public static void SendRegisterEmail(CsRegisterEmail cmail)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();

            try
            {
                msg.Subject = cmail.emailSubject;
                msg.Body = cmail.emailContent;
                msg.From= new MailAddress("dodam.KGHS@gmail.com");
                msg.To.Add(cmail.emailAddress);


                msg.IsBodyHtml = true;



                client.Host = "smtp.gmail.com";
                System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("dodam.KGHS@gmail.com", "1142311423");

                client.Port = int.Parse("587"); //if using SSL 465
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);



                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
                //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                ////to authenticate we set the username and password properites on the SmtpClient
                //smtp.Credentials = new NetworkCredential("dodam.KGHS@gmail.com", "1142311423");//no need to mention here?
                //smtp.Send(msg);

            }

            finally
            {

            }
        }


        public static void GetCustomerList(CsCustomerlist customertable)
        {
            string dQuery = string.Format(@"SELECT CUSTOMERID AS NO,(fName + '  '+ lName ) AS NAME,
EMAIL AS 'E-MAIL',CITY AS 'CITY',PROVINCE AS 'PROVINCE',
CONVERT(VARCHAR, REGISTERDATE,106) AS 'REGISTER DATE' FROM TBLCUSTOMER
ORDER BY CUSTOMERID DESC;");
            cmdString = new SqlCommand(dQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
  
                DataSet ds = new DataSet();
                da.Fill(ds);            
                customertable.customerList = ds.Tables[0];
            }

            finally
            {
                cntString.Close();
            }        
        }

        // Get Province list into dropdownlist 
        // 032 customer_seedetail.aspx page open
        public static void GetCustomerDetail(CsCustomer thecustomer)
        {
            string eQuery = string.Format(@"SELECT * FROM tblCustomer WHERE customerID = ('{0}');",thecustomer.cNumber);
            cmdString = new SqlCommand(eQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dt);
                ds.Tables.Add(dt);

                thecustomer.cFirstName = dt.Rows[0]["fName"].ToString();
                thecustomer.cLastName= dt.Rows[0]["lName"].ToString();
                thecustomer.cAddress= dt.Rows[0]["Address"].ToString();
                thecustomer.cCity= dt.Rows[0]["City"].ToString();

                thecustomer.cProvince=dt.Rows[0]["Province"].ToString();
                thecustomer.cPhone= dt.Rows[0]["Phone"].ToString();
                thecustomer.cEmail = dt.Rows[0]["Email"].ToString();
                thecustomer.cNote= dt.Rows[0]["Note"].ToString();
                thecustomer.cRegisterDate= dt.Rows[0]["RegisterDate"].ToString();
                thecustomer.cPostalCode= dt.Rows[0]["PostalCode"].ToString();
            }

            finally
            {
                cntString.Close();
            }
        }

        // Get Province list into dropdownlist 
        // 032 customer_seedetail.aspx press save button

        public static void UpdateCustomerDetail(CsCustomer thiscustomer)
        {
            string fQuery = string.Format(@"UPDATE tblCustomer SET fName=(N'{0}'),lName=(N'{1}'),
            Address=(N'{2}'),City=(N'{3}'),Province=('{4}'),Phone=('{5}'),Email=('{6}'),PostalCode=('{7}'),Note=(N'{8}') WHERE customerID=('{9}');",
            thiscustomer.cFirstName, thiscustomer.cLastName, thiscustomer.cAddress, thiscustomer.cCity,
            thiscustomer.cProvince, thiscustomer.cPhone, thiscustomer.cEmail, thiscustomer.cPostalCode, thiscustomer.cNote, thiscustomer.cNumber);


            cmdString = new SqlCommand(fQuery, cntString);

            try
            {
                cntString.Open();
                cmdString.ExecuteNonQuery();
            }

            finally
            {
                cntString.Close();
            }
        }


        public static void SearchCustomerByName(CsFilteredCustomerList fCustomerlist)
        {
            string gQuery = string.Format(@"SELECT customerID AS NO,(fName + '  '+ lName ) AS NAME, email AS 'E-MAIL',city AS 'CITY',
            Province AS 'PROVINCE',convert(varchar, RegisterDate,106) AS 'REGISTER DATE' FROM tblCustomer WHERE lOWER(fName+lName) LIKE LOWER(N'%{0}%');", fCustomerlist.kWordName);
            cmdString = new SqlCommand(gQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds);

                //ds.Tables.Add(dt);

                fCustomerlist.fCustomerList = ds.Tables[0];
            }
            catch
            {

            }

            finally
            {
                cntString.Close();
            }
        }




        public static void SearchCustomerByEmail(CsFilteredCustomerList fCustomerlist)
        {
            string hQuery = string.Format(@"SELECT customerID AS NO,(fName + '  '+ lName ) AS NAME, email AS 'E-MAIL',city AS 'CITY',Province AS 'PROVINCE',
convert(varchar, RegisterDate,106) AS 'REGISTER DATE' FROM tblCustomer WHERE lOWER(Email) LIKE LOWER('%{0}%');", fCustomerlist.kWordEmail);
            cmdString = new SqlCommand(hQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds);
                //ds.Tables.Add(dt);
                fCustomerlist.fCustomerList = ds.Tables[0];
            }
            catch
            {

            }

            finally
            {
                cntString.Close();
            }
        }

   // 021 item_add new page    
        public static void RegisterItem(CsItem newItem)
        {
            string iQuery = string.Format(@"INSERT INTO tblItem (itemID,itemCategory,itemName,itemPPrice,itemRPrice,itemNote,itemImage,itemQty,itemRegisterDate) 
            VALUES (NEXT VALUE FOR SQitem,(N'{0}'),(N'{1}'),('{2}'),('{3}'),(N'{4}'),(N'{5}'),('{6}'),GETDATE());", 
            newItem.itemCategory, newItem.itemName, newItem.itemPPrice, newItem.itemRPrice, newItem.itemNote, newItem.itemImage, newItem.itemQuantity);

            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();
                cmdString.ExecuteNonQuery();
                cntString.Close();
            }

            finally
            {
                cntString.Close();
            }

        }


        public static void GetItemCat(CsItemCat itemcat)
        {
            string bQuery = string.Format(@"SELECT * FROM tblCategory;");
            cmdString = new SqlCommand(bQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds);
                itemcat.itemCategory = ds.Tables[0];

                
            }

            finally
            {
                cntString.Close();
            }
        }



        //020 item page
        public static void GetItemList(CsItemlist itemtable)
        {
            string kQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
PurchasePrice='$'+CONVERT(VARCHAR,itemPPrice),RetailPrice='$'+CONVERT(VARCHAR,itemRPrice),
itemQty AS Quantity, convert(varchar, itemRegisterDate,106) AS 'REGISTER DATE' from tblItem order by itemRegisterDate DESC;");

            cmdString = new SqlCommand(kQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);

                DataSet ds = new DataSet();
                da.Fill(ds);
                itemtable.itemList = ds.Tables[0];
            }

            finally
            {
                cntString.Close();
            }
        }


        public static void SearchItemByName(CsFilteredItemList flist)
        {
            string iQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
itemPPrice AS PurchasePrice,itemRPrice AS RetailPrice,
itemQty AS Quantity, convert(varchar, itemRegisterDate,106) AS 'REGISTER DATE'
from tblItem WHERE lOWER(itemName) LIKE LOWER(N'%{0}%') ORDER BY itemRegisterDate DESC;", flist.keyiName);
            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds);

                //ds.Tables.Add(dt);

                flist.fiTable = ds.Tables[0];
            }
            catch
            {

            }

            finally
            {
                cntString.Close();
            }
        }



        public static void SearchItemByNote(CsFilteredItemList flist)
        {
            string iQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
itemPPrice AS PurchasePrice,itemRPrice AS RetailPrice,
itemQty AS Quantity, convert(varchar, itemRegisterDate,106) AS 'REGISTER DATE'
from tblItem WHERE lOWER(itemNote) LIKE LOWER(N'%{0}%') ORDER BY itemRegisterDate DESC;", flist.keyiNote);
            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                //DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(ds);

                //ds.Tables.Add(dt);

                flist.fiTable = ds.Tables[0];
            }
            catch
            {

            }

            finally
            {
                cntString.Close();
            }
        }


        // POS Page

        public static void GetItemAll(CsItemlist itable)
        {
            // string kQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
            //itemPPrice AS PurchasePrice,itemRPrice AS RetailPrice,
            //itemQty AS Quantity,itemImage AS URL from tblItem order by itemID;");

           string kQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
           PurchasePrice='$'+CONVERT(VARCHAR,itemPPrice),RetailPrice='$'+CONVERT(VARCHAR,itemRPrice),
           itemQty AS Quantity,itemImage AS URL from tblItem ORDER BY itemRegisterDate DESC;");

            cmdString = new SqlCommand(kQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);

                DataSet ds = new DataSet();
                da.Fill(ds);
                itable.itemList = ds.Tables[0];
            }

            finally
            {
                cntString.Close();
            }
        }

        //PurchasePrice='$'+CONVERT(VARCHAR, itemPPrice),RetailPrice='$'+CONVERT(VARCHAR, itemRPrice)
        public static void GetItemByCat(CsFilteredItemList fitem)
        {
            string kQuery = string.Format(@"SELECT itemID AS NO,itemCategory AS CATEGORY,itemName AS NAME, 
           PurchasePrice='$'+CONVERT(VARCHAR,itemPPrice),RetailPrice='$'+CONVERT(VARCHAR,itemRPrice),
           itemQty AS Quantity,itemImage AS URL from tblItem WHERE itemCategory=('{0}') ORDER BY itemRegisterDate DESC;", fitem.keyiName);

            cmdString = new SqlCommand(kQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);

                DataSet ds = new DataSet();
                da.Fill(ds);
                fitem.fiTable= ds.Tables[0];
            }

            finally
            {
                cntString.Close();
            }
        }












        // 022 item_seedetail page
        // page load

        public static void GetItemDetail(CsItem itemdetail)
        {
            string iQuery = string.Format(@"SELECT itemID,itemCategory, itemName, 
            FORMAT(itemPPrice,'C','en-us') AS itemPPrice, FORMAT(itemRPrice,'C','en-us') AS itemRPrice, itemNote,itemImage,itemQty,itemRegisterDate
            FROM tblItem WHERE itemID = ('{0}');", itemdetail.itemNumber);
            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dt);
                ds.Tables.Add(dt);


                itemdetail.itemCategory = dt.Rows[0]["itemCategory"].ToString();
                itemdetail.itemName = dt.Rows[0]["itemName"].ToString();
                itemdetail.itemQuantity= dt.Rows[0]["itemQty"].ToString();
                itemdetail.itemNote= dt.Rows[0]["itemNote"].ToString();

                itemdetail.itemPPrice= dt.Rows[0]["itemPPrice"].ToString();
                itemdetail.itemRPrice= dt.Rows[0]["itemRPrice"].ToString();

                itemdetail.itemImage= dt.Rows[0]["itemImage"].ToString();
               
            }

            finally
            {
                cntString.Close();
            }
        }





        // 010 POS.ASPX

        // GET ITEM DETAIL WITHOUT FORMATTNG TO CURRENCY (NEED TO CONVERT TO DOUBLE)
        public static void GetItemDetails(CsItem itemdetail)
        {
            string iQuery = string.Format(@"SELECT itemID,itemCategory, itemName, 
            itemPPrice,itemRPrice, itemNote,itemImage,itemQty,itemRegisterDate
            FROM tblItem WHERE itemID = ('{0}');", itemdetail.itemNumber);
            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dt);
                ds.Tables.Add(dt);


                itemdetail.itemCategory = dt.Rows[0]["itemCategory"].ToString();
                itemdetail.itemName = dt.Rows[0]["itemName"].ToString();
                itemdetail.itemQuantity = dt.Rows[0]["itemQty"].ToString();
                itemdetail.itemNote = dt.Rows[0]["itemNote"].ToString();

                itemdetail.itemPPrice = dt.Rows[0]["itemPPrice"].ToString();
                itemdetail.itemRPrice = dt.Rows[0]["itemRPrice"].ToString();

                itemdetail.itemImage = dt.Rows[0]["itemImage"].ToString();

            }

            finally
            {
                cntString.Close();
            }
        }

        public static void UpdateItemDetail(CsItem olditem)
        {
            string iQuery = string.Format(@"UPDATE tblItem SET itemCategory=(N'{0}'),itemName=(N'{1}'),
            itemPPrice=('{2}'),itemRPrice=('{3}'),itemNote=(N'{4}'),itemImage=(N'{5}'),itemQty=('{6}') 
			WHERE itemID=('{7}');",
            olditem.itemCategory, olditem.itemName, olditem.itemPPrice, olditem.itemRPrice, olditem.itemNote,
            olditem.itemImage,olditem.itemQuantity, olditem.itemNumber);
          
            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
                cntString.Open();
                cmdString.ExecuteNonQuery();
            }

            finally
            {
                cntString.Close();
            }
        }

        // POS PAGE
        public static void CreateOrder(CsOrder neworder)
        {
            string aQuery = string.Format(@"INSERT INTO tblOrder(orderNO, orderCustomerNO, orderDate) 
            VALUES (NEXT VALUE FOR SQOrderNum,'{0}',getdate());", neworder.CustomerNumber);

            string bQuery = string.Format(@"SELECT current_value FROM sys.sequences WHERE name = 'SQOrderNum'; ");



            cmdStringA = new SqlCommand(aQuery, cntString);
            cmdStringB = new SqlCommand(bQuery, cntString);

            try
            {
                // ORDER INSERT
                cntString.Open();
                cmdStringA.ExecuteNonQuery();

                // GET ORDERNUMBER (CURVAL)
                SqlDataAdapter da = new SqlDataAdapter(cmdStringB);
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                da.Fill(dt);
                ds.Tables.Add(dt);


                neworder.OrderNumber = dt.Rows[0]["current_value"].ToString();
            }

            finally
            {
                cntString.Close();
            }
        }

        public static void UploadOrderItem(DataTable ItemTable)
        {
            try
            {
                //SqlBulkCopy bulkCopy = new SqlBulkCopy(cntString);
                SqlBulkCopy bulkCopy = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["dodampos"].ConnectionString, SqlBulkCopyOptions.FireTriggers);
                cntString.Open();

                bulkCopy.ColumnMappings.Add("orderno", "orderID");
                bulkCopy.ColumnMappings.Add("No", "orderItemID");
                bulkCopy.ColumnMappings.Add("unitprice", "orderItemUPrice");
                bulkCopy.ColumnMappings.Add("qty", "orderItemQty");
                bulkCopy.ColumnMappings.Add("amount", "orderItemPrice");
                bulkCopy.ColumnMappings.Add("tax", "orderItemTax");


                bulkCopy.DestinationTableName = "tblOrderItem";
                bulkCopy.WriteToServer(ItemTable);

            }
            finally
            {
                cntString.Close();
            }

        }



        public static void DisplayReceipt(CsReceipt receipt)
        {

            string aQuery = string.Format
(@"SELECT orderItemID AS CODE,ITEMNAME AS 'ITEM NAME', 
PRICE='$'+ CONVERT(VARCHAR,OrderItemUPrice),
orderItemQty AS 'QTY',
AMOUNT='$'+ CONVERT(VARCHAR,OrderItemPrice),
TAX='$'+ CONVERT(VARCHAR,OrderItemTax)
FROM TBLORDERITEM JOIN TBLITEM ON ORDERITEMID = ITEMID WHERE ORDERID=('{0}');

SELECT SUM(OrderitemQty) AS TOTALQTY,
TOTALAMOUNT='$'+ CONVERT(VARCHAR,SUM(OrderItemPrice)),
TOTALTAX='$'+ CONVERT(VARCHAR,SUM(OrderItemTax)),
GRANDTOTAL='$'+CONVERT(VARCHAR,(SUM(orderitemtax) + SUM(orderItemPrice))),
CONVERT(VARCHAR(20),GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time',100) AS ORDERDATE
FROM tblOrderItem WHERE ORDERID = ('{0}')", receipt.OrderNum);

            cmdString = new SqlCommand(aQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataSet ds = new DataSet();
                da.Fill(ds);

                receipt.ReceiptTable = ds.Tables[0];
                receipt.TotalTable = ds.Tables[1];
                
            }

            finally
            {
                cntString.Close();
            }
        }


        // SALES REPORT

        public static void DisplayReport(CsDailyReport report)
        {

string rQuery = string.Format
(@"SELECT 
ISNULL((I.itemCategory),'TOTAL') AS CATEGORY,
SUM(O.orderItemQty) AS QTY,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE + O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO 
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) = CAST(GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY I.ITEMCATEGORY
WITH ROLLUP;



SELECT TOP 10
C.ITEMNAME AS NAME,
O.orderItemUPrice AS PRICE,
SUM(O.ORDERITEMQTY) AS QTY,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE + O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O 
JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO
JOIN TBLITEM C ON O.orderItemID=C.ITEMID 
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) = CAST(GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY C.ITEMNAME,O.orderItemUPrice
ORDER BY SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY) DESC;

SELECT TOP 5
OT.orderCustomerNO AS NO,
(C.fName + ' ' + C.lName) AS NAME,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE + O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O 
JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO
JOIN TBLCUSTOMER C ON OT.ORDERCUSTOMERNO = C.CUSTOMERID
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) = CAST( GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY OT.orderCustomerNO,(C.fName + ' ' + C.lName) 
ORDER BY SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY) DESC;




SELECT 
ISNULL((I.itemCategory),'TOTAL') AS CATEGORY,
SUM(O.orderItemQty) AS QTY,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE +O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO 
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) < CAST(GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) AND
CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) > CAST( (GETDATE() - (CONVERT(INT,'8'))) AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY I.ITEMCATEGORY
WITH ROLLUP;


SELECT 
CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) AS PASTDATE,	
SUM(O.ORDERITEMQTY) AS QTYTOTAL,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NETTOTAL,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE +O.ORDERITEMTAX),'C','EN-US') AS GROSSTOTAL
FROM TBLORDERITEM O JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO 
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) < CAST( GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) AND
CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) > CAST(GETDATE()-(CONVERT(INT,'8')) AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
WITH ROLLUP;

SELECT TOP 10
C.ITEMNAME AS NAME,
O.orderItemUPrice AS PRICE,
SUM(O.ORDERITEMQTY) AS QTY,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE + O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O 
JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO
JOIN TBLITEM C ON O.orderItemID=C.ITEMID 
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) < CAST(GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) AND
	CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) > CAST( (GETDATE()-(CONVERT(INT,'8'))) AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY C.ITEMNAME,O.orderItemUPrice
ORDER BY SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY) DESC;


SELECT TOP 5
OT.orderCustomerNO AS NO,
(C.fName + ' ' + C.lName) AS NAME,
FORMAT(SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(O.ORDERITEMQTY * O.ORDERITEMUPRICE + O.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDERITEM O 
JOIN TBLITEM I ON O.orderItemID = I.ITEMID
JOIN TBLORDER OT ON O.ORDERID = OT.ORDERNO
JOIN TBLCUSTOMER C ON OT.ORDERCUSTOMERNO = C.CUSTOMERID
WHERE CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) < CAST( GETDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) AND
CAST(OT.ORDERDATE AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE) > CAST(GETDATE()-(CONVERT(INT,'8')) AT TIME ZONE 'UTC' AT TIME ZONE 'Eastern Standard Time' AS DATE)
GROUP BY OT.orderCustomerNO,(C.fName + ' ' + C.lName) 
ORDER BY SUM(O.ORDERITEMUPRICE * O.ORDERITEMQTY) DESC;
");


            cmdString = new SqlCommand(rQuery, cntString);

            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataSet ds = new DataSet();
                da.Fill(ds);

                report.aTable = ds.Tables[0]; // TODAY
                report.bTable = ds.Tables[1]; // TODAY ITEM CHART           
                report.cTable = ds.Tables[2];

                report.dTable = ds.Tables[3];
                report.eTable = ds.Tables[4];// PAST 7 DAYS BY EACH DAY
                report.fTable = ds.Tables[5];
                report.gTable = ds.Tables[6];
               
              

            }

            finally
            {
                cntString.Close();
            }
        }


        public static void DisplayInvoice(CsInvoice invoice)
        {

            string iQuery = string.Format(@"
SELECT O.ORDERNO AS ORDERNO,
O.ORDERCUSTOMERNO AS CUSTOMERNO,
CONVERT(VARCHAR(11),CONVERT(DATE,O.ORDERDATE)) AS ORDERDATE,
(C.FNAME + ' ' + C.LNAME) AS NAME,
SUM(OI.ORDERITEMQTY) AS QTY,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY + OI.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDER O 
JOIN TBLORDERITEM OI ON O.ORDERNO = OI.ORDERID
JOIN TBLCUSTOMER C ON O.ORDERCUSTOMERNO = C.CUSTOMERID
GROUP BY O.ORDERNO,O.ORDERCUSTOMERNO,(C.FNAME + ' ' + C.LNAME),O.ORDERDATE
ORDER BY O.ORDERNO DESC;");

            cmdString = new SqlCommand(iQuery, cntString);

            try
            {
              
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);             
                DataSet ds = new DataSet();
                da.Fill(ds);
                invoice.InvoiceList = ds.Tables[0];
                        
            }

            finally
            {
                cntString.Close();
            }

        }

    public static void ViewCustomer(CsReceiptInfo vinfo)
        {

            string iQuery = string.Format(@"
SELECT TOP 1
O.ORDERCUSTOMERNO AS CUSTOMERNO,
CONVERT(VARCHAR(11),CONVERT(DATE,O.ORDERDATE)) AS ORDERDATE,
(C.FNAME + ' ' + C.LNAME) AS NAME,
C.Email AS EMAIL
FROM TBLORDER O 
JOIN TBLORDERITEM OI ON O.ORDERNO = OI.ORDERID
JOIN TBLCUSTOMER C ON O.ORDERCUSTOMERNO = C.CUSTOMERID
WHERE O.ORDERNO = ('{0}')", vinfo.OrderNum);

            cmdString = new SqlCommand(iQuery, cntString);


            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataSet ds = new DataSet();
                da.Fill(ds);
                vinfo.CustomerInfo = ds.Tables[0];


            }
            catch
            {

            }
            finally
            {
                cntString.Close();
            }
        }


        public static void SearchInvoiceByName(CsSearchInvoice list)
        {
            string iQuery = string.Format(@"
SELECT O.ORDERNO AS ORDERNO,
O.ORDERCUSTOMERNO AS CUSTOMERNO,
CONVERT(VARCHAR(11),CONVERT(DATE,O.ORDERDATE)) AS ORDERDATE,
(C.FNAME + ' ' + C.LNAME) AS NAME,
SUM(OI.ORDERITEMQTY) AS QTY,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY + OI.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDER O 
JOIN TBLORDERITEM OI ON O.ORDERNO = OI.ORDERID
JOIN TBLCUSTOMER C ON O.ORDERCUSTOMERNO = C.CUSTOMERID
WHERE LOWER (C.FNAME + ' ' + C.LNAME) LIKE LOWER (N'%{0}%') 
GROUP BY O.ORDERNO,O.ORDERCUSTOMERNO,(C.FNAME + ' ' + C.LNAME),O.ORDERDATE;",
list.KeyWord1);

            cmdString = new SqlCommand(iQuery, cntString);


            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataSet ds = new DataSet();
                da.Fill(ds);
            
                list.Invoicelist = ds.Tables[0];
             }
             catch
             {

             }

                finally
                {
                    cntString.Close();
                }
            }




        public static void SearchInvoiceByNum(CsSearchInvoice list)
        {
            string iQuery = string.Format(@"
SELECT O.ORDERNO AS ORDERNO,
O.ORDERCUSTOMERNO AS CUSTOMERNO,
CONVERT(VARCHAR(11),CONVERT(DATE,O.ORDERDATE)) AS ORDERDATE,
(C.FNAME + ' ' + C.LNAME) AS NAME,
SUM(OI.ORDERITEMQTY) AS QTY,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY),'C','EN-US') AS NET,
FORMAT(SUM(OI.ORDERITEMUPRICE * OI.ORDERITEMQTY + OI.ORDERITEMTAX),'C','EN-US') AS GROSS
FROM TBLORDER O 
JOIN TBLORDERITEM OI ON O.ORDERNO = OI.ORDERID
JOIN TBLCUSTOMER C ON O.ORDERCUSTOMERNO = C.CUSTOMERID
WHERE O.ORDERNO=('{0}')
GROUP BY O.ORDERNO,O.ORDERCUSTOMERNO,(C.FNAME + ' ' + C.LNAME),O.ORDERDATE;",
list.Keyword2);

            cmdString = new SqlCommand(iQuery, cntString);


            try
            {
                cntString.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmdString);
                DataSet ds = new DataSet();
                da.Fill(ds);

                list.Invoicelist = ds.Tables[0];
            }
            catch
            {

            }

            finally
            {
                cntString.Close();
            }


        }






    }
}