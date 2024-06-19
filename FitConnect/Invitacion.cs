using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Invitacion
{
    private string codigo;
    private bool utilizado;
    private string idSocio;

    public Invitacion(string codigo, string idSocio) 
    {
        this.codigo = codigo;
        this.utilizado = false;
        this.idSocio = idSocio; 
    }

    public string ObtenerIdSocio()
    {
        return idSocio;
    }

    public string ObtenerCodigo()
    {
        return codigo;
    }

    public bool EstaUtilizado()
    {
        return utilizado;
    }

    public void MarcarComoUtilizado()
    {
        this.utilizado = true;
    }
}
