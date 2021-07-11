using Application.Domain.Data;
using Application.Domain.Movies;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Data.Movies
{
    // [DATA]: Responsible for Create, Update, Delete (CUD) operations. Only concerned with "Movies".
    public class MovieManager : IManager<IMovie>
    {
        private readonly string _connectionString;
        
        // [DATA]: Pass in configuration so we can retrieve a connection string
        public MovieManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }
        
        public int Create(IMovie model)
        {
            var entity = model.Adapt<Data.Movies.Movie>();

            using (var dbContext = new ApplicationDbContext(_connectionString))
            {
                dbContext.Movies.Add(entity);
                dbContext.SaveChanges();
                return entity.Id;
            }
        }

        public void Update(IMovie model)
        {
            var entity = model.Adapt<Data.Movies.Movie>();

            using (var dbContext = new ApplicationDbContext(_connectionString))
            {
                dbContext.Movies.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity = new Data.Movies.Movie { Id = id };

            using (var dbContext = new ApplicationDbContext(_connectionString))
            {
                dbContext.Movies.Attach(entity);
                dbContext.Movies.Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}