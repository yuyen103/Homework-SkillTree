using Microsoft.EntityFrameworkCore;
using Homework_SkillTree.Models;
namespace Homework_SkillTree.Data
{
    public class HomeworkServiceContext : DbContext
    {
        public HomeworkServiceContext()
        {
        }

        public HomeworkServiceContext(DbContextOptions<HomeworkServiceContext> options)
            : base(options)
        {
        }
        public virtual DbSet<IncomeExpenseRecordModel> IncomeExpenseRecord { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IncomeExpenseRecordModel>(entity => { entity.Property(e => e.Id).ValueGeneratedNever(); });
        }
    }
}
