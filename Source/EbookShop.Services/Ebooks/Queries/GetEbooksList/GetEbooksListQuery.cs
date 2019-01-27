using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using EbookShop.Models;
using EbookShop.Services.Dtos;
using MediatR;
namespace EbookShop.Services.Ebooks.Queries.GetEbooksList
{
    public class GetEbooksListQuery : IRequest<List<EbookDto>>
    {
        public Expression<Func<Ebook, bool>> Filter { get; set; }
    }
}
