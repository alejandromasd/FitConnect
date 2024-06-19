using System;
public class Administrador : Usuario
{
    public Administrador(string nombre, string apellido, 
        string fechaNacimiento, string id, string contraseña)
        : base(nombre, apellido, fechaNacimiento, id, contraseña)
    {
    }


    public void AgregarSocio(GestorUsuarios gestorUsuarios, Socio socio)
    {
        gestorUsuarios.AgregarUsuario(socio);
    }


    public void EliminarSocio(GestorUsuarios gestorUsuarios, Socio socio)
    {
        gestorUsuarios.EliminarUsuario(socio);
    }

    public override string ToString()
    {
        return "Administrador: " + ObtenerNombre() + " " + 
            ObtenerApellido() + " (ID: " + ObtenerId() + ")";
    }
}

