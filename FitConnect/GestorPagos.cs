using System;
using System.IO;

public class GestorPagos
{
    private GestorUsuarios gestorUsuarios;

    public GestorPagos(GestorUsuarios gestorUsuarios)
    {
        this.gestorUsuarios = gestorUsuarios;
    }

    public void RegistrarPago(Socio socio)
    {
        socio.PagarCuota();
        ActualizarPagosEnArchivo("pagos.txt");
        gestorUsuarios.GuardarUsuariosEnArchivo("usuarios.txt");
        Console.WriteLine("Cuota pagada correctamente" + " " +
            "Puntos disponibles: " +
             + socio.Puntos);
        
    }
    public bool ComprobarPuntosSocio(Socio socio)
    {
       
        if (socio == null)
        {
            return true;
        }

        return socio.Puntos >= 1;
    }
    public void RestarPuntosSocio(Socio socio, int puntos)
    {
        if (socio != null)
        {

            socio.Puntos -= puntos;

         // Actualizamos el archivo de pagos con los nuevos puntos del socio
            ActualizarPagosEnArchivo("pagos.txt");
        }
    }
    public void AgregarPuntosSocio(Socio socio, int puntos)
    {
        if (socio != null)
        {

            socio.Puntos += puntos;

            ActualizarPagosEnArchivo("pagos.txt");
        }
    }
    public void ActualizarPagosEnArchivo(string archivo)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            foreach (Usuario usuario in gestorUsuarios.ObtenerListaUsuarios())
            {
                if (usuario is Socio socio)
                {
                    // Formato de la línea: "ID del socio Puntos del socio"
                    writer.WriteLine(socio.ObtenerId() + " " + socio.Puntos);
                }
            }
        }
    }
    public void CargarPagosDesdeArchivo(string archivo)
    {
        if (File.Exists(archivo))
        {
            using (StreamReader reader = new StreamReader(archivo))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] datos = line.Split(' ');
                    string id = datos[0];
                    int puntos = int.Parse(datos[1]);

                    Usuario usuario = gestorUsuarios.ObtenerUsuarioPorId(id);
                    if (usuario is Socio socio)
                    {
                        socio.Puntos = puntos;
                    }
                }
            }
        }
    }
}

