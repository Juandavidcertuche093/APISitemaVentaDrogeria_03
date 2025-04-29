using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DTO;

namespace SistemaVenta.BLL.Servicios.Contrato
{
    public interface IImagenMedicamentoService
    {
        Task<List<ImagenMedicamentoDTO>> Lista();
        Task<ImagenMedicamentoDTO> Crear(ImagenMedicamentoDTO modelo);
        Task<bool> Editar(ImagenMedicamentoDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
