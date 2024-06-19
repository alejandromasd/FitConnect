//Alejandro Mas Diego 1ºDAM

using OfficeOpenXml;
using System;

namespace FitConnect
{
    internal class Iniciar
    {
        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            GestorUsuarios gestorUsuarios = new GestorUsuarios();
            GestorActividades gestorActividades = new GestorActividades();
            GestorPagos gestorPagos = new GestorPagos(gestorUsuarios);
            GestorReservas gestorReservas = new GestorReservas(gestorPagos);
            GestorValoraciones gestorValoraciones = 
                new GestorValoraciones(gestorUsuarios);
            GestorInvitaciones gestorInvitaciones = 
                new GestorInvitaciones();
            GestorMaterial gestorMaterial = new GestorMaterial();
            gestorUsuarios.CargarUsuariosDesdeArchivo("usuarios.txt");
            gestorActividades.CargarActividadesDesdeExcel("actividades.xlsx");
            gestorPagos.CargarPagosDesdeArchivo("pagos.txt");
            gestorReservas.CargarReservasDesdeArchivo("reservas.txt",
                gestorUsuarios, gestorActividades);
            gestorValoraciones.
                CargarValoracionesDesdeArchivo("valoraciones.txt");
            gestorInvitaciones.
                CargarInvitacionesDesdeArchivo("invitaciones.txt");
            gestorMaterial.
                CargarMaterialesDesdeArchivo("Materiales.db");
            InterfazUsuario interfazUsuario = 
                new InterfazUsuario(gestorUsuarios);
            while (true)
            {
                int opcion = InterfazUsuario.MenuPrincipal();
                switch (opcion)
                {
                    case 1: //Menu administradores
                        Usuario usuarioAdmin = gestorUsuarios.IniciarSesion();
                        if (usuarioAdmin is Administrador)
                        {
                            Console.Clear();
                            Console.WriteLine("¡Inicio de sesión exitoso " +
                                "como administrador!");
                            int adminOption;
                            do
                            {
                                adminOption = InterfazUsuario.
                                    MostrarMenuAdministrador();
                                switch (adminOption)
                                {
                                    case 1:
                                        gestorUsuarios.
                                            AgregarNuevoUsuario(gestorUsuarios);
                                        break;
                                    case 2:
                                        gestorUsuarios.ModificarUsuario();
                                        break;
                                    case 3:
                                        gestorUsuarios.EliminarUsuario();
                                        break;
                                    case 4:
                                        gestorActividades.AgregarActividad();
                                        break;
                                    case 5:
                                        gestorActividades.EliminarActividad();
                                        break;
                                    case 6:
                                        gestorActividades.ModificarActividad();
                                        break;
                                    case 7:
                                        gestorUsuarios.MostrarInfoCuotaSocios();
                                        break;
                                    case 8:
                                        gestorUsuarios.
                                        CopiaSeguridadUsuarios("Copia" +
                                        "SeguridadUsuarios.pdf");
                                        break;
                                    case 9:
                                        Console.WriteLine("Volviendo" +
                                            " al menú principal...");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                    default:
                                        Console.WriteLine("Opción incorrecta," +
                                            "elige una opción disponible");
                                        break;
                                }
                            } while (adminOption != 9);
                        }
                        else
                        {
                            Console.WriteLine("ID o contraseña incorrecta." +
                                " Por favor, inténtelo de nuevo.");
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                    case 2: //Menu socios
                        Usuario usuarioSocio = gestorUsuarios.IniciarSesion();
                        if (usuarioSocio is Socio)
                        {
                            Console.Clear();
                            Console.WriteLine("¡Inicio de sesión" +
                                " exitoso como socio!");
                            int socioOpcion;
                            do
                            {
                                socioOpcion = 
                                    InterfazUsuario.MostrarMenuSocio();
                                switch (socioOpcion)
                                {
                                    case 1:
                                        gestorPagos.
                                            RegistrarPago
                                            ((Socio)usuarioSocio);
                                        break;
                                    case 2:
                                        Actividad actividadSeleccionada =
                                            gestorActividades.
                                            SeleccionarActividad();
                                        if (actividadSeleccionada != null)
                                        {
                                            gestorReservas.
                                                ReservarActividad
                                                ((Socio)usuarioSocio,
                                                actividadSeleccionada);
                                        }
                                        break;
                                    case 3:
                                        gestorReservas.
                                            VerReservasSocio
                                            ((Socio)usuarioSocio);
                                        break;
                                    case 4:
                                        gestorReservas.
                                            CancelarReserva
                                            ((Socio)usuarioSocio);
                                        break;
                                    case 5:
                                        gestorValoraciones.
                                            CrearYAgregarValoracion
                                            ((Socio)usuarioSocio);
                                        break;
                                    case 6:
                                        gestorValoraciones.
                                            MostrarValoraciones();
                                        break;
                                    case 7:
                                        gestorInvitaciones.
                                            CrearInvitacion
                                            ((Socio)usuarioSocio);
                                        break;
                                    case 8:
                                        Console.WriteLine("Volviendo al " +
                                            "menú principal...");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                    default:
                                        Console.WriteLine
                                            ("Opción incorrecta elige " +
                                            "una opción valida");
                                        break;
                                }

                            }
                            while (socioOpcion != 8);
                        }
                        else
                        {
                            Console.WriteLine("ID o contraseña incorrecta. " +
                                "Por favor, inténtelo de nuevo.");
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                    case 3://Menu entrenadores
                        Usuario usuarioEntrenador = 
                            gestorUsuarios.IniciarSesion();
                        if (usuarioEntrenador is Entrenador)
                        {
                            GestorMediciones gestorMediciones = 
                                new GestorMediciones
                                (gestorUsuarios, 
                                (Entrenador)usuarioEntrenador);
                            gestorMediciones.
                                CargarMedicionesDesdeArchivo();
                            Console.Clear();
                            Console.WriteLine
                                ("¡Inicio de sesión exitoso como entrenador!");
                            int entrenadorOpcion;
                            do
                            {
                                entrenadorOpcion = 
                                    InterfazUsuario.MostrarMenuEntrenador();
                                switch (entrenadorOpcion)
                                {
                                    case 1:
                                        gestorMaterial.
                                            AnyadirMaterial();
                                        break;
                                    case 2:
                                        gestorMaterial.
                                            MostrarMateriales();
                                        break;
                                    case 3:
                                        gestorMaterial.
                                            ModificarMaterial();
                                        break;
                                    case 4:
                                        gestorMaterial.
                                            EliminarMaterial();
                                        break;
                                    case 5:
                                        gestorMediciones.
                                            RealizarMedicion();
                                        break;
                                    case 6:
                                        gestorMediciones.
                                            MostrarMedicionesEntrenador();
                                        break;
                                    case 7:
                                        Console.WriteLine
                                            ("Volviendo al menú principal...");
                                        System.Threading.Thread.Sleep(1000);
                                        break;
                                }
                            }
                            while (entrenadorOpcion != 7);
                        }
                        else
                        {
                            Console.WriteLine
                                ("ID o contraseña incorrecta. Por favor, inténtelo de nuevo.");
                            System.Threading.Thread.Sleep(2000);
                        }
                        break;
                    case 4://Menu invitados
                        int invitadoOpcion;
                        do
                        {
                            invitadoOpcion = InterfazUsuario.MostrarMenuInvitado();
                            switch (invitadoOpcion)
                            {
                                case 1:
                                    gestorReservas.
                          ReservaComoInvitado(gestorInvitaciones, gestorActividades);
                                    Console.ReadKey();

                                    break;
                                case 2:
                                    Console.WriteLine("Volviendo al menú principal...");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                                default:
                                    Console.WriteLine("Intentalo de nuevo");
                                    System.Threading.Thread.Sleep(1000);
                                    break;
                            }
                           
                        }
                        while (invitadoOpcion != 2);
                       
                        break;
                    case 5: // Salir
                        Console.WriteLine("¡Gracias por usar FitConnect!");
                        return;
                    default:
                        Console.WriteLine("Opción no válida." +
                            " Por favor, inténtelo de nuevo.");
                        System.Threading.Thread.Sleep(2000);
                        break;
                }
            }
        }
    }
}
