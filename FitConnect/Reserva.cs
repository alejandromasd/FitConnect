using System;
public class Reserva
{
    private Socio socio;
    private Actividad actividad;
    private DateTime fechaReserva;

    public Reserva(Socio socio, Actividad actividad, DateTime fechaReserva)
    {
        this.socio = socio;
        this.actividad = actividad;
        this.fechaReserva = fechaReserva;
    }

    public Socio ObtenerSocio()
    {
        return socio;
    }

    public Actividad ObtenerActividad()
    {
        return actividad;
    }

    public DateTime ObtenerFechaReserva()
    {
        return fechaReserva;
    }

    public override string ToString()
    {
        return "Socio: " + socio.ObtenerNombre() + " " +
                socio.ObtenerApellido() + ", Actividad: " +
                actividad.ObtenerNombre() + 
                " Fecha de Reserva: " + 
                fechaReserva.ToString("yyyy-MM-dd HH:mm");
    }
}

