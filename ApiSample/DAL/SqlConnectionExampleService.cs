using ApiSample.DTOs.Guests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ApiSample.DAL
{
    public class SqlConnectionExampleService : IExampleService
    {
        //Proszę tutaj wpisać swój connection string
        private readonly string ConnStr = @"Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=yoshi;Integrated Security=True;";

        public ICollection<GuestResponseDto> GetGuestsCollection(string lastName)
        {
            var listToReturn = new List<GuestResponseDto>();

            using var sqlConn = new SqlConnection(ConnStr);
            using var sqlCmd = new SqlCommand
            {
                Connection = sqlConn
            };
            if(string.IsNullOrEmpty(lastName))
                sqlCmd.CommandText = @"SELECT g.IdGosc, g.Imie, g.Nazwisko, g.Procent_rabatu
                                    FROM Gosc g;";
            else
            {
                sqlCmd.CommandText = @"SELECT g.IdGosc, g.Imie, g.Nazwisko, g.Procent_rabatu
                                       FROM Gosc g
                                       WHERE g.Nazwisko = @LastName;";
                sqlCmd.Parameters.AddWithValue("LastName", lastName);
            }
            sqlConn.Open();
            using var reader = sqlCmd.ExecuteReader();
            while(reader.Read())
            {
                var item = new GuestResponseDto
                {
                    IdGuest = int.Parse(reader["IdGosc"].ToString()),
                    FirstName = reader["Imie"].ToString(),
                    LastName = reader["Nazwisko"].ToString(),
                    DiscountPercent = !string.IsNullOrEmpty(reader["Procent_rabatu"]?.ToString())
                                        ? int.Parse(reader["Procent_rabatu"].ToString()) : (int?)null

                };
                listToReturn.Add(item);
            }

            return listToReturn;
        }


        public bool AddGuest(GuestRequestDto newGuest)
        {
            throw new NotImplementedException();
        }

        public bool DeleteGuest(int id)
        {
            throw new NotImplementedException();
        }

        public GuestResponseDto GetGuestById(int idGuest)
        {
            throw new NotImplementedException();
        }


        public string Test()
        {
            throw new NotImplementedException();
        }

        public bool UpdateGuest(int id, GuestRequestDto updateGuest)
        {
            throw new NotImplementedException();
        }
    }
}
