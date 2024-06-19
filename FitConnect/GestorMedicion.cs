using System;
using System.Linq;
using System.Drawing;

public class GestorMediciones
{
    private GestorUsuarios gestorUsuarios;
    private Entrenador entrenadorActual;

    public GestorMediciones(GestorUsuarios gestorUsuarios, 
        Entrenador entrenadorActual)
    {
        this.gestorUsuarios = gestorUsuarios;
        this.entrenadorActual = entrenadorActual;
    }
    public void RealizarMedicion()
    {
        // Mostrar la lista de usuarios
        var usuarios = this.gestorUsuarios.ObtenerListaUsuarios();
        foreach (var usuario in usuarios)
        {
            // Solo mostrar los socios
            if (usuario is Socio)
            {
                Console.WriteLine(usuario.ObtenerNombre());
            }
        }
        string nombreUsuario = 
            GestorUsuarios.PedirNoVacio("Introduce el nombre del " +
            "usuario al que realizar la medición:");
        Socio socioAMedir = 
            usuarios.FirstOrDefault(u => u.ObtenerNombre() 
            == nombreUsuario) as Socio;
        if (socioAMedir == null)
        {
            Console.WriteLine("No se encontró al usuario especificado.");
            return;
        }

        string pesoCorporal = 
            GestorUsuarios.PedirNoVacio("Introduce el peso " +
            "corporal del usuario:");
        string masaMuscular = 
            GestorUsuarios.PedirNoVacio("Introduce la masa " +
            "muscular del usuario:");
        string masaGrasa = 
            GestorUsuarios.PedirNoVacio("Introduce la masa " +
            "grasa del usuario:");
        string objetivoPrincipal = 
            GestorUsuarios.PedirNoVacio("Introduce el objetivo " +
            "principal del socio:");

        Medicion nuevaMedicion = new Medicion(pesoCorporal, masaMuscular, 
            masaGrasa, objetivoPrincipal, DateTime.Now, 
            entrenadorActual.ObtenerId());
        socioAMedir.Mediciones.Add(nuevaMedicion);
        GuardarMedicionesEnArchivo(socioAMedir);
        CrearGraficoMediciones(socioAMedir);
        Console.WriteLine("¡Medición realizada y añadida al usuario!");
    }
    public void MostrarMedicionesEntrenador()
    {
        var usuarios = this.gestorUsuarios.ObtenerListaUsuarios();
        var sociosMedidosPorEntrenador = usuarios.OfType<Socio>().Where(s =>
        s.Mediciones.Any(m => m.IdEntrenador == entrenadorActual.ObtenerId()));
        foreach (var socio in sociosMedidosPorEntrenador)
        {
            Console.WriteLine("Mediciones de " + socio.ObtenerNombre() + ":");
            foreach (var medicion in socio.Mediciones)
            {
                if (medicion.IdEntrenador == entrenadorActual.ObtenerId())
                {
                    Console.WriteLine("Peso Corporal: " + medicion.PesoCorporal +
                        ", Masa Muscular: " + medicion.MasaMuscular +
                        ", Masa Grasa: " + medicion.MasaGrasa +
                        ", Objetivo: " + medicion.ObjetivoPrincipal +
                        ", Fecha: " + medicion.Fecha);
                }
            }
            Console.WriteLine();
        }
    }
    public void GuardarMedicionesEnArchivo(Socio socio)
    {
        try
        {
            using (StreamWriter file = 
                new StreamWriter(@"mediciones.txt", true)) 
            {
                foreach (Medicion medicion in socio.Mediciones)
                {
                    string linea = 
                        string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                        socio.ObtenerNombre(),
                        socio.ObtenerId(),
                        medicion.PesoCorporal,
                        medicion.MasaMuscular,
                        medicion.MasaGrasa,
                        medicion.ObjetivoPrincipal,
                        medicion.Fecha.ToString("yyyy-MM-dd HH:mm:ss"),
                        medicion.IdEntrenador); 

                    file.WriteLine(linea);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al guardar las " +
                "mediciones en el archivo: " + ex.Message);
        }
    }
    public void CargarMedicionesDesdeArchivo()
    {
        try
        {
            using (StreamReader file = 
                new StreamReader(@"mediciones.txt"))
            {
                string linea;
                while ((linea = file.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');
                    string nombreSocio = datos[0];
                    string idSocio = datos[1];
                    string pesoCorporal = datos[2];
                    string masaMuscular = datos[3];
                    string masaGrasa = datos[4];
                    string objetivoPrincipal = datos[5];
                    DateTime fecha = 
                        DateTime.ParseExact(datos[6], "yyyy-MM-dd HH:mm:ss", 
                        null);
                    string idEntrenador = datos[7];                     
                    Socio socio = 
                        this.gestorUsuarios.ObtenerListaUsuarios().FirstOrDefault(u 
                        => u.ObtenerNombre() == nombreSocio && u.ObtenerId() 
                        == idSocio) as Socio;

                    if (socio == null) continue;

                    Medicion medicion = new Medicion(pesoCorporal, masaMuscular, 
                        masaGrasa, objetivoPrincipal, fecha, idEntrenador);  
                    socio.Mediciones.Add(medicion);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ocurrió un error al cargar las mediciones " +
                "desde el archivo: " + ex.Message);
        }
    }
    public void CrearGraficoMediciones(Socio socio)
    {
        const int anchoImagen = 800;
        const int altoImagen = 600;
        const int margen = 50;
        Bitmap bitmap = new Bitmap(anchoImagen, altoImagen);
        Graphics graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);

        var mediciones = socio.Mediciones;

        var pesosCorporales = 
            mediciones.Select(m => double.Parse(m.PesoCorporal)).ToList();

        int numBarras = pesosCorporales.Count;

        double maxPesoCorporal = pesosCorporales.Max();
        double minPesoCorporal = 20; 

        double anchoBarra = (anchoImagen - 2 * margen) / (double)numBarras;
        double altoMaximoBarra = altoImagen - 2 * margen;

        for (int i = 0; i < numBarras; i++)
        {
            double alturaBarra = altoMaximoBarra 
                * (pesosCorporales[i] - minPesoCorporal) 
                / (maxPesoCorporal - minPesoCorporal);
            graphics.FillRectangle(Brushes.Blue, margen + (float)(i * anchoBarra),
                altoImagen - margen - (float)alturaBarra, (float)anchoBarra, 
                (float)alturaBarra);

            string etiqueta = mediciones[i].Fecha.ToString("dd/MM/yyyy");
            graphics.DrawString(etiqueta, new Font("Arial", 12), 
                Brushes.Black, margen + (float)(i * anchoBarra), 
                altoImagen - margen + 5);

            string etiquetaPeso = $"{pesosCorporales[i]} kg";
            graphics.DrawString(etiquetaPeso, new Font("Arial", 12),
                Brushes.Red, margen + (float)(i * anchoBarra), 
                altoImagen - margen - (float)alturaBarra - 20);
        }

        bitmap.Save($"{socio.ObtenerNombre()}_grafico.png", 
            System.Drawing.Imaging.ImageFormat.Png);
    }

}

