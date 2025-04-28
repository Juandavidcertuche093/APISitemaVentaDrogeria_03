﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DTO
{
    public class DetalleCompraDTO
    {
        public int? IdMedicamentoEmpaque { get; set; }
        public string? DescripcionMedicamentoEmpaque { get; set; }//Este campo se usa para mostrar el nombre del producto
        public int? Cantidad { get; set; }
        public string? PrecioTexto { get; set; }//lo cambiamos de decimal a string
        public string? TotalTexto { get; set; }//lo cambiamos de decimal a string
    }
}
