using Microsoft.EntityFrameworkCore;
using Susep.SISRH.Domain.AggregatesModel.UnidadeAggregate;
using Susep.SISRH.Infrastructure.Contexts;
using SUSEP.Framework.Data.Concrete.Repositories;
using System;
using System.Threading.Tasks;

namespace Susep.SISRH.Infrastructure.Repositories
{
    public class UnidadeRepository : SqlServerRepository<Unidade>, IUnidadeRepository
    {
        private readonly SISRHDbContext _context;

        public UnidadeRepository(SISRHDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Unidade> ObterAsync(Guid unidadeId)
        {
            var item = await Entity.FindAsync(unidadeId);
            //if (item != null)
            //{
            //    await _context.Entry(item)
            //        .Reference<Unidade>(i => i.UnidadeIdPai).LoadAsync();
            //}
            return item;
        }

        //public async Task<Unidades> AdicionarAsync(Unidades item)
        //{
        //    var result = await Entity.AddAsync(item);
        //    return result.Entity;
        //}

        //public void Atualizar(Unidades item)
        //{
        //    _context.Entry(item).State = EntityState.Modified;
        //}

        //public void Excluir(Unidades item)
        //{
        //    _context.Entry(item).State = EntityState.Deleted;
        //}

        //public async Task ExcluirAsync(Int64 id)
        //{
        //    var item = await Entity.FindAsync(id);
        //    _context.Entry(item).State = EntityState.Deleted;
        //}
    }
}
