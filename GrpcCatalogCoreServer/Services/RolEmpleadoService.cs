using Grpc.Core;
using GrpcCatalogCoreServer.Models;
using GrpcCatalogCoreServer.Protos;
using Microsoft.EntityFrameworkCore;

namespace GrpcCatalogCoreServer.Services
{
    public class RolEmpleadoService : RolEmpleadoServ.RolEmpleadoServBase
    {
        private readonly CatalogCoreDbContext _dbcontext;
        private readonly ILogger<ProveedorService> _logger;

        public RolEmpleadoService(CatalogCoreDbContext dbcontext, ILogger<ProveedorService> logger)
        {
            _dbcontext = dbcontext;
            _logger = logger;
        }

        public override async Task<RolEmpleadosReply> getRolEmpleados(EmptyRolEmpleadoRequest request, ServerCallContext context)
        {
            try
            {
                var r = await _dbcontext.Set<RolEmpleados>().FromSqlRaw("EXEC sp_GetRolEmpleados").ToListAsync();

                RolEmpleadosReply reply = new RolEmpleadosReply();

                reply.Resultado = true;
                reply.Lista.AddRange(r);

                return reply;

            }
            catch (Exception ex)
            {
                return new RolEmpleadosReply() { Resultado = false, Message = ex.Message };
            }
        }

    }
}
