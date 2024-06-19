using System;
public class Entrenador : Usuario
{
    private string especialidad;

    public Entrenador(string nombre, string apellido, string fechaNacimiento, 
        string id, string contraseña, string especialidad)
        : base(nombre, apellido, fechaNacimiento, id, contraseña)
    {
        this.especialidad = especialidad;
    }
    public string ObtenerEspecialidad()
    {
        return especialidad;
    }
    public void EstablecerEspecialidad(string especialidad)
    {
        this.especialidad = especialidad;
    }
    public override string ToString()
    {
        return "Entrenador: " + ObtenerNombre() + " " + ObtenerApellido() 
            + " (ID: " + ObtenerId() + ", Especialidad: " 
            + ObtenerEspecialidad() + ")";
    }
}

