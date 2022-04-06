<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Log.aspx.cs" Inherits="NevenIvanisic_RWA._21.Log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NIvanisic_RWA.21</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="form-group">
                <label>Username</label>                
                <asp:TextBox 
                    runat="server"
                    AutoCompleteType="Disabled"
                    ID="txtUsername"
                    CssClass="form-control"/>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="txtUsername"
                    Display="Dynamic"
                    ErrorMessage="Username required!" 
                    Text="Username required!"/>
                <asp:RegularExpressionValidator 
                    runat="server"
                    ControlToValidate="txtUsername"
                    ErrorMessage="Username has to be between 5-20 letters!"                    
                    ForeColor="#CC0000"
                    ValidationExpression="^(?i)(((?=.{6,21}$)[a-z\d]+\.[a-z\d]+)|[a-z\d]{5,20})$"/>
            </div>
            <div class="form-group">
                <label>Password</label>
                <asp:TextBox 
                    runat="server"
                    AutoCompleteType="Disabled"
                    ID="txtPassword"
                    CssClass="form-control"/>
                <asp:RequiredFieldValidator
                    runat="server"
                    ControlToValidate="txtPassword"
                    Display="Dynamic"
                    ErrorMessage="Password required!"
                    Text="Password required!" />
                <asp:RegularExpressionValidator
                    runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="Password has to be between 5-20 letters!"
                    ForeColor="#CC0000"
                    ValidationExpression="^(?i)(((?=.{6,21}$)[a-z\d]+\.[a-z\d]+)|[a-z\d]{5,20})$"/>
            </div>
            <div class="form-group">
                <asp:Button 
                    runat="server"
                    CssClass="btn btn-primary btn-lg btn-block"
                    ID="btnLogin"
                    OnClick="btnLogin_Click"
                    Text="Login"/>
            </div>
            <asp:ValidationSummary runat="server"/>
        </div>
    </form>
</body>
</html>
