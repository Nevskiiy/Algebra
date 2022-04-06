using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Stavka
{
    public int IDStavka { get; set; }
    public int RacunID { get; set; }
    public int Kolicina { get; set; }
    public int ProizvodID { get; set; }
    public double CijenaPoKomadu { get; set; }
    public double PopustUPostotcima { get; set; }
    public double UkupnaCijena { get; set; }
}