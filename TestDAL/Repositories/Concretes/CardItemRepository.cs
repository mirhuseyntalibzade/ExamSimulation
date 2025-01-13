using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCORE.Models;
using TestDAL.Contexts;
using TestDAL.Repositories.Abstractions;

namespace TestDAL.Repositories.Concretes
{
    public class CardItemRepository : GenericRepository<CardItem>, ICardItemRepository
    {
        public CardItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
