using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GestorInvitaciones
{
    private List<Invitacion> invitaciones;
    private Dictionary<string, int> codigosPorSocio;


    public GestorInvitaciones()
    {
        invitaciones = new List<Invitacion>();
        codigosPorSocio = new Dictionary<string, int>();
    }
    public Invitacion CrearInvitacion(Socio socio)
    {
        // Comprobar si el socio ya ha generado 5 códigos
        if (codigosPorSocio.ContainsKey(socio.ObtenerId()) 
            && codigosPorSocio[socio.ObtenerId()] >= 5)
        {
            Console.WriteLine("Ha alcanzado el límite " +
                "de códigos de invitación.");
            return null;
        }
        // Generar un código aleatorio de 4 dígitos
        Random random = new Random();
        string codigo = random.Next(1000, 9999).ToString();

        Invitacion invitacion = new Invitacion(codigo, socio.ObtenerId());
        invitaciones.Add(invitacion);

        Console.WriteLine("El código de invitación es: " + codigo);

        GuardarInvitacionesEnArchivo("invitaciones.txt");

        return invitacion;
    }
    public bool ValidarInvitacion(string codigo)
    {
        foreach (Invitacion invitacion in invitaciones)
        {
            if (invitacion.ObtenerCodigo() == codigo 
                && !invitacion.EstaUtilizado())
            {
                // Marcar la invitación como utilizada y guardar en el archivo
                invitacion.MarcarComoUtilizado();
                GuardarInvitacionesEnArchivo("invitaciones.txt");
                return true;
            }
        }
        return false;
    }
    public void GuardarInvitacionesEnArchivo(string archivo)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            foreach (Invitacion invitacion in invitaciones)
            {
                string estado = invitacion.EstaUtilizado() 
                    ? "usado" : "no usado";
                writer.WriteLine($"{invitacion.ObtenerIdSocio()}," +
                    $"{invitacion.ObtenerCodigo()},{estado}");
            }
        }
    }
    public void CargarInvitacionesDesdeArchivo(string archivo)
    {
        if (File.Exists(archivo))
        {
            using (StreamReader reader = new StreamReader(archivo))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');
                    string idSocio = datos[0];
                    string codigo = datos[1];
                    bool utilizado = datos[2] == "usado";

                    Invitacion invitacion = new Invitacion(codigo, idSocio);
                    if (utilizado)
                    {
                        invitacion.MarcarComoUtilizado();
                    }
                    invitaciones.Add(invitacion);
                }
            }
        }
    }
}
