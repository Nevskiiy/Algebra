using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
public class Kupac
{
    public int IDKupac { get; set; }
    
    [Required(ErrorMessage = "Name is mandatory")]
    [StringLength(20, ErrorMessage = "Max length is 50 chars")]
    public string Ime { get; set; }

    [Required(ErrorMessage = "Lastame is mandatory")]
    [StringLength(20, ErrorMessage = "Max length is 50 chars")]
    public string Prezime { get; set; }

    [EmailAddress(ErrorMessage = "Wrong e-mail address")]
    public string Email { get; set; }
    public string Telefon { get; set; }

    [Required(ErrorMessage = "No town was choosen")]
    [Display(Name = "Town")]
    public int GradID { get; set; }

    public override string ToString()
    {
        return $"{Ime} {Prezime}";
    }
}