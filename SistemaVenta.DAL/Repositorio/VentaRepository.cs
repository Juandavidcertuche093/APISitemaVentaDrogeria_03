using SistemaVenta.DAL.DBContext;
using SistemaVenta.DAL.Repositorio.Contrato;
using SistemaVenta.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVenta.DAL.Repositorio
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly MyDbContext _dbContext;

        public VentaRepository(MyDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerada = new Venta();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (Detalleventa dv in modelo.Detalleventa)
                    {
                        var medicamentoEmpaque = _dbContext.MedicamentoEmpaques
                            .FirstOrDefault(pe => pe.IdMedicamentoEmpaque == dv.IdMedicamentoEmpaque);

                        if (medicamentoEmpaque == null)
                        {
                            throw new Exception("Medicamento no encontrado.");
                        }

                        // Buscar el producto real en la tabla Producto
                        var medicamento = _dbContext.Medicamentos
                            .FirstOrDefault(p => p.IdMedicamento == medicamentoEmpaque.IdMedicamento);

                        if (medicamento == null)
                        {
                            throw new Exception("Medicamento en inventario no encontrado.");
                        }

                        // Obtener la cantidad de unidades por empaque desde la tabla Presentacion
                        int unidadesPorEmpaque = _dbContext.Presentacions
                            .Where(p => p.IdPresentacion == medicamentoEmpaque.IdPresentacion)
                            .Select(p => (int?)p.CantidadUnidades) // Convierte a nullable int
                            .FirstOrDefault() ?? 1; // Si es null, usa 1 como valor por defecto

                        // Calcular la cantidad real de unidades a descontar
                        int unidadesADescontar = (dv.Cantidad ?? 0) * unidadesPorEmpaque;

                        if (medicamento.Stock < unidadesADescontar)
                        {
                            throw new Exception("Stock insuficiente para la venta.");
                        }

                        // Descontar del stock real del producto
                        medicamento.Stock -= unidadesADescontar;
                        _dbContext.Medicamentos.Update(medicamento);
                    }

                    await _dbContext.SaveChangesAsync();

                    // Generar número de venta
                    Numeroventa correlativo = _dbContext.Numeroventa.First();
                    correlativo.UltimoNumero++;
                    correlativo.FechaRegistro = DateTime.Now;
                    _dbContext.Numeroventa.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    modelo.NumVenta = correlativo.UltimoNumero.ToString("D4");
                    await _dbContext.Venta.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();

                    ventaGenerada = modelo;
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return ventaGenerada;
        }
    }
}
