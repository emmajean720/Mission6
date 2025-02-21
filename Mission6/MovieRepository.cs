using Microsoft.Extensions.Configuration;
using Mission6.Models; // Ensure this using directive is present
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace Mission6.Data
{
    public class MovieRepository
    {
        private readonly string _connectionString;

        public MovieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FilmCollectionDB");
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = @"
                    SELECT MovieId, Title, Year, Director, Rating, Edited, LentTo, CopiedToPlex, Notes
                    FROM Movies;
                ";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            movies.Add(new Movie
                            {
                                MovieID = Convert.ToInt32(reader["MovieId"]),
                                Title = reader["Title"].ToString(),
                                Year = Convert.ToInt32(reader["Year"]),
                                Director = reader["Director"] != DBNull.Value ? reader["Director"].ToString() : null,
                                Rating = reader["Rating"] != DBNull.Value ? reader["Rating"].ToString() : null,
                                Edited = Convert.ToBoolean(reader["Edited"]),
                                LentTo = reader["LentTo"] != DBNull.Value ? reader["LentTo"].ToString() : null,
                                CopiedToPlex = Convert.ToBoolean(reader["CopiedToPlex"]),
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return movies;
        }
        //add rows
        public void AddMovie(Movie movie)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Movies (Title, Year, Director, Rating, Edited, LentTo, CopiedToPlex, Notes)
                    VALUES (@Title, @Year, @Director, @Rating, @Edited, @LentTo, @CopiedToPlex, @Notes);
                ";

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Year", movie.Year);
                command.Parameters.AddWithValue("@Director", movie.Director ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Rating", movie.Rating ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Edited", movie.Edited);
                command.Parameters.AddWithValue("@LentTo", movie.LentTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CopiedToPlex", movie.CopiedToPlex);
                command.Parameters.AddWithValue("@Notes", movie.Notes ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        //update rows
        public void UpdateMovie(Movie movie)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                    UPDATE Movies
                    SET Title = @Title, Year = @Year, Director = @Director, Rating = @Rating, 
                        Edited = @Edited, LentTo = @LentTo, CopiedToPlex = @CopiedToPlex, Notes = @Notes
                    WHERE MovieId = @MovieId;
                ";

                command.Parameters.AddWithValue("@MovieId", movie.MovieID);
                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Year", movie.Year);
                command.Parameters.AddWithValue("@Director", movie.Director ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Rating", movie.Rating ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Edited", movie.Edited);
                command.Parameters.AddWithValue("@LentTo", movie.LentTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CopiedToPlex", movie.CopiedToPlex);
                command.Parameters.AddWithValue("@Notes", movie.Notes ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        //deleting rows
        public void DeleteMovie(int movieId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Movies WHERE MovieId = @MovieId";
                command.Parameters.AddWithValue("@MovieId", movieId);

                command.ExecuteNonQuery();
            }
        }
    }
}