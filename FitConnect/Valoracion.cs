using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Valoracion
{
    private string comentario;
    private int nota;
    private Socio socio;

    public Valoracion(string comentario, int nota, Socio socio)
    {
        this.comentario = comentario;
        this.nota = nota;
        this.socio = socio;
    }
    public string ObtenerComentario()
    {
        return this.comentario;
    }
    public int ObtenerNota()
    {
        return this.nota;
    }
    public Socio ObtenerSocio()
    {
        return this.socio;
    }
}
