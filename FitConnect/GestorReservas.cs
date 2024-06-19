using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using FitConnect;

public class GestorReservas
{
    private List<Reserva> reservas;
    private GestorPagos gestorPagos;

    public GestorReservas(GestorPagos gestorPagos)
    {
        reservas = new List<Reserva>();
        this.gestorPagos = gestorPagos;
    }

    public GestorReservas()
    {
        reservas = new List<Reserva>();
    }

    public void AgregarReserva(Reserva reserva)
    {
        reservas.Add(reserva);
    }
    public bool TieneReserva(Socio socio, Actividad actividad)
    {
        foreach (Reserva reserva in reservas)
        {
            Socio socioReserva = reserva.ObtenerSocio();
            if (socioReserva != null && socioReserva.Equals(socio) 
                && reserva.ObtenerActividad().Equals(actividad))
            {
                return true;
            }
        }

        return false;
    }
    public bool ReservarActividad(Socio socio, Actividad actividad)
    {
        // Comprobamos si el socio ya tiene una reserva para esta actividad
        if (TieneReserva(socio, actividad))
        {
            Console.WriteLine("Ya tienes una reserva para esta actividad.");
            return false;
        }

        if (gestorPagos.ComprobarPuntosSocio(socio))
        {
            // Creamos nueva reserva
            Reserva nuevaReserva = new Reserva(socio, actividad, DateTime.Now);
  
            reservas.Add(nuevaReserva);
  
            gestorPagos.RestarPuntosSocio(socio, 1);
            gestorPagos.ActualizarPagosEnArchivo("pagos.txt");
            // Guardamos las reservas en el archivo
            GuardarReservasEnArchivo("reservas.txt");
            Console.WriteLine("Reserva realizada correctamente");
            return true;
        }
        else
        {
            Console.WriteLine("No tienes suficientes puntos para " +
                "realizar esta reserva.");
            return false;
        }
    }
    public void ReservaComoInvitado(GestorInvitaciones gestorInvitaciones, 
        GestorActividades gestorActividades)
    {
        Console.WriteLine("Por favor, introduce el código de invitación:");
        string codigoInvitacion = Console.ReadLine();
        if (gestorInvitaciones.ValidarInvitacion(codigoInvitacion))
        {
            Actividad actividadSeleccionada = 
                gestorActividades.SeleccionarActividad();
            if (actividadSeleccionada != null)
            {
                this.ReservarActividad(null, actividadSeleccionada);
                
                
            }
        }
        else
        {
            Console.WriteLine("El código de invitación no es válido " +
                "o ya ha sido utilizado. Por favor, inténtalo de nuevo." +
                "(Pulsa Intro para continuar)");
        }
    }

    public bool CancelarReserva(Socio socio)
    {
        VerReservasSocio(socio);

        Console.Write("Por favor, introduce el nombre de la actividad que " +
            "deseas cancelar: ");
        string nombreActividad = Console.ReadLine();

        // Buscamos la reserva que coincide con el socio y la actividad
        Reserva reservaParaCancelar = null;
        foreach (Reserva reserva in reservas)
        {
            Socio socioReserva = reserva.ObtenerSocio();
            Actividad actividadReserva = reserva.ObtenerActividad();

            if (socioReserva != null && actividadReserva != null && 
                socioReserva.Equals(socio) && 
                actividadReserva.ObtenerNombre().Equals(nombreActividad))
            {
                reservaParaCancelar = reserva;
                break;
            }
        }
        
        if (reservaParaCancelar == null)
        {
            Console.WriteLine("No se encontró ninguna reserva " +
                "para la actividad indicada.");
            return false;
        }

        Console.Write("¿Estás seguro de que quieres " +
            "cancelar esta reserva? (s/n): ");
        string respuesta = Console.ReadLine();
        if (respuesta.ToLower() != "s")
        {
            Console.WriteLine("Cancelado.");
            return false;
        }

        reservas.Remove(reservaParaCancelar);

        // Devolvemos los puntos al socio
        gestorPagos.AgregarPuntosSocio(socio, 1);
        gestorPagos.ActualizarPagosEnArchivo("pagos.txt");
        GuardarReservasEnArchivo("reservas.txt");
        Console.WriteLine("Reserva cancelada correctamente.");
        return true;
    }
    public void GuardarReservasEnArchivo(string archivo)
    {
        using (StreamWriter escribir = new StreamWriter(archivo))
        {
            foreach (Reserva reserva in reservas)
            {
                // Comprobamos si el usuario añadido es un invitado
                string idSocio = reserva.ObtenerSocio() == null 
                    ? "Invitado" : reserva.ObtenerSocio().ObtenerId();

                escribir.WriteLine(idSocio + "," +
                reserva.ObtenerActividad().ObtenerNombre() + "," +
                reserva.ObtenerFechaReserva().ToString("yyyy-MM-dd HH:mm"));
            }
        }
    }
    public void CargarReservasDesdeArchivo(string archivo, 
        GestorUsuarios gestorUsuarios,GestorActividades gestorActividades)
    {
        if (!File.Exists(archivo)) return;

        using (StreamReader reader = new StreamReader(archivo))
        {
            string linea;
            while ((linea = reader.ReadLine()) != null)
            {

                string[] datos = linea.Split(',');

                // Si el socio es null es un invitado
                Socio socio = null;
                if (datos[0] != "Invitado")
                {
                    // Encontrar el socio correspondiente
                    socio = 
                    (Socio)gestorUsuarios.ObtenerUsuarioPorId(datos[0]);
                }

                Actividad actividad = 
                    gestorActividades.ObtenerActividadPorNombre(datos[1]);

                if (actividad != null)
                {
                    DateTime fechaReserva = 
                        DateTime.ParseExact(datos[2], "yyyy-MM-dd HH:mm", 
                        CultureInfo.InvariantCulture);
                    Reserva nuevaReserva = 
                        new Reserva(socio, actividad, fechaReserva);
                    reservas.Add(nuevaReserva);
                }
            }
        }
    }
    public void VerReservasSocio(Socio socio)
    {
        Console.WriteLine("Tus reservas:");
        bool tieneReservas = false;
        foreach (Reserva reserva in reservas)
        {
            Socio miembroReserva = reserva.ObtenerSocio();
            if (miembroReserva != null && miembroReserva.Equals(socio))
            {
                Console.WriteLine(reserva.ToString());
                Console.WriteLine("--------------------");
                tieneReservas = true;
            }
        }
        if (!tieneReservas)
        {
            Console.WriteLine("No tienes reservas.");
        }
    }
}


