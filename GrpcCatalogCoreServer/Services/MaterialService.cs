using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class MaterialService : MaterialesServ.MaterialesServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public MaterialService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<MaterialesReply> getMateriales(EmptyMaterialRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<Materiales>().FromSqlRaw("EXEC sp_GetMateriales").ToListAsync();

                MaterialesReply reply = new MaterialesReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;
            }
            catch (Exception ex)
            {
                return new MaterialesReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<MaterialReply> getMaterial(MaterialRequest request, ServerCallContext context)
        {
            try
            {
                var dbRes = await _dbcontext.Set<Materiales>().
                    FromSqlRaw("EXEC sp_GetMaterial {0}", request.MaterialId).ToListAsync();

                var r = dbRes.Find(m => m.MaterialId == request.MaterialId);

                if(r != null)
                {
                    return new MaterialReply() { Resultado = true, Registro = r};
                }

                return new MaterialReply() { Resultado = false };

            }
            catch (Exception ex)
            {
                return new MaterialReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<MaterialReply> insertMaterial(MaterialRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.
                    ExecuteSqlRawAsync("EXEC sp_InsertMaterial {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                        request.Registro.Titulo,
                        request.Registro.CategoriaId,
                        request.Registro.ProveedorId,
                        request.Registro.Editorial,
                        request.Registro.ISBN,
                        request.Registro.AnoPublicacion,
                        request.Registro.Edicion,
                        request.Registro.Descripcion,
                        request.Registro.UbicacionFisica,
                        request.Registro.Estado
                    );

                if(r > 0)
                {
                    return new MaterialReply() { Resultado = true };
                }

                return new MaterialReply() { Resultado = false };

            }
            catch (Exception ex)
            {
                return new MaterialReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<MaterialReply> updateMaterial(MaterialRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.
                    ExecuteSqlRawAsync("EXEC sp_UpdateMaterial {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}",
                        request.Registro.MaterialId,
                        request.Registro.Titulo,
                        request.Registro.CategoriaId,
                        request.Registro.ProveedorId,
                        request.Registro.Editorial,
                        request.Registro.ISBN,
                        request.Registro.AnoPublicacion,
                        request.Registro.Edicion,
                        request.Registro.Descripcion,
                        request.Registro.UbicacionFisica,
                        request.Registro.Estado
                    );

                if (r > 0)
                {
                    return new MaterialReply() { Resultado = true };
                }

                return new MaterialReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new MaterialReply() { Resultado = false, Message = ex.Message };
            }
        }

        public override async Task<MaterialReply> deleteMaterial(MaterialRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Database.
                    ExecuteSqlRawAsync("EXEC sp_DeleteMaterial {0}", request.MaterialId);

                if (r > 0)
                {
                    return new MaterialReply() { Resultado = true };
                }

                return new MaterialReply() { Resultado = false };
            }
            catch (Exception ex)
            {
                return new MaterialReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
