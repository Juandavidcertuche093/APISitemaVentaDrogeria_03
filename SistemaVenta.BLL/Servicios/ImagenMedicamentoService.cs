using AutoMapper;
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
    public class ImagenMedicamentoService : IImagenMedicamentoService
    {
        private readonly IGenericRepository<ImagenProducto> _imagenMedicamenroRepository;
        private readonly IMapper _mapper;

        public ImagenMedicamentoService(IGenericRepository<ImagenProducto> imagenMedicamenroRepository, IMapper mapper)
        {
            _imagenMedicamenroRepository = imagenMedicamenroRepository;
            _mapper = mapper;
        }

        public async Task<List<ImagenMedicamentoDTO>> Lista()
        {
            try
            {
                var listaImagenProducto = await _imagenMedicamenroRepository.Consultar();
                return _mapper.Map<List<ImagenMedicamentoDTO>>(listaImagenProducto.ToList());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ImagenMedicamentoDTO> Crear(ImagenMedicamentoDTO modelo)
        {
            try
            {
                var imagenProductoCreado = await _imagenMedicamenroRepository.Crear(_mapper.Map<ImagenProducto>(modelo));//mapeo inverso convierte el DTO a categoria para almacenar
                if (imagenProductoCreado.IdImagen == 0)
                    throw new TaskCanceledException("No se pudo crear la imagen");
                return _mapper.Map<ImagenMedicamentoDTO>(imagenProductoCreado);//devuelve el objeto creado en formato CategoriaDTO
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(ImagenMedicamentoDTO modelo)
        {
            try
            {
                var imagenModelo = _mapper.Map<ImagenProducto>(modelo);
                var imagenEncontrado = await _imagenMedicamenroRepository.Obtener(u => u.IdImagen == imagenModelo.IdImagen);

                if (imagenEncontrado == null)
                    throw new TaskCanceledException("imagen no encontrado");
                imagenEncontrado.NombreArchivo = imagenModelo.NombreArchivo;
                imagenEncontrado.Ruta = imagenModelo.Ruta;

                var imagenActualizado = await _imagenMedicamenroRepository.Editar(imagenEncontrado);

                return imagenActualizado != null;
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
                var imagenEncontrado = await _imagenMedicamenroRepository.Obtener(u => u.IdImagen == id);
                if (imagenEncontrado == null)
                    throw new TaskCanceledException("imagen no encontrado");
                var imagenEliminado = await _imagenMedicamenroRepository.Eliminar(imagenEncontrado);
                return imagenEliminado != null;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
