using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class Repo
{
    public static DataSet ds { get; set; }

    private static string cs = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;

    public static IEnumerable<Drzava> _GetCountry()
    {
        var lbCountry = SqlHelper.ExecuteDataset(cs, "_GetCountry").Tables[0];
        foreach (DataRow row in lbCountry.Rows)
        {
            yield return new Drzava
            {
                IDDrzava = (int)row["IDDrzava"],
                Naziv = row["Naziv"].ToString()
            };
        }
    }


    public static IEnumerable<Grad> _GetTowns(int drzavaId)
    {
        var lbTowns = SqlHelper.ExecuteDataset(cs, "_GetTowns", drzavaId).Tables[0];
        foreach (DataRow row in lbTowns.Rows)
        {
            yield return new Grad
            {
                IDGrad = (int)row["IDGrad"],
                Naziv = row["Naziv"].ToString()
            };
        }
    }

    public static List<Grad> _GetUnqTowns()
    {
        List<Grad> collectionTowns = new List<Grad>();

        ds = SqlHelper.ExecuteDataset(cs, "_GetUnqTowns");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            collectionTowns.Add(new Grad
            {
                IDGrad = (int)row["IDGrad"],
                Naziv = row["Naziv"].ToString()
            });
        }
        return collectionTowns;
    }

    public static Kupac GetCustomer(int IDKupac)
    {
        return _GetCustomers(10).SingleOrDefault(k => k.IDKupac == IDKupac);
    }

    public static IEnumerable<Kupac> _GetCustomers(int customers)
    {
        // For sorting and filtering use SqlDataAdapter -> DataTable
        SqlDataReader sdr = SqlHelper.ExecuteReader(cs, "_GetCustomers", customers);
        DataTable dt = new DataTable();
        dt.Load(sdr);

        foreach (DataRow row in dt.Rows)
        {
            yield return Repo.GetCustomerFromDataRow(row);
        }
    }

    public static Kupac _GetCustomerById(int IDKupac)
    {
        DataRow row = SqlHelper.ExecuteDataset(cs, "_GetCustomerById", IDKupac).Tables[0].Rows[0];
        return GetCustomerFromDataRow(row);
    }

    public static Kupac GetCustomerFromDataRow(DataRow row)
    {
        return new Kupac
        {
            IDKupac = (int)row["IDKupac"],
            Ime = row["Ime"].ToString(),
            Prezime = row["Prezime"].ToString(),
            Email = row["Email"].ToString(),
            Telefon = row["Telefon"].ToString(),
            GradID = (int)row["GradID"]
        };
    }

    public static IEnumerable<Kupac> _Get_Customers()
    {
        ds = SqlHelper.ExecuteDataset(cs, "DohvatiKupce");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            yield return new Kupac
            {
                IDKupac = (int)row["IDKupac"],
                Ime = row["Ime"].ToString(),
                Prezime = row["Prezime"].ToString(),
                Email = row["Email"].ToString(),
                Telefon = row["Telefon"].ToString(),
                GradID = row["GradID"] != DBNull.Value ? (int)row["GradID"] : 1
            };
        }
    }

    public static int _UpdateCustomer(Kupac k)
    {
        return SqlHelper.ExecuteNonQuery(cs, "_UpdateCustomer",
            k.IDKupac, k.Ime, k.Prezime, k.Email, k.Telefon, k.GradID);
    }

    public static Grad _GetTown(int gradId)
    {
        DataRow row = SqlHelper.ExecuteDataset(cs, "_GetTown", gradId).Tables[0].Rows[0];
        return new Grad
        {
            IDGrad = (int)row["IDGrad"],
            Naziv = row["Naziv"].ToString(),
            DrzavaId = (int)row["DrzavaID"]
        };
    }

    internal static void _GetDeleteTown(int gradId) => SqlHelper.ExecuteNonQuery(cs, "_DeleteTown", gradId);

    internal static void _InsertTown(Grad grad) => SqlHelper.ExecuteNonQuery(cs, "_InsertTown", grad.Naziv, grad.DrzavaId);

    public static IEnumerable<Racun> _GetInvoiceByCustomer(int kupacId)
    {
        ds = SqlHelper.ExecuteDataset(cs, "_GetInvoiceByCustomer", kupacId);
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            yield return new Racun
            {
                IDRacun = (int)row["IDRacun"],
                DatumIzdavanja = DateTime.Parse(row["DatumIzdavanja"].ToString()),
                BrojRacuna = row["BrojRacuna"].ToString(),
                Komentar = row["Komentar"] != DBNull.Value ? row["Komentar"].ToString() : "-"
            };
        }
    }
    //public static IEnumerable<Proizvod> _GetProducts()
    //{
    //    ds = SqlHelper.ExecuteDataset(cs, "_GetProducts");
    //    foreach (DataRow row in ds.Tables[0].Rows)
    //    {
    //        yield return new Proizvod
    //        {
    //            IDProizvod = (int)row["IDProizvod"],
    //            Naziv = row["Naziv"].ToString(),
    //            BrojProizvoda = row["BrojProizvoda"].ToString(),
    //            Boja = row["Boja"].ToString(),
    //            MinimalnaKolicinaNaSkladistu = (int)row["MinimalnaKolicinaNaSkladistu"],
    //            CijenaBezPDV = (double)row["CijenaBezPDV"],
    //            PotkategorijaID = row["PotkategorijaID"] != DBNull.Value ? (int)row["PotkategorijaID"] : 1
    //        };
    //    }
    //}

    public static IEnumerable<Proizvod> _GetProducts()
    {

        ds = SqlHelper.ExecuteDataset(cs, "_GetProducts");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            yield return new Proizvod
            {
                IDProizvod = (int)row["IDProizvod"],
                Naziv = row["Naziv"].ToString(),
                BrojProizvoda = row["BrojProizvoda"].ToString(),
                Boja = row["Boja"].ToString(),
                MinimalnaKolicinaNaSkladistu = (int)row["MinimalnaKolicinaNaSkladistu"],
                CijenaBezPDV = Convert.ToDouble(row["CijenaBezPDV"]),
                PotkategorijaID = (int)row["PotkategorijaID"]
            };
        }
    }


    public static Proizvod _GetProduct(int id)
    {
        DataRow row = SqlHelper.ExecuteDataset(cs, "_GetProduct", id).Tables[0].Rows[0];

        return new Proizvod
        {
            IDProizvod = (int)row["IDProizvod"],
            Naziv = row["Naziv"].ToString(),
            BrojProizvoda = row["BrojProizvoda"].ToString(),
            Boja = row["Boja"].ToString(),
            MinimalnaKolicinaNaSkladistu = (int)row["MinimalnaKolicinaNaSkladistu"],
            CijenaBezPDV = (int)row["CijenaBezPDV"],
            PotkategorijaID = row["PotkategorijaID"] != DBNull.Value ? (int)row["PotkategorijaID"] : 1
        };
    }

    internal static void _InsertProduct(Proizvod proizvod)
    {
        SqlHelper.ExecuteNonQuery(cs, "_InsertProduct",
            proizvod.Naziv,
            proizvod.BrojProizvoda,
            proizvod.Boja,
            proizvod.MinimalnaKolicinaNaSkladistu,
            proizvod.CijenaBezPDV,
            proizvod.PotkategorijaID);
    }

    internal static void _DeleteProduct(int id) => SqlHelper.ExecuteNonQuery(cs, "_DeleteProduct", id);
    public static int _UpdateProduct(Proizvod proizvod)
    {
        return SqlHelper.ExecuteNonQuery(cs, "_UpdateProduct", proizvod.IDProizvod, proizvod.Naziv, proizvod.BrojProizvoda, proizvod.Boja, proizvod.MinimalnaKolicinaNaSkladistu, proizvod.CijenaBezPDV, proizvod.PotkategorijaID);
    }

    public static IEnumerable<Kategorija> _GetCategories()
    {
        ds = SqlHelper.ExecuteDataset(cs, "_GetCategories");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            yield return new Kategorija
            {
                IDKategorija = (int)row["IDKategorija"],
                Naziv = row["Naziv"].ToString()
            };
        }
    }

    internal static void _DeleteCategory(int id) => SqlHelper.ExecuteNonQuery(cs, "_DeleteCategory", id);

    public static Kategorija _GetCategory(int id)
    {
        DataRow row = SqlHelper.ExecuteDataset(cs, "_GetCategory", id).Tables[0].Rows[0];

        return new Kategorija
        {
            IDKategorija = (int)row["IDKategorija"],
            Naziv = row["Naziv"].ToString()
        };
    }
    public static int _UpdateCategory(Kategorija kategorija)
    {
        return SqlHelper.ExecuteNonQuery(cs, "_UpdateCategory", kategorija.IDKategorija, kategorija.Naziv);
    }
    public static IEnumerable<Potkategorija> _GetSubcategories()
    {
        ds = SqlHelper.ExecuteDataset(cs, "_GetSubcategories");
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            yield return new Potkategorija
            {
                IDPotkategorija = (int)row["IDPotkategorija"],
                Naziv = row["Naziv"].ToString(),
                KategorijaID = (int)row["KategorijaID"]
            };
        }
    }
    internal static void _DeleteSubcategory(int id) => SqlHelper.ExecuteNonQuery(cs, "_DeleteSubcategory", id);
    internal static object _GetSubcategory(int id)
    {
        DataRow row = SqlHelper.ExecuteDataset(cs, "_GetSubcategory", id).Tables[0].Rows[0];

        return new Potkategorija
        {
            IDPotkategorija = (int)row["IDPotkategorija"],
            KategorijaID = (int)row["KategorijaID"],
            Naziv = row["Naziv"].ToString()
        };
    }

    public static int _UpdateSubcategory(Potkategorija potkategorija)
    {
        return SqlHelper.ExecuteNonQuery(cs, "_UpdateSubcategory", potkategorija.IDPotkategorija, potkategorija.KategorijaID, potkategorija.Naziv);
    }
}