using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GestorValoraciones
{
    private List<Valoracion> valoraciones;
    private GestorUsuarios gestorUsuarios;
    public GestorValoraciones()
    {
        valoraciones = new List<Valoracion>();
    }
    public GestorValoraciones(GestorUsuarios gestorUsuarios) 
    {
        valoraciones = new List<Valoracion>();
        this.gestorUsuarios = gestorUsuarios; 
    }
    public void AgregarValoracion(Valoracion valoracion)
    {
        valoraciones.Add(valoracion);
    }
    public void CrearYAgregarValoracion(Socio socio)
    {
        // Obtenemos el comentario y la nota del usuario
        Console.Write("Por favor, introduce tu comentario: ");
        string comentario = Console.ReadLine();
        Console.Write("Por favor, introduce tu nota (1-10): ");
        int nota = Convert.ToInt32(Console.ReadLine());

        Valoracion valoracion = new Valoracion(comentario, nota, socio);
        AgregarValoracion(valoracion);
        GuardarValoracionesEnArchivo("valoraciones.txt");
    }
    public double CalcularNotaMedia()
    {
        if (valoraciones.Count == 0)
        {
            return 0;
        }

        int total = 0;
        foreach (Valoracion valoracion in valoraciones)
        {
            total += valoracion.ObtenerNota();
        }

        return (double)total / valoraciones.Count;
    }
    public void GuardarValoracionesEnArchivo(string archivo)
    {
        using (StreamWriter writer = new StreamWriter(archivo))
        {
            foreach (Valoracion valoracion in valoraciones)
            {
               
                writer.WriteLine(valoracion.ObtenerSocio().ObtenerId() + "," +
                                 valoracion.ObtenerComentario() + "," +
                                 valoracion.ObtenerNota());
            }
        }
    }
    public void CargarValoracionesDesdeArchivo(string archivo)
    {
        if (File.Exists(archivo))
        {
            using (StreamReader reader = new StreamReader(archivo))
            {
                string linea;
                while ((linea = reader.ReadLine()) != null)
                {
                    
                    if (string.IsNullOrWhiteSpace(linea))
                        continue;

                    string[] datos = linea.Split(',');
                  
                    if (datos.Length != 3)
                    {
                        Console.WriteLine("La linea " + linea + "no tiene " +
                            "el formato correcto");
                        continue;
                    }

                    Socio socio = 
                        (Socio)gestorUsuarios.ObtenerUsuarioPorId(datos[0]);

                    string comentario = datos[1];
                    // Convertimos la nota a int para calcular la media
                    int nota = int.Parse(datos[2]);

                    Valoracion valoracion = new Valoracion(comentario, nota, socio);
                    valoraciones.Add(valoracion);
                }
            }
        }
    }
    public void MostrarValoraciones()
    {
        const int anchoID = 13;
        const int anchoComentario = 24;
        const int anchoNota = 6;
        //Usamos PadRight(int totalWidth) para ajustar las cadenas
        Console.WriteLine("+" + new string('-', anchoID) + "+" + 
            new string('-', anchoComentario) + "+" + 
            new string('-', anchoNota) + "+");
        Console.WriteLine("| " + "ID del socio".PadRight(anchoID) + 
            "| " + "Comentario".PadRight(anchoComentario) + "| " +
            "Nota".PadRight(anchoNota) + "|");
        Console.WriteLine("+" + new string('-', anchoID) + "+" +
            new string('-', anchoComentario) + "+" + 
            new string('-', anchoNota) + "+");

        foreach (Valoracion valoracion in valoraciones)
        {
            Socio socio = valoracion.ObtenerSocio();

            if (socio == null)
                continue;

            Console.WriteLine("| " + 
                socio.ObtenerId().ToString().PadRight(anchoID) + "| " + 
                valoracion.ObtenerComentario().PadRight(anchoComentario) + 
                "| " + valoracion.ObtenerNota().ToString().PadRight(anchoNota) 
                + "|");
        }
        //Mostramos nota media
        Console.WriteLine("+" + new string('-', anchoID) + "+" + 
            new string('-', anchoComentario) + "+" + 
            new string('-', anchoNota) + "+");
        Console.WriteLine("La nota media es: " + 
            CalcularNotaMedia());
    }
}

