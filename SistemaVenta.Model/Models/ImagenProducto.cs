using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class ImagenProducto
{
    public int IdImagen { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string Ruta { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Medicamento> Medicamentos { get; set; } = new List<Medicamento>();
}
