using System;
using System.Collections.Generic;

namespace SistemaVenta.Model.Models;

public partial class MedicamentoEmpaque
{
    public int IdMedicamentoEmpaque { get; set; }

    public int IdMedicamento { get; set; }

    public int IdPresentacion { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioVenta { get; set; }

    public decimal? PrecioCompra { get; set; }

    public ulong? EsActivo { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Detallecompra> Detallecompras { get; set; } = new List<Detallecompra>();

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Medicamento IdMedicamentoNavigation { get; set; } = null!;

    public virtual Presentacion IdPresentacionNavigation { get; set; } = null!;
}
