using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenta.API.Utilidad;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class MedicamentoController : ControllerBase
    {
        private readonly IMedicamentoService _medicamentoService;

        public MedicamentoController(IMedicamentoService medicamentoService)
        {
            _medicamentoService = medicamentoService;
        }

        //devolvemos la lista de los medicamentos
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<MedicamentoDTO>>();

            try
            {
                rsp.status = true;
                rsp.value = await _medicamentoService.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;

            }
            return Ok(rsp);
        }

        [HttpGet("ProductosStockBajo")]
        public async Task<IActionResult> ObtenerProductosConStockBajo([FromQuery] int stockMinimo = 10)
        {
            try
            {
                var productosBajoStock = await _medicamentoService.ObtenerProductosConStockBajo(stockMinimo);
                return Ok(productosBajoStock);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }

        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] MedicamentoDTO producto)// El objeto MedicamentoDTO se recibe en el cuerpo de la solicitud.
        {

            var rsp = new Response<MedicamentoDTO>();

            try
            {
                rsp.status = true;
                rsp.value = await _medicamentoService.Crear(producto);//llama al servicio para crear un nuevo medicamento y devuelve el resultado.

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
        public async Task<IActionResult> Editar([FromBody] MedicamentoDTO producto)// El objeto MedicamentoDTO se recibe en el cuerpo de la solicitud.
        {

            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _medicamentoService.Editar(producto);//Llama al servicio para actualizar la información de un medicamento existente.

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
                rsp.value = await _medicamentoService.Eliminar(id);//Llama al servicio para eliminar un medicamento por su ID

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
