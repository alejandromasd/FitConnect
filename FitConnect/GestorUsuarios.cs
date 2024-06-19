using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
public class GestorUsuarios
{
    private List<Usuario> listaUsuarios;

    public GestorUsuarios()
    {
        this.listaUsuarios = new List<Usuario>();
    }

    public void AgregarUsuario(Usuario usuario)
    {
        this.listaUsuarios.Add(usuario);
    }

    public void EliminarUsuario(Usuario usuario)
    {
        this.listaUsuarios.Remove(usuario);
    }

    public List<Usuario> ObtenerListaUsuarios()
    {
        return listaUsuarios;
    }

    public Usuario Autenticar(string id, string contraseña)
    {
        foreach (Usuario usuario in listaUsuarios)
        {
            if (usuario.ObtenerId().Equals(id) 
                && usuario.ObtenerContraseña().Equals(contraseña))
            {
                return usuario;
            }
        }
        return null;
    }
    public static string PedirNoVacio(string mensaje)
    {
        while (true)
        {
            Console.WriteLine(mensaje);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("La entrada no puede estar vacía. " +
                "Por favor, inténtalo de nuevo.");
        }
    }
    public static string LeerContraseña()
    {
        string contraseña = "";
        ConsoleKeyInfo info = Console.ReadKey(true);
        while (info.Key != ConsoleKey.Enter)
        {
            if (info.Key != ConsoleKey.Backspace)
            {
                Console.Write("*");
                contraseña += info.KeyChar;
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                if (!string.IsNullOrEmpty(contraseña))
                {
                    // Borrar el caracter en la consola
                    contraseña = contraseña.Substring(0, contraseña.Length - 1);
                    int pos = Console.CursorLeft;
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(pos - 1, Console.CursorTop);
                }
            }
            info = Console.ReadKey(true);
        }
      
        Console.WriteLine();
        return contraseña;
    }
    public Usuario IniciarSesion()
    {
        Console.WriteLine("Introduce el ID:");
        string id = Console.ReadLine();
        Console.WriteLine("Introduce la contraseña:");
        string contraseña = LeerContraseña();

        Usuario usuario = Autenticar(id, contraseña);

        if (usuario != null)
        {        
            return usuario;
        }
        else
        {
            return null;
        }
    }
    public Usuario ObtenerUsuarioPorId(string id)
    {
        foreach (Usuario usuario in listaUsuarios)
        {
            if (usuario.ObtenerId().Equals(id))
            {
                return usuario;
            }
        }
        return null;
    }

    public bool ExisteUsuario(string idUsuario)
    {
        return listaUsuarios.Any(usuario => 
        usuario.ObtenerId().Equals(idUsuario));
    }
    public void AgregarNuevoUsuario(GestorUsuarios gestorUsuarios)
    {
        Console.WriteLine("Introduce el tipo de usuario a " +
            "agregar (socio, entrenador):");
        string tipoUsuario = Console.ReadLine().ToLower();

        Console.WriteLine("Introduce el nombre:");
        string nombre = Console.ReadLine();
        Console.WriteLine("Introduce el apellido:");
        string apellido = Console.ReadLine();
        Console.WriteLine("Introduce la fecha de nacimiento (dd/mm/yyyy):");
        string fechaNacimiento = Console.ReadLine();
        Console.WriteLine("Introduce el ID:");
        string id = Console.ReadLine();
        if (gestorUsuarios.ExisteUsuario(id))
        {
            Console.WriteLine("Error: Un usuario con este ID ya existe.");
        }
        Console.WriteLine("Introduce la contraseña:");
        string contraseña = Console.ReadLine();

        switch (tipoUsuario)
        {
            case "socio":
                string tipoMembresia;
                do
                {
                    Console.WriteLine("Introduce el tipo de membresía " +
                        "(oro o platino):");
                    tipoMembresia = Console.ReadLine().ToLower();

                    if (!tipoMembresia.Equals("oro") && 
                        !tipoMembresia.Equals("platino"))
                    {
                        Console.WriteLine("Error: Por favor introduce " +
                            "'oro' o 'platino'.");
                    }
                } while (!tipoMembresia.Equals("oro") && 
                !tipoMembresia.Equals("platino"));

                Console.WriteLine("Introduce la fecha de " +
                    "la membresía (dd/mm/yyyy):");
                string fechaMembresia = Console.ReadLine();
                
                gestorUsuarios.AgregarUsuario(new Socio(nombre, 
                    apellido, fechaNacimiento, id, contraseña, 
                    tipoMembresia, fechaMembresia, false));
                break;

            case "entrenador":
                Console.WriteLine("Introduce la especialidad:");
                string especialidad = Console.ReadLine();

                gestorUsuarios.AgregarUsuario(new Entrenador(nombre, 
                    apellido, fechaNacimiento, id, contraseña, especialidad));
                break;

            default:
                Console.WriteLine("Tipo de usuario no reconocido.");
                break;
        }
        // Guardamos la lista actualizada de usuarios en el archivo
        try
        {
            gestorUsuarios.GuardarUsuariosEnArchivo("usuarios.txt");
            
        }
        catch (IOException e)
        {
            Console.WriteLine("Error guardando usuarios en archivo: " 
                + e.Message);
        }
    }
    public void MostrarUsuarios()
    {
        // Cargamos los usuarios desde el archivo
        CargarUsuariosDesdeArchivo("usuarios.txt");

        for (int i = 0; i < listaUsuarios.Count; i++)
        {
            Console.WriteLine(listaUsuarios[i].ToString());
        }
    }
    public void ModificarUsuario()
    {
        Console.WriteLine("Introduce el ID del usuario a modificar:");
        string PedirUsuario = Console.ReadLine();
        Usuario usuario = ObtenerUsuarioPorId(PedirUsuario);

        if (usuario != null)
        {
            Console.WriteLine("Datos actuales del usuario:");
            Console.WriteLine(usuario.ToString());
            Console.WriteLine("Introduce el nuevo nombre " +
                "(presiona enter para no cambiar):");
            string nombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nombre))
            {
                usuario.EstablecerNombre(nombre);
            }

            Console.WriteLine("Introduce el nuevo apellido " +
                "(presiona enter para no cambiar):");
            string apellido = Console.ReadLine();
            if (!string.IsNullOrEmpty(apellido))
            {
                usuario.EstablecerApellido(apellido);
            }

            Console.WriteLine("Introduce la nueva fecha de nacimiento " +
                "(presiona enter para no cambiar):");
            string fechaNacimiento = Console.ReadLine();
            if (!string.IsNullOrEmpty(fechaNacimiento))
            {
                usuario.EstablecerFechaNacimiento(fechaNacimiento);
            }
            Console.WriteLine("Introduce el nuevo ID " +
                "(presiona enter para no cambiar):");
            string nuevoId = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoId))
            {
                usuario.EstablecerId(nuevoId);
            }
            Console.WriteLine("Introduce la nueva contraseña " +
                "(presiona enter para no cambiar):");
            string nuevaContra = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaContra))
            {
                usuario.EstablecerContraseña(nuevaContra);
            }

            if (usuario is Socio socio)
            {
                Console.WriteLine("Introduce el nuevo tipo de membresía " +
                    "(presiona enter para no cambiar):");
                string tipoMembresia = Console.ReadLine();
                if (!string.IsNullOrEmpty(tipoMembresia))
                {
                    socio.EstablecerTipoMembresia(tipoMembresia);
                }

                Console.WriteLine("Introduce la nueva fecha de membresía " +
                    "(presiona enter para no cambiar):");
                string fechaMembresia = Console.ReadLine();
                if (!string.IsNullOrEmpty(fechaMembresia))
                {
                    socio.EstablecerFechaMembresia(fechaMembresia);
                }
            }
            else if (usuario is Entrenador entrenador)
            {
                Console.WriteLine("Introduce la nueva especialidad " +
                    "(presiona enter para no cambiar):");
                string especialidad = Console.ReadLine();
                if (!string.IsNullOrEmpty(especialidad))
                {
                    entrenador.EstablecerEspecialidad(especialidad);
                }
            }
            Console.WriteLine("Datos del usuario modificados correctamente.");
            GuardarUsuariosEnArchivo("usuarios.txt");
        }
        else
        {
            Console.WriteLine("No se encontró un usuario con ese ID.");
        }
    }
    public void EliminarUsuario()
    {
        Console.Write("Introduce el ID del usuario que quieres eliminar: ");
        string idUsuario = Console.ReadLine();

        // Buscamos el usuario
        Usuario usuarioEliminar = null;
        foreach (Usuario usuario in listaUsuarios)
        {
            if (usuario.ObtenerId().Equals(idUsuario))
            {
                usuarioEliminar = usuario;
                break;
            }
        }

        if (usuarioEliminar == null)
        {
            Console.WriteLine("No se encontró un usuario con ese ID.");
            return;
        }

        // Mostramos información del usuario
        Console.WriteLine(usuarioEliminar.ToString());

        Console.Write("¿Estás seguro de que quieres " +
            "eliminar este usuario? (si/no): ");
        string confirmacion = Console.ReadLine();

        if (confirmacion.ToLower() == "si")
        {
            listaUsuarios.Remove(usuarioEliminar);
            Console.WriteLine("Usuario eliminado.");
            GuardarUsuariosEnArchivo("usuarios.txt");
        }
        else
        {
            Console.WriteLine("Operación cancelada.");
        }
    }
    public void MostrarInfoCuotaSocios()
    {

        foreach (Usuario usuario in listaUsuarios)
        {

            if (usuario is Socio socio)
            {
                
                Console.WriteLine(socio.ObtenerNombre() + ": cuota " +
                                  (socio.EstaCuotaPagada() ? 
                                  "pagada" : "no pagada") +
                                  ", tipo de cuota: " + 
                                  socio.ObtenerTipoMembresia());
            }
        }
        
    }
    public void GuardarUsuariosEnArchivo(string nombreArchivo)
    {
        using (StreamWriter sw = new StreamWriter(nombreArchivo))
        {
            foreach (Usuario usuario in listaUsuarios)
            {
                if (usuario is Administrador)
                {
                    sw.WriteLine("Admin," + usuario.ObtenerNombre() 
                        + "," + usuario.ObtenerApellido() + "," 
                        + usuario.ObtenerFechaNacimiento()
                        + "," + usuario.ObtenerId() + "," + 
                        usuario.ObtenerContraseña());
                }
                else if (usuario is Socio)
                {
                    Socio miembro = (Socio)usuario;
                    sw.WriteLine("Socio," + usuario.ObtenerNombre() + "," +
                        usuario.ObtenerApellido() + "," + 
                        usuario.ObtenerFechaNacimiento() + "," +
                        usuario.ObtenerId() + "," + 
                        usuario.ObtenerContraseña() + "," + 
                        miembro.ObtenerTipoMembresia()
                        + "," + miembro.ObtenerFechaMembresia() + "," + 
                        miembro.EstaCuotaPagada());
                }
                else if (usuario is Entrenador)
                {
                    Entrenador entrenador = (Entrenador)usuario;
                    sw.WriteLine("Entrenador," + usuario.ObtenerNombre() + "," + 
                        usuario.ObtenerApellido()
                        + "," + usuario.ObtenerFechaNacimiento() + "," + 
                        usuario.ObtenerId() + "," 
                        + usuario.ObtenerContraseña() + "," + 
                        entrenador.ObtenerEspecialidad());
                }
            }
        }
    }
    public void CargarUsuariosDesdeArchivo(string nombreArchivo)
    {
        listaUsuarios.Clear();
        using (StreamReader sr = new StreamReader(nombreArchivo))
        {
            string linea;
            while ((linea = sr.ReadLine()) != null)
            {
                string[] datos = linea.Split(",");
                string tipoUsuario = datos[0];

                if (tipoUsuario.Equals("Admin"))
                {
                    listaUsuarios.Add(new Administrador(datos[1], datos[2], 
                        datos[3], datos[4], datos[5]));
                }
                else if (tipoUsuario.Equals("Socio"))
                {
                    if (datos[8] == "True")
                    {
                        bool cuotaPagada = true;
                        listaUsuarios.Add(new Socio(datos[1], datos[2], datos[3],
                            datos[4], datos[5], datos[6], datos[7],cuotaPagada));
                    }
                    else
                    {
                        bool cuotaPagada = false;
                        listaUsuarios.Add(new Socio(datos[1], datos[2], datos[3],
                            datos[4], datos[5], datos[6], datos[7], cuotaPagada));
                    }
                        
                }
                else if (tipoUsuario.Equals("Entrenador"))
                {
                    listaUsuarios.Add(new Entrenador(datos[1], datos[2], datos[3],
                        datos[4], datos[5], datos[6]));
                }
            }
        }
    }
    public void CopiaSeguridadUsuarios(string nombreArchivoPDF)
    {
        Document document = new Document();

        PdfWriter.GetInstance(document, new FileStream(nombreArchivoPDF,
            FileMode.Create));

        document.Open();

        Paragraph paragraph =
            new Paragraph("Copia de seguridad de usuarios\n\n");
        document.Add(paragraph);

        // Obtenemos lista de usuarios y los ordenamos en el pdf
        var administradores = listaUsuarios.OfType<Administrador>().ToList();
        var socios = listaUsuarios.OfType<Socio>().ToList();
        var entrenadores = listaUsuarios.OfType<Entrenador>().ToList();

        foreach (Administrador admin in administradores)
        {
            Paragraph adminParagraph =
                new Paragraph("Admin: " + admin.ObtenerNombre() + " "
                + admin.ObtenerApellido() + ", ID: " + admin.ObtenerId() +
                "\n");
            document.Add(adminParagraph);
        }

        foreach (Socio socio in socios)
        {
            Paragraph socioParagraph =
                new Paragraph("Socio: " + socio.ObtenerNombre() + " "
                + socio.ObtenerApellido() + ", ID: " +
                socio.ObtenerId() + ", Membresía: "
                + socio.ObtenerTipoMembresia() + "\n");
            document.Add(socioParagraph);
        }

        foreach (Entrenador entrenador in entrenadores)
        {
            Paragraph entrenadorParagraph =
                new Paragraph("Entrenador: " + entrenador.ObtenerNombre() + " "
                + entrenador.ObtenerApellido() + ", ID: " +
                entrenador.ObtenerId() + ", Especialidad: "
                + entrenador.ObtenerEspecialidad() + "\n");
            document.Add(entrenadorParagraph);
        }

        document.Close();

        Console.WriteLine("Copia de seguridad guardada correctamente");
    }

}