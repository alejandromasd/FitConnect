using System;


public abstract class Usuario
{
    private string nombre;
    private string apellido;
    private string fechaNacimiento;
    private string id;
    private string contraseña;
    

    public Usuario(string nombre, string apellido, string fechaNacimiento,
        string id, string contraseña)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.fechaNacimiento = fechaNacimiento;
        this.id = id;
        this.contraseña = contraseña;
        
    }
    public Usuario(string nombre, string id)
    {
        this.nombre = nombre;
        this.id = id;
    }

    

    public string ObtenerNombre()
    {
        return nombre;
    }

    public void EstablecerNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string ObtenerApellido()
    {
        return apellido;
    }

    public void EstablecerApellido(string apellido)
    {
        this.apellido = apellido;
    }

    public string ObtenerFechaNacimiento()
    {
        return fechaNacimiento;
    }

    public void EstablecerFechaNacimiento(string fechaNacimiento)
    {
        this.fechaNacimiento = fechaNacimiento;
    }

    public string ObtenerId()
    {
        return id;
    }

    public void EstablecerId(string id)
    {
        this.id = id;
    }

    public string ObtenerContraseña()
    {
        return contraseña;
    }

    public void EstablecerContraseña(string contraseña)
    {
        this.contraseña = contraseña;
    }
}


