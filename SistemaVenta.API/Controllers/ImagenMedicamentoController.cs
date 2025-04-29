using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ImagenMedicamentoController : ControllerBase
    {
        private readonly IImagenMedicamentoService _imagenMedicamentoRepository;

        public ImagenMedicamentoController(IImagenMedicamentoService imagenMedicamentoRepository)
        {
            _imagenMedicamentoRepository = imagenMedicamentoRepository;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<ImagenMedicamentoDTO>>();//lo que nos duelve una lista de categoriaDTO

            try
            {
                rsp.status = true;
                rsp.value = await _imagenMedicamentoRepository.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] ImagenMedicamentoDTO categoria)// El objeto MedicamentoDTO se recibe en el cuerpo de la solicitud.
        {

            var rsp = new Response<ImagenMedicamentoDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _imagenMedicamentoRepository.Crear(categoria);//llama al servicio para crear un nuevo medicamento y devuelve el resultado.

            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);

        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] ImagenMedicamentoDTO modelo)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _imagenMedicamentoRepository.Editar(modelo);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _imagenMedicamentoRepository.Eliminar(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }
    }
}
