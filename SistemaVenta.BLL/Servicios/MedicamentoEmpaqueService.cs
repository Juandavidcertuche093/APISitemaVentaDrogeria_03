using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.BLL.Servicios.Contrato;
using SistemaVenta.DAL.Repositorio.Contrato;
using SistemaVenta.DTO;
using SistemaVenta.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.BLL.Servicios
{
    public class MedicamentoEmpaqueService : IMedicamentoEmpaqueService
    {
        private readonly IGenericRepository<MedicamentoEmpaque> _medicamentoEmpaqueRepository;
        private readonly IMapper _mapper;

        public MedicamentoEmpaqueService(IGenericRepository<MedicamentoEmpaque> medicamentoproEmpaqueRepository, IMapper mapper)
        {
            _medicamentoEmpaqueRepository = medicamentoproEmpaqueRepository;
            _mapper = mapper;
        }

        public async Task<List<MedicamentoEmpaqueDTO>> Lista()
        {
            try
            {
                var queryMedicamentoEmpaque = await _medicamentoEmpaqueRepository.Consultar();

                var listaMedicamentoEmpaque = queryMedicamentoEmpaque
                    .Include(pro => pro.IdMedicamentoNavigation)
                    .Include(pro => pro.IdPresentacionNavigation)
                    .ToList();
                return _mapper.Map<List<MedicamentoEmpaqueDTO>>(listaMedicamentoEmpaque.ToList());

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<MedicamentoEmpaqueDTO> Crear(MedicamentoEmpaqueDTO modelo)
        {
            try
            {
                var MedicamentoEntidad = _mapper.Map<MedicamentoEmpaque>(modelo);
                var medicamentoEmpaqueCreado = await _medicamentoEmpaqueRepository.Crear(MedicamentoEntidad);

                if (medicamentoEmpaqueCreado.IdMedicamento == 0)
                {
                    throw new TaskCanceledException("No se puede crear el medicamentoEmpaque");
                }

                var consulta = await _medicamentoEmpaqueRepository.Consultar();
                var medicamentoConRelaciones = consulta
                    .Where(p => p.IdMedicamento == medicamentoEmpaqueCreado.IdMedicamento)
                    .Include(p => p.IdPresentacionNavigation)
                    .FirstOrDefault();// No se usa async porque ya cargamos la consulta en memoria
                return _mapper.Map<MedicamentoEmpaqueDTO>(medicamentoConRelaciones);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(MedicamentoEmpaqueDTO modelo)
        {
            try
            {
                var medicamentoEmpaqueModelo = _mapper.Map<MedicamentoEmpaque>(modelo);
                var medicamentoEnpaqueEcontrado = await _medicamentoEmpaqueRepository.Obtener(u => u.IdMedicamentoEmpaque == medicamentoEmpaqueModelo.IdMedicamentoEmpaque);

                if (medicamentoEnpaqueEcontrado == null)
                    throw new TaskCanceledException("El medicamentoEmpaque no exite");
                medicamentoEnpaqueEcontrado.IdMedicamento = medicamentoEmpaqueModelo.IdMedicamento;
                medicamentoEnpaqueEcontrado.IdPresentacion = medicamentoEmpaqueModelo.IdPresentacion;
                medicamentoEnpaqueEcontrado.Cantidad = medicamentoEmpaqueModelo.Cantidad;
                medicamentoEnpaqueEcontrado.PrecioVenta = medicamentoEmpaqueModelo.PrecioVenta;
                medicamentoEnpaqueEcontrado.PrecioCompra = medicamentoEmpaqueModelo.PrecioCompra;
                medicamentoEnpaqueEcontrado.EsActivo = medicamentoEmpaqueModelo.EsActivo;

                var medicamentoEnpaqueActualizado = await _medicamentoEmpaqueRepository.Editar(medicamentoEnpaqueEcontrado);

                return medicamentoEnpaqueActualizado != null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var medicamentoEnpaqueEcontrado = await _medicamentoEmpaqueRepository.Obtener(p => p.IdMedicamentoEmpaque == id);
                if (medicamentoEnpaqueEcontrado == null)
                    throw new TaskCanceledException("El medicamentoEmpaque no exite");
                var medicamentoEmpaqueEliminado = await _medicamentoEmpaqueRepository.Eliminar(medicamentoEnpaqueEcontrado);
                return medicamentoEmpaqueEliminado != null;
            }
            catch (Exception)
            {

                throw;
            }
        }       
    }
}
