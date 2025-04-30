using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IMedicamentoService
    {
        Task<List<MedicamentoDTO>> Lista();
        Task<MedicamentoDTO> Crear(MedicamentoDTO modelo);
        Task<bool> Editar(MedicamentoDTO modelo);
        Task<bool> Eliminar(int id);
        Task<List<MedicamentoDTO>> ObtenerProductosConStockBajo(int stockMinimo);
    }
}
