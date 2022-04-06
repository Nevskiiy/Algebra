<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="NevenIvanisic_RWA._21.Customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script>
        // Assign bootstap framework to table cells
        window.addEventListener('DOMContentLoaded', () => {
            const inputs = document.querySelectorAll('input[type=text]');
            const td = document.querySelectorAll('td');

            // Allign table cells
            td.forEach(cell => cell.classList.add('align-middle'));
            inputs.forEach(input => input.classList.add('form-control'));

            // Handle validation for false e-mail update!
            document.querySelector('#RegularExpressionValidator1').style.display = 'none';
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Label
            runat="server"
            ID="Label2"
            Text="User: ">            
        </asp:Label>
        <asp:Label
            runat="server"
            ID="lblCookieInfo"
            Font-Bold="true"
            Text="Label">            
        </asp:Label>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="margin-top: 0.2em;">
                <asp:Label
                    runat="server"
                    Text="Country"
                    Font-Bold="true" />
            </div>
            <div class="col-sm-2">
                <asp:DropDownList
                    runat="server"
                    AutoPostBack="true"
                    CssClass="form-control"
                    ID="ddlCountry"
                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" />
            </div>
            <div class="col-sm-2" style="margin-top:0.2em;">
                <asp:Label
                    runat="server"
                    Text="Update town"
                    Font-Bold="true"/>
            </div>
            <div class="col-sm-2">
                <asp:TextBox
                    runat="server"
                    CssClass="form-control"
                    ID="txtTown" />
            </div>
            <div class="col-sm-4">
                <asp:Button
                    runat="server"
                    CssClass="form-control btn btn-warning"
                    ID="btnUpdateTown"
                    OnClick="btnUpdateTown_Click"
                    Text="Add" />
            </div>
        </div>
    </div>

    <div class="container">
        <div class="row">
            <div class="col-sm-2" style="margin-top:0.2em;">
                <asp:Label 
                    runat="server"
                    Text="Towns"
                    Font-Bold="true"/>
            </div>
            <div class="col-sm-6">
                <asp:ListBox
                    runat="server"
                    AutoPostBack="true"
                    CssClass="form-control"
                    ID="lbTowns"
                    OnSelectedIndexChanged="lbTowns_SelectedIndexChanged" />
            </div>
            <div class="col-sm-4">
                <asp:Button
                    runat="server"
                    AutoPostBack="true"
                    CssClass="form-control btn btn-warning"
                    Enabled="false"
                    ID="btnSelect"
                    OnClick="btnSelect_Click"
                    Style="margin-bottom: 1.5em;"
                    Text="Select" />
                <asp:Button
                    runat="server"
                    AutoPostBack="true"
                    CssClass="form-control btn btn-danger"
                    Enabled="false"
                    ID="btnDelete"
                    OnClick="btnDelete_Click"
                    Text="Delete" />
            </div>
        </div>
    </div>

    <div class="container">
        <asp:GridView  
            runat="server" 
            AutoGenerateColumns="False" 
            BackColor="White" 
            BorderColor="#CCCCCC" 
            BorderStyle="None" 
            BorderWidth="1px" 
            CellPadding="4" 
            CssClass="table"  
            DataKeyNames="IDKupac"
            ForeColor="Black" 
            GridLines="Horizontal" 
            ID="gridViewCustomers"
            OnRowEditing="gridViewCustomers_RowEditing"
            OnRowCancelingEdit="gridViewCustomers_RowCancelingEdit"
            OnRowUpdating="gridViewCustomers_RowUpdating">

            <Columns>
                <asp:BoundField DataField="Ime" HeaderText="Name" />
                <asp:BoundField DataField="Prezime" HeaderText="Lastname" />
                <asp:TemplateField HeaderText="Email">
                    <EditItemTemplate>
                        <asp:TextBox 
                            runat="server" 
                            ID="txtEmail" 
                            Text='<%# Bind("Email") %>'>
                        </asp:TextBox>
                        <asp:RegularExpressionValidator 
                            runat="server"                   
                            ControlToValidate="txtEmail"          
                            ClientIDMode="Static"
                            ErrorMessage="Invalid e-mail" 
                            ID="RegularExpressionValidator1" 
                            Text="*"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label 
                            runat="server" 
                            ID="Label1"
                            Text='<%# Bind("Email", "<a href=mailto:{0}>{0}</a>") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Telefon" HeaderText="Telephone" />
                <asp:BoundField DataField="GradID" HeaderText="Town" />
                <asp:TemplateField ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton 
                            runat="server" 
                            CausesValidation="True" 
                            CommandName="Update" 
                            CssClass="btn btn-sm btn-warning"
                            ID="LinkButton1" 
                            Text="Update">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton 
                            runat="server" 
                            CausesValidation="False" 
                            CommandName="Cancel" 
                            CssClass="btn btn-sm btn-danger"
                            ID="LinkButton2" 
                            Text="Cancel">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton
                            runat="server"
                            CausesValidation="False"
                            CommandName="Select"
                            CssClass="btn btn-sm btn-warning"
                            ID="linkButtonSelect"
                            Text="Select">
                        </asp:LinkButton>
                        &nbsp;
                        <asp:LinkButton
                            runat="server"
                            CausesValidation="False"
                            CommandName="Edit"
                            CssClass="btn btn-sm btn-danger"
                            ID="LinkButton1"
                            Text="Edit">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#999999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <div>
            <asp:ValidationSummary runat="server"/>
        </div>
    </div>
</asp:Content>
