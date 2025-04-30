using AutoMapper;
using Microsoft.AspNetCore.SignalR;
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
    public class MedicamentoService : IMedicamentoService
    {
        private readonly IGenericRepository<Medicamento> _medicamentoRepositorio;
        private readonly IMapper _mapper;        
        private const int StockMinimo = 10; // Definir el umbral como constante


        public MedicamentoService(IGenericRepository<Medicamento> productoRepositorio, IMapper mapper)
        {
            _medicamentoRepositorio = productoRepositorio;
            _mapper = mapper;

        }

        // Método para obtener lista de medicamentos con stock bajo
        public async Task<List<MedicamentoDTO>> ObtenerProductosConStockBajo(int stockMinimo)
        {
            try
            {
                var producto = await _medicamentoRepositorio.Consultar();
                var productosConStockBajo = producto
                    .Where(m => m.Stock <= stockMinimo)
                    .Include(cat => cat.IdCategoriaNavigation)
                    .ToList();

                return _mapper.Map<List<MedicamentoDTO>>(productosConStockBajo);
            }
            catch
            {
                throw;
            }
        }


        public async Task<List<MedicamentoDTO>> Lista()
        {
            try
            {
                var queryMedicamento = await _medicamentoRepositorio.Consultar();

                var listaMedicamento = queryMedicamento
                    .Include(cat => cat.IdCategoriaNavigation)//incluimos la relacion con categoria del producto
                    .Include(prov => prov.IdProveedorNavigation)//incluimos la relacion con proveedor del producto
                    .Include(imag => imag.IdImagenNavigation)//incluimos la relacion con imagen del producto
                    .ToList();
                return _mapper.Map<List<MedicamentoDTO>>(listaMedicamento.ToList());
            }
            catch
            {
                throw;
            }
        }


        public async Task<MedicamentoDTO> Crear(MedicamentoDTO modelo)
        {
            try
            {
                // Verificar si ya existe un producto con el mismo nombre
                var existeProducto = await _medicamentoRepositorio.Obtener(m => m.Nombre.ToLower() == modelo.Nombre.ToLower());
                if (existeProducto != null)
                {
                    throw new Exception("Ya existe un medicamento con este nombre.");
                }

                var productoEntidad = _mapper.Map<Medicamento>(modelo);
                var productoCreado = await _medicamentoRepositorio.Crear(productoEntidad);

                if (productoCreado.IdMedicamento == 0)
                {
                    throw new TaskCanceledException("No se pudo crear el Medicamento.");
                }

                // ✅ CORRECCIÓN: Usar 'await' para obtener la consulta antes de aplicar filtros
                var consulta = await _medicamentoRepositorio.Consultar();
                var productoConRelaciones = consulta
                    .Where(p => p.IdMedicamento == productoCreado.IdMedicamento)
                    .Include(p => p.IdCategoriaNavigation)
                    .Include(p => p.IdProveedorNavigation)
                    .Include(p => p.IdImagenNavigation)
                    .FirstOrDefault(); // No se usa async porque ya cargamos la consulta en memoria

                return _mapper.Map<MedicamentoDTO>(productoConRelaciones);
            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Editar(MedicamentoDTO modelo)
        {
            try
            {
                var productoModelo = _mapper.Map<Medicamento>(modelo);//mapeo inverso convierte el DTO a medicamento para almacenar
                var productoEncotrado = await _medicamentoRepositorio.Obtener(u => u.IdMedicamento == productoModelo.IdMedicamento);//buscamos el medicamento con su idmedicamento

                if (productoEncotrado == null)//si el medicamento no se encuentra en la base de datos se lanza una acepcion 
                    throw new TaskCanceledException("El medicamento no existe");
                productoEncotrado.Nombre = productoModelo.Nombre;
                productoEncotrado.IdCategoria = productoModelo.IdCategoria;
                productoEncotrado.IdImagen = productoModelo.IdImagen;
                productoEncotrado.Stock = productoModelo.Stock;
                productoEncotrado.FechaVencimiento = productoModelo.FechaVencimiento;
                productoEncotrado.EsActivo = productoModelo.EsActivo;


                //Aquí se llama al método Editar del repositorio para persistir los cambios en la base de datos
                var productoActualizado = await _medicamentoRepositorio.Editar(productoEncotrado);

                if (productoEncotrado.Stock <= 5)
                {
                    var productoDTO = _mapper.Map<MedicamentoDTO>(productoEncotrado);
                }

                // Verificar si el objeto devuelto no es nulo, lo que indica que la actualización fue exitosa (true)
                return productoActualizado != null;

            }
            catch
            {
                throw;
            }
        }


        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var productoEncotrado = await _medicamentoRepositorio.Obtener(p => p.IdMedicamento == id);//buscamos el medicamto por su idmedicamento
                if (productoEncotrado == null)
                    throw new TaskCanceledException("El medicamento no existe");
                var productoEliminado = await _medicamentoRepositorio.Eliminar(productoEncotrado);//Si lo encuentra, lo elimina usando el repositorio.
                return productoEliminado != null;//Devuelve true si la eliminación fue exitosa.
            }
            catch
            {
                throw;
            }
        }


    }
}
