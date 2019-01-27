using AutoMapper;
using EbookShop.DataAccess;
using EbookShop.Services.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using EbookShop.Models;

namespace EbookShop.Services.Ebooks.Queries.GetEbooksList
{
    public class GetEbooksListQueryHandler : IRequestHandler<GetEbooksListQuery, List<EbookDto>>
    {
        private readonly EbookShopContext context;
        private readonly IMapper mapper;

        public GetEbooksListQueryHandler(EbookShopContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public Task<List<EbookDto>> Handle(GetEbooksListQuery request, CancellationToken cancellationToken)
        {
            var ebooksQuery = (request.Filter != null) ? context.Ebooks.Where(request.Filter) :
                context.Ebooks.AsQueryable();

            return ebooksQuery
            .Include(e => e.EbookAuthors).ThenInclude(x => x.Author)
            .Include(e => e.EbookGenres).ThenInclude(x => x.Genre)
            .Include(e => e.Files)
            .AsNoTracking()
            .ProjectTo<EbookDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        }
    }
}
