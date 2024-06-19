using System;

public class Medicion
{
    public string PesoCorporal { get; set; }
    public string MasaMuscular { get; set; }
    public string MasaGrasa { get; set; }
    public string ObjetivoPrincipal { get; set; }
    public DateTime Fecha { get; set; }
    public string IdEntrenador { get; set; }

    public Medicion(string pesoCorporal, string masaMuscular, string masaGrasa,
        string objetivoPrincipal, DateTime fecha, string idEntrenador)
    {
        this.PesoCorporal = pesoCorporal;
        this.MasaMuscular = masaMuscular;
        this.MasaGrasa = masaGrasa;
        this.ObjetivoPrincipal = objetivoPrincipal;
        this.Fecha = fecha;
        this.IdEntrenador = idEntrenador;
    }
}



