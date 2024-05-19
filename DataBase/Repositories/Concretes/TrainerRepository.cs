using DataBase.Contexts;
using DataBase.Entities.Concretes;
using DataBase.Repositories.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repositories.Concretes
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        public TrainerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
