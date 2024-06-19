using System;

public class Actividad
{
    private string nombre;
    private string descripcion;
    private string fecha;
    private string hora;
    private string idEntrenador;

    public Actividad(string nombre, string descripcion, string fecha,
        string hora)
    {
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.fecha = fecha;
        this.hora = hora;
    }

    public Actividad(string nombre, string descripcion, string fecha,
        string hora, string idEntrenador)
        : this(nombre, descripcion, fecha, hora)
    {
        this.idEntrenador = idEntrenador;
    }

    public string ObtenerIdEntrenador()
    {
        return idEntrenador;
    }

    public void EstablecerIdEntrenador(string idEntrenador)
    {
        this.idEntrenador = idEntrenador;
    }

    public string ObtenerNombre()
    {
        return nombre;
    }

    public void EstablecerNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string ObtenerDescripcion()
    {
        return descripcion;
    }

    public void EstablecerDescripcion(string descripcion)
    {
        this.descripcion = descripcion;
    }

    public string ObtenerFecha()
    {
        return fecha;
    }

    public void EstablecerFecha(string fecha)
    {
        this.fecha = fecha;
    }

    public string ObtenerHora()
    {
        return hora;
    }

    public void EstablecerHora(string hora)
    {
        this.hora = hora;
    }

    public override string ToString()
    {
        return "Actividad: " + ObtenerNombre() + "\n" +
                "Descripcion: " + ObtenerDescripcion() + "\n" +
                "Fecha: " + ObtenerFecha() + "\n" +
                "Hora: " + ObtenerHora() + "\n";
    }

    public override bool Equals(object obj)
    {
        if (obj is Actividad)
        {
            Actividad otraActividad = obj as Actividad;
            return this.ObtenerNombre() == otraActividad.ObtenerNombre() 
                && this.ObtenerFecha() 
                == otraActividad.ObtenerFecha() && this.ObtenerHora() 
                == otraActividad.ObtenerHora();
        }
        return false;
    }
    public override int GetHashCode()
    {
        return this.ObtenerNombre().GetHashCode() ^ this.ObtenerFecha().GetHashCode() 
            ^ this.ObtenerHora().GetHashCode();
    }
}

