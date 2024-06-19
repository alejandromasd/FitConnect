using System;
public class InterfazUsuario
{
    private GestorUsuarios gestorUsuarios;

    public InterfazUsuario(GestorUsuarios gestorUsuarios)
    {
        this.gestorUsuarios = gestorUsuarios;
    }
    public static int MenuPrincipal()
    {
        // Color de fondo y letra de la consola
        Console.BackgroundColor = ConsoleColor.DarkBlue; 
        Console.ForegroundColor = ConsoleColor.White; 
        Console.Clear(); 
        string[] lineas = {
        "¡Bienvenido a FitConnect!",
        "-------------------------",
        "1. Iniciar sesión como administrador",
        "2. Iniciar sesión como socio",
        "3. Iniciar sesión como entrenador",
        "4. Iniciar sesión como invitado",
        "5. Cerrar sesión"
    };

        // Longitud del marco
        int frameWidth = 40; 

        // Creamos las líneas de borde
        string bordeHorizontal = new string('*', frameWidth);
        string bordeVertical = "*" + new string(' ', frameWidth - 2) + "*";

        // Lineas borde superior
        PrintCenteredLine(bordeHorizontal);
        PrintCenteredLine(bordeVertical);

        // Imprimimos las lineas dentro del marco
        foreach (string linea in lineas)
        {
            int padding = (frameWidth - 2 - linea.Length) / 2;
            string paddedLine = "* " + new string(' ', padding) 
                + linea + new string(' ', frameWidth - 2 - padding 
                - linea.Length) + " *";
            PrintCenteredLine(paddedLine);
        }

        // Imprimimos las lineas del borde inferior
        PrintCenteredLine(bordeVertical);
        PrintCenteredLine(bordeHorizontal);

        int opcion;
        if (!int.TryParse(Console.ReadLine(), out opcion))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }

        return opcion;
    }
    private static void PrintCenteredLine(string line)
    {
        Console.SetCursorPosition((Console.WindowWidth - 
            line.Length) / 2, Console.CursorTop);
        Console.WriteLine(line);
    }
    public static int MostrarMenuAdministrador()

    {
        Console.BackgroundColor = ConsoleColor.DarkRed; 
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Pulsa Intro para continuar");
        Console.ReadKey();
        Console.Clear(); 
        Console.WriteLine("--------Menú Administrador----------");
        Console.WriteLine("1. Añadir nuevo miembro");
        Console.WriteLine("2. Modificar datos de un miembro");
        Console.WriteLine("3. Eliminar un miembro");
        Console.WriteLine("4. Añadir nueva actividad");
        Console.WriteLine("5. Eliminar una actividad");
        Console.WriteLine("6. Modificar datos de una actividad");
        Console.WriteLine("7. Mostrar estado de cuota de los socios");
        Console.WriteLine("8. Guardado de seguridad usuarios");
        Console.WriteLine("9. Salir al menu pricipal");

        int opcionAdmin;
        if (!int.TryParse(Console.ReadLine(), out opcionAdmin))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }

        return opcionAdmin;
    }

    public static int MostrarMenuSocio()
    {
        Console.BackgroundColor = ConsoleColor.DarkGreen; 
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine("Pulsa Intro para continuar");
        Console.ReadKey();
        Console.Clear(); 
        Console.WriteLine("-------Menú Socio---------");
        Console.WriteLine("1. Pagar cuota");
        Console.WriteLine("2. Reservar una actividad");
        Console.WriteLine("3. Ver mis reservas");
        Console.WriteLine("4. Cancelar una reserva");
        Console.WriteLine("5. Escribir una valoracion");
        Console.WriteLine("6. Ver reseñas del club");
        Console.WriteLine("7. Generar código de invitación");
        Console.WriteLine("8. Salir");

        int opcionMiembro;
        if (!int.TryParse(Console.ReadLine(), out opcionMiembro))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }

        return opcionMiembro;
    }
    public static int MostrarMenuEntrenador()
    {
        Console.BackgroundColor = ConsoleColor.DarkGray; 
        Console.ForegroundColor = ConsoleColor.White; 
        Console.WriteLine("Pulsa Intro para continuar");
        Console.ReadKey();
        Console.Clear(); 
        Console.WriteLine("--------Menú Entrenador------------");
        Console.WriteLine("1. Añadir material");
        Console.WriteLine("2. Ver todo el material disponible");
        Console.WriteLine("3. Modificar material");
        Console.WriteLine("4. Eliminar material");
        Console.WriteLine("5. Realizar medicion a un socio");
        Console.WriteLine("6. Mis mediciones");

        Console.WriteLine("7. Salir");

        int opcionEntrenador;
        if (!int.TryParse(Console.ReadLine(), out opcionEntrenador))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
        }

        return opcionEntrenador;
    }
    public static int MostrarMenuInvitado()
    {
        
        Console.Clear();
        Console.WriteLine("Menú Invitado");
        Console.WriteLine("----------------");
        Console.WriteLine("1.Reservar actividad");
        Console.WriteLine("2. Volver al menú principal");
        Console.Write("Elige una opción: ");
        int opcionInvitado;
        if (!int.TryParse(Console.ReadLine(), out opcionInvitado))
        {
            Console.WriteLine("Por favor, introduce un número válido.");
            
        }

        return opcionInvitado;
    }
}
