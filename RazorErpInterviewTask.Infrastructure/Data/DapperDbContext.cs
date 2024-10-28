using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using RazorErpInterviewTask.Core.Interfaces;
using RazorErpInterviewTask.Core.Entities;
using RazorErpInterviewTask.Application.Models;
using System.Security.Claims;
using System.Text;
using System.Data;

namespace RazorErpInterviewTask.Infrastructure.Repository
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
