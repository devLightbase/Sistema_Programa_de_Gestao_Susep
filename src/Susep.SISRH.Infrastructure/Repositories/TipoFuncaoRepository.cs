using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.TipoFuncaoAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class TipoFuncaoRepository : SqlServerRepository<TipoFuncao>, ITipoFuncaoRepository
    {
        private readonly SISRHDbContext _context;

        public TipoFuncaoRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TipoFuncao> ObterAsync(Int64 tipoFuncaoId)
        {
            var item = await Entity.FindAsync(tipoFuncaoId);
            return item;
        }

        public async Task<TipoFuncao> AdicionarAsync(TipoFuncao item)
        {
            var result = await Entity.AddAsync(item);
            return result.Entity;
        }

        public void Atualizar(TipoFuncao item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Excluir(TipoFuncao item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task ExcluirAsync(Int64 tipoFuncaoId)
        {
            var item = await Entity.FindAsync(tipoFuncaoId);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}