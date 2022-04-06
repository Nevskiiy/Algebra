using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

public class Proizvod
{
    public int IDProizvod { get; set; }

    public string Naziv { get; set; }

    public string BrojProizvoda { get; set; }

    public string Boja { get; set; }
    public int MinimalnaKolicinaNaSkladistu { get; set; }

    public double CijenaBezPDV { get; set; }
    public int PotkategorijaID { get; set; }
}