using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCheckLan.Models
{
    public class MenuItemOperation
    {
        private readonly IConfiguration config;
        private readonly string constr;

        public MenuItemOperation(IConfiguration config)
        {
            this.config = config;
            constr = config.GetConnectionString("constr");
        }
        public async Task<List<MenuItem>> GetMenuItems()
        {
            List<MenuItem> MeItems = new List<MenuItem>();
            var conn = new SqlConnection(constr);
            var comm = new SqlCommand("select * from MenuItems", conn);
            conn.Open();
            var reader = await comm.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                MenuItem m;
                while (reader.Read())
                {
                    m = new MenuItem();
                    m.Id = reader.GetInt32(0);
                    m.Name = reader.GetString(1);
                    m.Price = Convert.ToDouble(reader[2]);
                    m.Active = reader.GetBoolean(3);
                    m.DateOfLaunch = reader.GetDateTime(4);
                    m.Category = reader.GetString(5);
                    m.FreeDelivery = reader.GetBoolean(6);
                    MeItems.Add(m);
                }
            }
            reader.Close();
            conn.Close();

            return MeItems;
        }

        public MenuItem GetMenuItem(int id)
        {
            MenuItem m = null;
            var conn = new SqlConnection(constr);
            var comm = new SqlCommand("select * from MenuItems where Id=@id", conn);
            conn.Open();
            comm.Parameters.AddWithValue("@id", id);
            var reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                m= new MenuItem();
                reader.Read();

                m.Id = reader.GetInt32(0);
                m.Name = reader.GetString(1);
                m.Price = Convert.ToDouble(reader[2]);
                m.Active = reader.GetBoolean(3);
                m.DateOfLaunch = reader.GetDateTime(4);
                m.Category = reader.GetString(5);
                m.FreeDelivery = reader.GetBoolean(6);

            }
            reader.Close();
            conn.Close();

            return m;
        }



        public int AddNew(MenuItem p)
        {
            var conn = new SqlConnection(constr);
            var comm = new SqlCommand("insert into MenuItems values(@p1,@p2,@p3,@p4,@p5,@p6)", conn);
            comm.Parameters.AddWithValue("@p1", p.Name);
            comm.Parameters.AddWithValue("@p2", p.Price);
            comm.Parameters.AddWithValue("@p3", p.Active);
            comm.Parameters.AddWithValue("@p4", p.DateOfLaunch);
            comm.Parameters.AddWithValue("@p5", p.Category);
            comm.Parameters.AddWithValue("@p6", p.FreeDelivery);

            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }

        public int Update(MenuItem m)
        {
            var conn = new SqlConnection(constr);
            var comm = new SqlCommand("update MenuItems set Name=@p1, Price=@p2 ,Active=@p4,DateOfLaunch=@p5,Category=@p6,FreeDelivery=@p7 where Id=@p3", conn);
            comm.Parameters.AddWithValue("@p1", m.Name);
            comm.Parameters.AddWithValue("@p2", m.Price);
            comm.Parameters.AddWithValue("@p3", m.Id);
            comm.Parameters.AddWithValue("@p4", m.Active);

            comm.Parameters.AddWithValue("@p5", m.DateOfLaunch);

            comm.Parameters.AddWithValue("@p6", m.Category);
            comm.Parameters.AddWithValue("@p7", m.FreeDelivery);


            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }

        public int Delete(int id)
        {
            var conn = new SqlConnection(constr);
            var comm = new SqlCommand("delete from MenuItems where Id=@p1", conn);
            comm.Parameters.AddWithValue("@p1", id);

            conn.Open();
            var rows = comm.ExecuteNonQuery();
            conn.Close();
            return rows;
        }
    }
}

    

