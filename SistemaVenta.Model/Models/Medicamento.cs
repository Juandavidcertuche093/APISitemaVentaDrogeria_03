using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class Medicamento
{
    public int IdMedicamento { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdCategoria { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdImagen { get; set; }

    public int Stock { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public ulong? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual ImagenProducto? IdImagenNavigation { get; set; }

    public virtual Proveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<MedicamentoEmpaque> MedicamentoEmpaques { get; set; } = new List<MedicamentoEmpaque>();
}
