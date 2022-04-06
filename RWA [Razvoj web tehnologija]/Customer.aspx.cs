
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NevenIvanisic_RWA._21
{
    public partial class Customer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["username"] == null)
            {
                Response.Redirect("~/Log.aspx");
            }
            else lblCookieInfo.Text = Request.Cookies["username"].Value;

            if (!IsPostBack)
            {
                ShowCountry();
                ShowTowns();
                ShowCustomers();
            }
        }
        private void ShowCountry()
        {
            ddlCountry.DataSource = Repo._GetCountry();
            ddlCountry.DataTextField = "Naziv";
            ddlCountry.DataValueField = "IDDrzava";
            ddlCountry.DataBind();
        }

        private void ShowTowns()
        {
            var drzavaId = int.Parse(ddlCountry.SelectedValue);
            lbTowns.DataSource = Repo._GetTowns(drzavaId);
            lbTowns.DataTextField = "Naziv";
            lbTowns.DataValueField = "IDGrad";
            lbTowns.DataBind();
        }
        private void ShowCustomers()
        {
            gridViewCustomers.DataSource = Repo._GetCustomers(10);
            gridViewCustomers.DataBind();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e) => ShowTowns();

        protected void btnUpdateTown_Click(object sender, EventArgs e)
        {
            if (txtTown.Text.Trim() == "") return;

            var drzavaId = int.Parse(ddlCountry.SelectedValue);
            Repo._InsertTown(new Grad
            {
                Naziv = txtTown.Text,
                DrzavaId = drzavaId
            });

            txtTown.Text = "";
            ShowTowns();
        }

        protected void lbTowns_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
            btnDelete.Enabled = true;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            var gradId = int.Parse(lbTowns.SelectedValue);
            Repo._GetDeleteTown(gradId);
            ShowTowns();
        }

        protected void gridViewCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridViewCustomers.SelectedIndex = -1;
            gridViewCustomers.EditIndex = e.NewEditIndex;
            gridViewCustomers.DataSource = Repo._GetCustomers(10);
            gridViewCustomers.DataBind();
        }

        protected void gridViewCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridViewCustomers.EditIndex = -1;
            gridViewCustomers.DataSource = Repo._GetCustomers(10);
            gridViewCustomers.DataBind();
        }

        protected void gridViewCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow gvr = gridViewCustomers.Rows[e.RowIndex];
            var kupacId = gridViewCustomers.DataKeys[e.RowIndex].Value;
            var gradId = gridViewCustomers.DataKeys[e.RowIndex].Value;

            var ime = gvr.Cells[0].Controls.OfType<TextBox>().First().Text;
            var prezime = gvr.Cells[1].Controls.OfType<TextBox>().First().Text;
            var email = gvr.Cells[2].Controls.OfType<TextBox>().First().Text;
            var telefon = gvr.Cells[3].Controls.OfType<TextBox>().First().Text;
             

            var kupac = new Kupac
            {
                IDKupac = (int)kupacId,
                Ime = ime,
                Prezime = prezime,
                Email = email,
                Telefon = telefon,
                GradID = (int)gradId
            };

            Repo._UpdateCustomer(kupac);

            gridViewCustomers.EditIndex = -1;
            gridViewCustomers.DataSource = Repo._GetCustomers(10);
            gridViewCustomers.DataBind();
        }
    }
}