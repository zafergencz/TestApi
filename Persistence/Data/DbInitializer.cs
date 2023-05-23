using Microsoft.EntityFrameworkCore;
using Serilog;
namespace TestApi.Persistence.Data
{

    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public  void Initialize()
        {
           try{
                //await Task.Run(() =>
                //{
                    _context.Database.Migrate();
               // });
                //_context.Database.EnsureDeleted(); // Drop the existing database
                //_context.Database.EnsureCreated(); // Create a new database with the latest schema
            }catch(Exception ex){
                Log.Error(ex.Message);
                return;
            }           
            
            // You can also seed initial data if needed
        }
    }

    public interface IDbInitializer
    {        
        void Initialize();
    }
}
