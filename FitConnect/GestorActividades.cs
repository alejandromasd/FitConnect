using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using OfficeOpenXml;
public class GestorActividades
{
    private List<Actividad> listaActividades;

    public GestorActividades()
    {
        this.listaActividades = new List<Actividad>();
    }
    public void AgregarActividad(Actividad actividad)
    {
        this.listaActividades.Add(actividad);
    }
    public void EliminarActividad(Actividad actividad)
    {
        this.listaActividades.Remove(actividad);
    }
    public List<Actividad> ObtenerListaActividades()
    {
        return listaActividades;
    }
    public Actividad ObtenerActividadPorNombre(string nombre)
    {
        foreach (Actividad actividad in listaActividades)
        {
            if (actividad.ObtenerNombre().Equals(nombre))
            {
                return actividad;
            }
        }
        return null;
    }
    public void VerActividades()
    {
        if (listaActividades.Count == 0)
        {
            Console.WriteLine("No hay actividades disponibles.");
        }
        else
        {
            Console.WriteLine("Actividades:");
            foreach (Actividad actividad in listaActividades)
            {
                Console.WriteLine("Nombre: " + 
                    actividad.ObtenerNombre());
                Console.WriteLine("Descripción: " + 
                    actividad.ObtenerDescripcion());
                Console.WriteLine("Fecha: " + 
                    actividad.ObtenerFecha());
                Console.WriteLine("Hora: " + 
                    actividad.ObtenerHora());
                Console.WriteLine("--------------------");
            }
        }
    }
    public Actividad SeleccionarActividad()
    {
        VerActividades();
        Console.WriteLine("Ingresa el nombre de la actividad " +
            "que deseas reservar:");
        string nombreActividad = Console.ReadLine();

        foreach (Actividad actividad in listaActividades)
        {
            if (actividad.ObtenerNombre().ToLower() 
                == nombreActividad.ToLower())
            {
                return actividad;
            }
        }
        Console.WriteLine("No se encontró ninguna actividad " +
            "con ese nombre.");
        return null;
    }
    public void GuardarActividadesEnExcel(string nombreArchivo)
    {
        using (ExcelPackage excel = new ExcelPackage())
        {
            ExcelWorksheet worksheet = 
                excel.Workbook.Worksheets.Add("Actividades");
            // Agregar encabezados
            worksheet.Cells[1, 1].Value = "Nombre";
            worksheet.Cells[1, 2].Value = "Descripción";
            worksheet.Cells[1, 3].Value = "Fecha";
            worksheet.Cells[1, 4].Value = "Hora";
            // Agregar datos
            int primeraFila = 2;
            foreach (Actividad actividad in listaActividades)
            {
                worksheet.Cells[primeraFila, 1].Value = 
                    actividad.ObtenerNombre();
                worksheet.Cells[primeraFila, 2].Value = 
                    actividad.ObtenerDescripcion();
                worksheet.Cells[primeraFila, 3].Value = 
                    actividad.ObtenerFecha();
                worksheet.Cells[primeraFila, 4].Value = 
                    actividad.ObtenerHora();
                primeraFila++;
            }
            // Guardamos archivo
            excel.SaveAs(new FileInfo(nombreArchivo));
        }
    }
    public void CargarActividadesDesdeExcel(string nombreArchivo)
    {
        using (ExcelPackage excel = 
            new ExcelPackage(new FileInfo(nombreArchivo)))
        {
            ExcelWorksheet worksheet = 
                excel.Workbook.Worksheets["Actividades"];
            int rowCount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowCount; row++)
            {
                string nombreActividad =
                    worksheet.Cells[row, 1].Value.ToString();
                string descripcionActividad = 
                    worksheet.Cells[row, 2].Value.ToString();
                string fechaActividad = 
                    worksheet.Cells[row, 3].Value.ToString();
                string horaActividad =
                    worksheet.Cells[row, 4].Value.ToString();

                Actividad actividad = new Actividad(nombreActividad, 
                    descripcionActividad, fechaActividad, horaActividad);
                listaActividades.Add(actividad);
            }
        }
    } 
    public void AgregarActividad()
    {
        Console.WriteLine("Introduce el nombre de la actividad:");
        string nombreActividad = Console.ReadLine();
        Console.WriteLine("Introduce la descripción de la actividad:");
        string descripcionActividad = Console.ReadLine();
        Console.WriteLine("Introduce la fecha de la actividad (dd/mm/yyyy):");
        string fechaActividad = Console.ReadLine();
        Console.WriteLine("Introduce la hora de la actividad (hh:mm):");
        string horaActividad = Console.ReadLine();
        this.AgregarActividad(new Actividad(nombreActividad, 
            descripcionActividad, fechaActividad, horaActividad));
        Console.WriteLine("Actividad agregada exitosamente.");
        try
        {
            
            this.GuardarActividadesEnExcel("actividades.xlsx");
        }
        catch (Exception e)
        {
            Console.WriteLine("Error al guardar los datos de las " +
                "actividades en el archivo." + e.Message);
        }
    }
    public void EliminarActividad()
    {
        Console.WriteLine("Introduce el nombre de la actividad " +
            "que deseas eliminar:");
        string nombreActividad = Console.ReadLine();

        Actividad actividadAEliminar = 
            this.ObtenerActividadPorNombre(nombreActividad);

        if (actividadAEliminar != null)
        {
            Console.WriteLine("¿Estás seguro de que deseas " +
                "eliminar la actividad? (s/n)");
            string respuesta = Console.ReadLine();

            if (respuesta.ToLower() == "s")
            {
                this.EliminarActividad(actividadAEliminar);
                Console.WriteLine("Actividad eliminada exitosamente.");

                // Guarda los cambios en el archivo
                try
                {
                    this.GuardarActividadesEnExcel("actividades.xlsx");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al guardar los datos de " +
                        "las actividades en el archivo." + e.Message);
                }
            }
            else
            {
                Console.WriteLine("La actividad no ha sido eliminada.");
            }
        }
        else
        {
            Console.WriteLine("No se encontró una actividad " +
                "con el nombre proporcionado.");
        }
    }


    public void ModificarActividad()
    {
        VerActividades();
        Console.WriteLine("Introduce el nombre de la actividad " +
            "que deseas modificar:");
        string nombreActividad = Console.ReadLine();

        Actividad actividadAModificar = 
            this.ObtenerActividadPorNombre(nombreActividad);

        if (actividadAModificar != null)
        {
            Console.WriteLine("Introduce el nuevo nombre o " +
                "presiona Enter para mantener el actual:");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                actividadAModificar.EstablecerNombre(nuevoNombre);
            }

            Console.WriteLine("Introduce la nueva descripción o " +
                "presiona Enter para mantener la actual:");
            string nuevaDescripcion = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaDescripcion))
            {
                actividadAModificar.EstablecerDescripcion(nuevaDescripcion);
            }

            Console.WriteLine("Introduce la nueva fecha (dd/mm/yyyy) o " +
                "presiona Enter para mantener la actual:");
            string nuevaFecha = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaFecha))
            {
                actividadAModificar.EstablecerFecha(nuevaFecha);
            }

            Console.WriteLine("Introduce la nueva fecha (dd/mm/yyyy) o " +
                "presiona Enter para mantener la actual:");
            string nuevaHora = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaHora))
            {
                actividadAModificar.EstablecerHora(nuevaHora);
            }

            Console.WriteLine("Datos de la actividad " +
                "modificados exitosamente.");

            // Guardar las actividades en el archivo después de modificar una actividad.
            try
            {
                this.GuardarActividadesEnExcel("actividades.xlsx");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al guardar los datos de las " +
                    "actividades en el archivo." + e.Message);
            }
        }
        else
        {
            Console.WriteLine("No se encontró una actividad con " +
                "el nombre proporcionado.");
        }
    }
}


