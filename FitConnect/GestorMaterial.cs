using System;
using OfficeOpenXml;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Data.SQLite;
public class GestorMaterial
{
    private List<Material> materiales;

    public GestorMaterial()
    {
        //Llamamos a la función para crear la tabla
        CrearTablaMaterial(); 
        materiales = new List<Material>();
        CargarMaterialesDesdeArchivo("Materiales.db");
    }

    public void AgregarMaterial(Material material)
    {
        using (SQLiteConnection con = 
            new SQLiteConnection("Data Source=Materiales.db"))
        {
            con.Open();
            string sql = "INSERT INTO Materiales (TipoMaterial, Actividad," +
                " UnidadesDisponibles, Estado) VALUES (@TipoMaterial," +
                " @Actividad, @UnidadesDisponibles, @Estado)";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@TipoMaterial", 
                    material.TipoMaterial);
                cmd.Parameters.AddWithValue("@Actividad", 
                    material.Actividad);
                cmd.Parameters.AddWithValue("@UnidadesDisponibles", 
                    material.UnidadesDisponibles);
                cmd.Parameters.AddWithValue("@Estado", 
                    material.Estado);

                cmd.ExecuteNonQuery();
            }
        }
       //Recargamos la lista de materiales 
        CargarMaterialesDesdeArchivo("Materiales.db");
    }
    public void MostrarMateriales()
    {
        foreach (Material material in materiales)
        {
            Console.WriteLine("Tipo de material: " + 
                material.TipoMaterial);
            Console.WriteLine("Actividad: " + 
                material.Actividad);
            Console.WriteLine("Unidades disponibles: " +
                material.UnidadesDisponibles);
            Console.WriteLine("Estado: " + 
                material.Estado);
            Console.WriteLine("--------------------");
        }
    }
    public void AnyadirMaterial()
    {
        string tipoMaterial = 
            GestorUsuarios.PedirNoVacio("Introduce el tipo de material:");
        string actividad = 
            GestorUsuarios.PedirNoVacio("Introduce la actividad para la que " +
            "se usará el material:");
        Console.WriteLine("Introduce las unidades disponibles del material:");
        int unidadesDisponibles;
        while (!int.TryParse(Console.ReadLine(), out unidadesDisponibles))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }
        string estado = GestorUsuarios.PedirNoVacio("Introduce el " +
            "estado del material:");
        Material nuevoMaterial = new Material(tipoMaterial, actividad, 
            unidadesDisponibles, estado);
        AgregarMaterial(nuevoMaterial);
        Console.WriteLine("Material añadido con éxito!");
    }
    public void CrearTablaMaterial()
    {
        using (SQLiteConnection conn = 
            new SQLiteConnection("Data Source=Materiales.db;Version=3;"))
        {
            conn.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS Materiales (
                    ID INTEGER PRIMARY KEY,
                    TipoMaterial TEXT NOT NULL,
                    Actividad TEXT NOT NULL,
                    UnidadesDisponibles INTEGER NOT NULL,
                    Estado TEXT NOT NULL)";

            using (SQLiteCommand cmd = 
                new SQLiteCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
    public void ModificarMaterial()
    {
        // Mostramos todos los materiales disponibles
        MostrarMateriales();

        Console.WriteLine("Introduce el nombre del material " +
            "que quieres modificar:");
        string nombreMaterial = Console.ReadLine();

        Material materialAModificar = 
            materiales.FirstOrDefault(m => m.TipoMaterial == nombreMaterial);

        if (materialAModificar == null)
        {
            Console.WriteLine("No se encontró el material especificado.");
            return;
        }
        Console.WriteLine("Introduce el nuevo tipo de material " +
            "(presiona enter para no cambiar):");
        string nuevoTipoMaterial = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoTipoMaterial))
        {
            materialAModificar.TipoMaterial = nuevoTipoMaterial;
        }

        Console.WriteLine("Introduce la nueva actividad para la que " +
            "se usará el material (presiona enter para no cambiar):");
        string nuevaActividad = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevaActividad))
        {
            materialAModificar.Actividad = nuevaActividad;
        }

        Console.WriteLine("Introduce las nuevas unidades disponibles " +
            "del material (presiona enter para no cambiar):");
        string nuevaUnidadesStr = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevaUnidadesStr) && 
            int.TryParse(nuevaUnidadesStr, out int nuevasUnidadesDisponibles))
        {
            materialAModificar.UnidadesDisponibles 
                = nuevasUnidadesDisponibles;
        }
        else if (!string.IsNullOrEmpty(nuevaUnidadesStr))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }

        Console.WriteLine("Introduce el nuevo estado del material " +
            "(presiona enter para no cambiar):");
        string nuevoEstado = Console.ReadLine();
        if (!string.IsNullOrEmpty(nuevoEstado))
        {
            materialAModificar.Estado = nuevoEstado;
        }

        using (SQLiteConnection con = 
            new SQLiteConnection("Data Source=Materiales.db"))
        {
            con.Open();

            string sql = "UPDATE Materiales SET TipoMaterial " +
                "= @TipoMaterial, Actividad = @Actividad, UnidadesDisponibles =" +
                " @UnidadesDisponibles, Estado = @Estado WHERE TipoMaterial =" +
                " @NombreAnterior";

            using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@TipoMaterial",
                    materialAModificar.TipoMaterial);
                cmd.Parameters.AddWithValue("@Actividad",
                    materialAModificar.Actividad);
                cmd.Parameters.AddWithValue("@UnidadesDisponibles",
                    materialAModificar.UnidadesDisponibles);
                cmd.Parameters.AddWithValue("@Estado",
                    materialAModificar.Estado);
                cmd.Parameters.AddWithValue("@NombreAnterior", 
                    nombreMaterial);
                cmd.ExecuteNonQuery();
            }
        }
        CargarMaterialesDesdeArchivo("Materiales.db");
    }

    public void EliminarMaterial()
    {
        MostrarMateriales();

        Console.WriteLine("Introduce el nombre del material que " +
            "quieres eliminar:");
        string nombreMaterial = Console.ReadLine();

        Material materialAEliminar = 
            materiales.FirstOrDefault(m => m.TipoMaterial 
            == nombreMaterial);

        if (materialAEliminar == null)
        {
            Console.WriteLine("No se encontró el material especificado.");
            return;
        }
        Console.WriteLine("Estás seguro de que quieres eliminar el material " 
            + materialAEliminar.TipoMaterial + "? (s/n)");
        string confirmacion = Console.ReadLine();
        if (confirmacion.ToLower() == "s")
        {
            using (SQLiteConnection con = 
                new SQLiteConnection("Data Source=Materiales.db"))
            {
                con.Open();

                string sql = "DELETE FROM Materiales WHERE TipoMaterial = " +
                    "@TipoMaterial";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@TipoMaterial", 
                        nombreMaterial);

                    cmd.ExecuteNonQuery();
                }
            }
            CargarMaterialesDesdeArchivo("Materiales.db");
            Console.WriteLine("Material eliminado correctamente!");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }
    public void CargarMaterialesDesdeArchivo(string archivo)
    {
        materiales.Clear();

        using (SQLiteConnection con = 
            new SQLiteConnection("Data Source=" + archivo))
        {
            con.Open();

            using (SQLiteCommand cmd = new SQLiteCommand(con))
            {
                cmd.CommandText = "SELECT * FROM Materiales";

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Material material = new Material(
                            reader["TipoMaterial"].ToString(),
                            reader["Actividad"].ToString(),
                            int.Parse(reader["UnidadesDisponibles"].ToString()),
                            reader["Estado"].ToString());

                        materiales.Add(material);
                    }
                }
            }
        }
    }
}

