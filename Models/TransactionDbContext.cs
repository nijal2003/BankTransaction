using Microsoft.EntityFrameworkCore;

namespace BankTransations.Models
{
    public class TransactionDbContext:DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> option):base(option)
        {
            
        }

        public DbSet<Transation> Transcription { get; set; }
    }
}
