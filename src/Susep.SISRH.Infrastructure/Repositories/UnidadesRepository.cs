using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.UnidadesAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class UnidadesRepository : SqlServerRepository<UnidadeDB>, IUnidadesRepository
    {
        private readonly SISRHDbContext _context;

        public UnidadesRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UnidadeDB> ObterAsync(Int64 unidadeId)
        {
            var item = await Entity.FindAsync(unidadeId);
            return item;
        }

        public async Task<UnidadeDB> AdicionarAsync(UnidadeDB item)
        {
            var result = await Entity.AddAsync(item);
            return result.Entity;
        }

        public void Atualizar(UnidadeDB item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Excluir(UnidadeDB item)
        {
            _context.Entry(item).State = EntityState.Deleted;
        }

        public async Task ExcluirAsync(Int64 id)
        {
            var item = await Entity.FindAsync(id);
            _context.Entry(item).State = EntityState.Deleted;
        }
    }
}
