using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CorpExample1.Models
{
    public class AgentContext : DbContext
    {
        public AgentContext(DbContextOptions<AgentContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Agent>().HasKey(a => a._Id);
            mb.Entity<AgentDetail>().HasKey(a => a._Id);
            mb.Entity<Customer>().HasKey(c => c.Guid);
            mb.Entity<Name>().HasKey(n => n.Guid);
            mb.Entity<Phone>().HasKey(p => p._Id);

            mb.Entity<AgentDetail>()
                .HasOne(a => a.Phone)
                .WithOne()
                .HasForeignKey<Phone>(p => p._Id);

            mb.Entity<CustomerDetail>()
                .HasOne(cd => cd.Name)
                .WithOne()
                .HasForeignKey<Name>(n => n.Guid);

            mb.Entity<CustomerDetail>()
                .Property(p => p.Tags)
                .HasConversion(
                    t => string.Join(',', t),
                    t => t.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.EnableSensitiveDataLogging();      //debug
        }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<AgentDetail> AgentDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetail> CustomerDetails { get; set; }

    }
}
