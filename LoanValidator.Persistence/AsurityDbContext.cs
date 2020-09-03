using Microsoft.EntityFrameworkCore;
using System;

namespace LoanValidator.Persistence
{
    public class AsurityDbContext : DbContext
    {
        public AsurityDbContext(DbContextOptions<AsurityDbContext> options) : base(options)
        {
        }
    }
}
