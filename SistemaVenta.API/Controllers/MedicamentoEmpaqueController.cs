using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.API.Utilidad;
using SistemaVenta.DTO;

namespace SistemaVenta.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MedicamentoEmpaqueController : ControllerBase
    {
        private readonly IMedicamentoEmpaqueService _medicamentoEmpaqueService;

        public MedicamentoEmpaqueController(IMedicamentoEmpaqueService medicamentoEmpaqueService)
        {
            _medicamentoEmpaqueService = medicamentoEmpaqueService;
        }

        //devolvemos la lista de los roles
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<MedicamentoEmpaqueDTO>>();//lo que nos duelve una lista de RolDTO

            try
            {
                rsp.status = true;
                rsp.value = await _medicamentoEmpaqueService.Lista();
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
        public async Task<IActionResult> Guardar([FromBody] MedicamentoEmpaqueDTO modelo)
        {
            var rsp = new Response<MedicamentoEmpaqueDTO>();

            try
            {
                if (modelo == null)
                {
                    rsp.status = false;
                    rsp.msg = "El modelo no puede ser nulo.";
                    return BadRequest(rsp);
                }

                rsp.status = true;
                rsp.value = await _medicamentoEmpaqueService.Crear(modelo);
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
        public async Task<IActionResult> Editar([FromBody] MedicamentoEmpaqueDTO producto)// El objeto MedicamentoDTO se recibe en el cuerpo de la solicitud.
        {

            var rsp = new Response<bool>();

            try
            {
                rsp.status = true;
                rsp.value = await _medicamentoEmpaqueService.Editar(producto);//Llama al servicio para actualizar la información de un medicamento existente.

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
                rsp.value = await _medicamentoEmpaqueService.Eliminar(id);//Llama al servicio para eliminar un medicamento por su ID

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
