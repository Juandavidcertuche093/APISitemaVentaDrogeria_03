using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class MedicamentoEmpaqueDTO
    {
        public int IdMedicamentoEmpaque { get; set; }
        public int IdMedicamento { get; set; }
        public string? DescripcionMedicamento { get; set; } //específicamente para mostrar la descripción o nombre del producto
        public int IdPresentacion { get; set; }
        public string? DescripcionPresentacion { get; set; } //específicamente para mostrar la descripción o nombre del presentación
        public int? Cantidad { get; set; }
        public string? PrecioVenta { get; set; }//lo cambiamos de decimal a string
        public string? PrecioCompra { get; set; }//lo cambiamos de decimal a string
        public int? EsActivo { get; set; }//lo cambiamos de boolen a entero 

    }
}
