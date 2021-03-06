﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="032customer_seedetail.aspx.cs" Inherits="DodamPOS._032customer_seedetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" style="height:100%;">
<head runat="server">
    <title></title>
	<link href="css/poscss.css" rel="stylesheet" type="text/css" />
</head>
<body id="container_newwindow">
    <form id="form1" runat="server">

		<h2>Customer Details</h2><hr />

		<p style="text-align:right;"> Customer No :<asp:Label ID="cnumlbl" runat="server" Text="" Font-Size="Medium"></asp:Label> </p>

				<table style="width:100%">
                
				<tr>        
                <td><span style="color:hotpink">● </span> First Name
					<asp:RequiredFieldValidator ID="ReqFname" runat="server"  ErrorMessage="&nbsp; Please enter first name"  ControlToValidate="fnamebox" CssClass="ErrorMSG" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegFname" runat="server" ErrorMessage="Up to 30 characters,please"  ControlToValidate="fnamebox" CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^.{1,30}$" ></asp:RegularExpressionValidator>
                </td>	
					
				<td><span style="color:hotpink;">● </span> Address 
					<asp:RequiredFieldValidator ID="ReqAddress" runat="server" ErrorMessage="&nbsp; Please enter address &nbsp;"  ControlToValidate="addressbox" CssClass="ErrorMSG" Display="Dynamic" ></asp:RequiredFieldValidator>
				    <asp:RegularExpressionValidator ID="RegAddress" runat="server" ErrorMessage="Up to 40 Characters,please" ControlToValidate="addressbox" CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^.{1,40}$" ></asp:RegularExpressionValidator>
				</td>
				</tr>

				<tr>
				<td><asp:TextBox ID="fnamebox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>  
				<td><asp:TextBox ID="addressbox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>           
                </tr>

				<tr>        
                <td><span style="color:hotpink">● </span> Last Name
					<asp:RequiredFieldValidator ID="ReqLname" runat="server" ErrorMessage="&nbsp; Please enter last name &nbsp;" CssClass="ErrorMSG" ControlToValidate="lnamebox" Display="Dynamic"  ></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegLname" runat="server" ErrorMessage="Up to 20 Characters,please" ControlToValidate="lnamebox" CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^.{1,20}$" ></asp:RegularExpressionValidator>
                </td>
					
				<td><span style="color:hotpink;">● </span> City
				<asp:RequiredFieldValidator ID="ReqCity" runat="server" ErrorMessage="&nbsp; Please enter city &nbsp;" CssClass="ErrorMSG" ControlToValidate="citybox" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="Regcity" runat="server" ErrorMessage="Up to 20 Characters,please" ControlToValidate="citybox" CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^.{1,20}$" ></asp:RegularExpressionValidator>
				</td>
				</tr>

				<tr>
				<td><asp:TextBox ID="lnamebox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>
				<td><asp:TextBox ID="citybox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td> 
                </tr>

				<tr>        
                <td><span style="color:hotpink">● </span> Email 
				<asp:RequiredFieldValidator ID="ReqEmail" runat="server" ErrorMessage="&nbsp; Please enter email &nbsp;" CssClass="ErrorMSG"  Display="Dynamic" ControlToValidate="emailbox"></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegexEmail" runat="server" ErrorMessage="&nbsp; Please enter valid email &nbsp;" ControlToValidate="emailbox" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="ErrorMSG" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
				
					
					
				<td><span style="color:hotpink;">● </span> Province
                <asp:RequiredFieldValidator ID="ReqProvince" runat="server" ErrorMessage="&nbsp; Please select province. Choose N/A if not applicable &nbsp;"  ControlToValidate="provincelist" CssClass="ErrorMSG"></asp:RequiredFieldValidator>
				</td>
				</tr>

				<tr>
				<td><asp:TextBox ID="emailbox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>
				
				<td><asp:DropDownList ID="provincelist" runat="server" CssClass="ProvinceInput" AppendDataBoundItems="True" AutoPostBack="True"  >
				<asp:ListItem Text="" Value="" Selected="false"></asp:ListItem>
				</asp:DropDownList></td> 
                </tr>
				 
				<tr>        
                <td><span style="color:hotpink">● </span> Phone
				<asp:RequiredFieldValidator ID="ReqPhone" runat="server" ErrorMessage="Please enter phone number(US & Canada)" CssClass="ErrorMSG"  Display="Dynamic" ControlToValidate="phonebox"  ></asp:RequiredFieldValidator>
				<asp:RegularExpressionValidator ID="RegexPhone" runat="server" ErrorMessage="Please enter valid phone number " ControlToValidate="phonebox"  CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$"></asp:RegularExpressionValidator>
                </td>

				<td><span style="color:hotpink">● </span> Postal Code
				<asp:RegularExpressionValidator ID="RegexPcode" runat="server" ErrorMessage="Please enter US/Canada Zipcode.Input12345 if N/A" ControlToValidate="pcodebox"  CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[A-Za-z]{1} *\d{1}[A-Za-z]{1}\d{1}$)" ></asp:RegularExpressionValidator>
				</td>
				</tr>

				<tr>
				<td><asp:TextBox ID="phonebox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>
				<td><asp:TextBox ID="pcodebox" runat="server" CssClass="CustomerOutput" ></asp:TextBox></td>
                </tr>

				<tr>
				<td><span style="color:azure">● </span> Note
					<asp:RegularExpressionValidator ID="Regnote" runat="server" ErrorMessage="Up to 100 Characters,please" ControlToValidate="notebox" CssClass="ErrorMSG" Display="Dynamic" ValidationExpression="^.{1,100}$" ></asp:RegularExpressionValidator>
				</td>
				</tr>

				
				<tr>
				<td><asp:TextBox ID="notebox" runat="server" CssClass="CustomerOutput"  Height="60px" Width="300px"></asp:TextBox></td>
				</tr>


				</table>

		     <br />
             <asp:Button ID="btnUpdate" runat="server" Text="UPDATE" CssClass="RegisterBtn" OnClick="btnUpdate_Click" CausesValidation="False" /> &nbsp;&nbsp;
		     <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="RegisterBtn" OnClick="btnSave_Click" Visible="False"  /> &nbsp;&nbsp;
             <asp:Button ID="btnClose" runat="server" Text="CLOSE" CssClass="CloseBtn" OnClientClick="javascript:window.close();" CausesValidation="False"  />&nbsp;&nbsp;
		     <asp:Label ID="TEST" runat="server" Text=""></asp:Label>
          

    </form>
</body>
</html>
