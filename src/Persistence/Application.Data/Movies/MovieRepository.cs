using System;
using System.Collections.Generic;
using System.Linq;
using Application.Domain.Data;
using Application.Domain.Movies;
using Mapster;
using Microsoft.Extensions.Configuration;

namespace Application.Data.Movies
{
    // [DATA]: Read only repository for querying the database. Only concerned with "Movies".
    public class MovieRepository : IRepository<IMovie>
    {
        private readonly IList<Data.Movies.Movie> FakeMovieDatabase = new List<Data.Movies.Movie>()
        {
            new Data.Movies.Movie{ Id = 1, Title = "The Godfather", Description = "", Genre = "Drama", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 2, Title = "The Empire Strikes Back", Description = "", Genre = "SciFi", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 3, Title = "The Dark Knight", Description = "", Genre = "Action", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 4, Title = "The Shawshank Redemption", Description = "", Genre = "Drama", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 5, Title = "Pulp Fiction", Description = "", Genre = "Action", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 6, Title = "Goodfellas", Description = "", Genre = "Drama", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 7, Title = "Raiders Of The Lost Ark", Description = "", Genre = "Action", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 8, Title = "Jaws", Description = "", Genre = "Drama", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 9, Title = "Star Wars", Description = "", Genre = "SciFi", Created = new DateTime(2021, 07, 10) },
            new Data.Movies.Movie{ Id = 10, Title = "The Lord Of The Rings: The Fellowship Of The Ring", Description = "", Genre = "Fantasy", Created = new DateTime(2021, 07, 10) },
        };
        
        private readonly string _connectionString;
        
        // [DATA]: Pass in configuration so we can retrieve a connection string
        public MovieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
            
            TypeAdapterConfig<Data.Movies.Movie, Domain.Movies.Movie>.NewConfig();
        }
        
        public IMovie FindById(int id)
        {
            var entity = FakeMovieDatabase.FirstOrDefault(x => x.Id == id);
            if (entity == null) return null;

            return entity.Adapt<Domain.Movies.Movie>();
        }

        public ICollection<IMovie> FindAll()
        {
            var entities = FakeMovieDatabase.ToList();
            if (entities.Count == 0) return new List<IMovie>();

            return entities.Adapt<List<Domain.Movies.Movie>>().Cast<IMovie>().ToList();
        }
    }
}