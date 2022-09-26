using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SIBKMNET_WebApplication.Models
{
    public class Country
    {
            public int Id { get; set; }
            public string Name { get; set; }

        public Country()
        {
            Id = this.Id;
            Name = this.Name;
            
        }

        public Country(int id, string name, string pembaruan)
        {
            Id = id;
            Name = name;
            
        }

    }
}
