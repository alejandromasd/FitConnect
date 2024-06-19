using OfficeOpenXml.ExternalReferences;
using System;

public class Socio : Usuario
{
    private string tipoMembresia;
    private string fechaMembresia;
    private int contadorInvitaciones;
    private bool cuotaPagada;
    private int puntos;
    private bool estadoPago;
    public List<Medicion> Mediciones { get; set; }

    public Socio(string nombre, string apellido, string fechaNacimiento,
        string id, string contraseña, string tipoMembresia,
        string fechaMembresia, bool cuotaPagada)
        : base(nombre, apellido, fechaNacimiento,
            id, contraseña)
    {
        this.tipoMembresia = tipoMembresia;
        this.fechaMembresia = fechaMembresia;
        this.cuotaPagada = cuotaPagada;
        this.Mediciones = new List<Medicion>();
    }

    public Socio(string nombre, string apellido, string fechaNacimiento,
        string id, string contraseña, string tipoMembresia,
        string fechaMembresia, bool cuotaPagada, int contadorInvitaciones)
        : this(nombre, apellido, fechaNacimiento, id, contraseña,
              tipoMembresia, fechaMembresia, cuotaPagada)
    {
        this.contadorInvitaciones = contadorInvitaciones;
    }
    public bool EstadoPago
    {
        get { return estadoPago; }
        set { estadoPago = value; }
    }

    public int Puntos
    {
        get { return puntos; }
        set { puntos = value; }
    }
    public void PagarCuota()
    {
        cuotaPagada = true;
        puntos += 5;
    }

    public string ObtenerTipoMembresia()
    {
        return tipoMembresia;
    }

    public bool EstaCuotaPagada()
    {
        return cuotaPagada;

    }
    public void ObtenerCuotaPagada()
    {
        cuotaPagada = true;
    }

    public void EstablecerTipoMembresia(string tipoMembresia)
    {
        this.tipoMembresia = tipoMembresia;
    }

    public string ObtenerFechaMembresia()
    {
        return fechaMembresia;
    }

    public void EstablecerFechaMembresia(string fechaMembresia)
    {
        this.fechaMembresia = fechaMembresia;
    }

    public int ObtenerContadorInvitaciones()
    {
        return this.contadorInvitaciones;
    }

    public void DecrementarContadorInvitaciones()
    {
        if (this.contadorInvitaciones > 0)
        {
            this.contadorInvitaciones--;
        }
    }
   

    public override string ToString()
    {
        return "Socio: " + ObtenerNombre() 
            + " " + ObtenerApellido() + " (ID: " 
            + ObtenerId() + ", Tipo de membresía: " 
            + ObtenerTipoMembresia() + ", Fecha de membresía: " 
            + ObtenerFechaMembresia() + ")";
    }
    public override bool Equals(object obj)
    {
        if (obj is Socio)
        {
            Socio otroSocio = obj as Socio;
            return this.ObtenerId() == otroSocio.ObtenerId();
        }
        return false;
    }


}

