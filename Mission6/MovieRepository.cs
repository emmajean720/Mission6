using Microsoft.Extensions.Configuration;
using Mission6.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace Mission6.Data
{
    public class MovieRepository
    {
        private readonly string _connectionString = "Data Source=FilmCollection.db;Version=3;";


        public void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Movies (
                    MovieId INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    Director TEXT NOT NULL,
                    Rating TEXT NOT NULL,
                    Edited BOOLEAN,
                    LentTo TEXT,
                    Notes TEXT
                );
            ";
                command.ExecuteNonQuery();
            }
        }

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Movies";

                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            movies.Add(new Movie
                            {
                                MovieID = Convert.ToInt32(reader["MovieID"]),
                                Title = reader["Title"].ToString(),
                                Category = reader["Category"].ToString(),
                                Director = reader["Director"].ToString(),
                                Rating = reader["Rating"].ToString(),
                                Edited = reader["Edited"] != DBNull.Value ? (bool?)Convert.ToBoolean(reader["Edited"]) : null,
                                LentTo = reader["LentTo"] != DBNull.Value ? reader["LentTo"].ToString() : null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return movies;
        }

        public void AddMovie(Movie movie)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                INSERT INTO Movies (Title, Category, Director, Rating, Edited, LentTo, Notes)
                VALUES (@Title, @Category, @Director, @Rating, @Edited, @LentTo, @Notes);
            ";

                command.Parameters.AddWithValue("@Title", movie.Title);
                command.Parameters.AddWithValue("@Category", movie.Category);
                command.Parameters.AddWithValue("@Director", movie.Director);
                command.Parameters.AddWithValue("@Rating", movie.Rating);
                command.Parameters.AddWithValue("@Edited", movie.Edited ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@LentTo", movie.LentTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Notes", movie.Notes ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
    }
}