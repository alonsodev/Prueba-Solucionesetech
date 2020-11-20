using Solucionesetech.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solucionesetech.DataAccess.Repositories
{
    public class Repository
    {
        private readonly DbContextOptions<TRAVELSContext> _options;

        public Repository(DbContextOptions<TRAVELSContext> options)
        {
            _options = options;
        }

        public TRAVELSContext GenerateNewContext()
        {
           
            return new TRAVELSContext(this._options);
        }
    }
}
