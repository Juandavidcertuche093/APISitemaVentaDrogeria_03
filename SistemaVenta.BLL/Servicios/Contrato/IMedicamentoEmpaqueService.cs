using SistemaVenta.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IMedicamentoEmpaqueService
    {
        Task<List<MedicamentoEmpaqueDTO>> Lista();
        Task<MedicamentoEmpaqueDTO> Crear(MedicamentoEmpaqueDTO modelo);
        Task<bool> Editar(MedicamentoEmpaqueDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
