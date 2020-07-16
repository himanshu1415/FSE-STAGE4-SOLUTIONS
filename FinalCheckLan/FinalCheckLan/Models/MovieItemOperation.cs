using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCheckLan.Models
{
    public class MovieItemOperation
    {
        private readonly IConfiguration config;
        private readonly string conr;

        public MovieItemOperation(IConfiguration config)
        {
            this.config = config;
            conr = config.GetConnectionString("constr");
        }
        public async Task<List<MovieItem>> GetMovieItems()
        {
            List<MovieItem> MovieItemsList = new List<MovieItem>();
            var conn = new SqlConnection(conr);
            var comm = new SqlCommand("select * from MovieItems", conn);
            conn.Open();
            var reader = await comm.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                MovieItem m;
                while (reader.Read())
                {
                    m = new MovieItem();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    m.DateOfLaunch = reader.GetDateTime(2);
                    m.Genre = reader.GetString(3);
                    MovieItemsList.Add(m);
                }
            }
            reader.Close();
            conn.Close();

            return MovieItemsList;
        }

        public MovieItem GetMovieItems(int id)
        {
            MovieItem m = null;
            var conn = new SqlConnection(conr);
            var comm = new SqlCommand("select * from MovieItems where Id=@id", conn);
            conn.Open();
            comm.Parameters.AddWithValue("@id", id);
            var reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                m = new MovieItem();
                reader.Read();

                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                m.DateOfLaunch = reader.GetDateTime(2);
                m.Genre = reader.GetString(3);

            }
            reader.Close();
            conn.Close();

            return m;
        }



        public int AddNew(MovieItem p)
        {
            var conn = new SqlConnection(conr);
            var comm = new SqlCommand("insert into MovieItems values(@p1,@p2,@p3)", conn);
            comm.Parameters.AddWithValue("@p1", p.Name);
            comm.Parameters.AddWithValue("@p2", p.DateOfLaunch);
            comm.Parameters.AddWithValue("@p3", p.Genre);
            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }

        public int Update(MovieItem m)
        {
            var conn = new SqlConnection(conr);
            var comm = new SqlCommand("update MovieItems set Name=@p1,DateOfLaunch=@p3,Category=@p4 where Id=@p2", conn);
            comm.Parameters.AddWithValue("@p1", m.Name);
            comm.Parameters.AddWithValue("@p2", m.Id);
            comm.Parameters.AddWithValue("@p3", m.DateOfLaunch);
            comm.Parameters.AddWithValue("@p4", m.Genre);
            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }

        public int Delete(int id)
        {
            var conn = new SqlConnection(conr);
            var comm = new SqlCommand("delete from MovieItems where Id=@p1", conn);
            comm.Parameters.AddWithValue("@p1", id);

            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }

    }
}
