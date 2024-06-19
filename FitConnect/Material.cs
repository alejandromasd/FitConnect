using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Material
{
    public string TipoMaterial { get; set; }
    public string Actividad { get; set; }
    public int UnidadesDisponibles { get; set; }
    public string Estado { get; set; }

    public Material(string tipoMaterial, string actividad, 
        int unidadesDisponibles, string estado)
    {
        TipoMaterial = tipoMaterial;
        Actividad = actividad;
        UnidadesDisponibles = unidadesDisponibles;
        Estado = estado;
    }
}

