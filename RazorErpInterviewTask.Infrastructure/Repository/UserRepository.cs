﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using RazorErpInterviewTask.Core.Interfaces;
using RazorErpInterviewTask.Core.Entities;
using RazorErpInterviewTask.Application.Models;
using System.Security.Claims;
using System.Text;
using RazorErpInterviewTask.Core.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Data;

namespace RazorErpInterviewTask.Infrastructure.Repository
{
    public class UserRepository : IUserRepository<User, UserAddUpdate, UserLogin>
    {
        private readonly DapperDbContext _dapperDbContext;
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public UserRepository(DapperDbContext dapperDbContext, IConfiguration configuration)
        {
            _dapperDbContext = dapperDbContext;
            _dbConnection = _dapperDbContext.CreateConnection();
            _configuration = configuration;
        }

        public async Task<int> AddAsync(UserAddUpdate entity)
        {
            var sql = "INSERT INTO [dbo].[User] (Username, Password, FirstName, LastName, Company, Role) VALUES (@Username, @Password, @FirstName, @LastName, @Company, @Role)";

            var result = await _dbConnection.ExecuteAsync(sql, entity);
            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM [dbo].[User] WHERE Id = @Id";

            var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });
            return result;

        }

        public async Task<IEnumerable<User>> GetAllUsersByCompanyAsync(string role, string company)
        {
            var sql = "SELECT * FROM [dbo].[User] WHERE Company = @Company";
            
            var parameters = new DynamicParameters();
            parameters.Add("Company", company);

            if (role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                sql = sql + " AND Role = @Role";
                parameters.Add("Role", 2);
            }

            var result = await _dbConnection.QueryAsync<User>(sql, parameters);
            return result.ToList();
        }

        public async Task<User> GetByIdAsync(int id, string role, string company)
        {
            var sql = "SELECT * FROM [dbo].[User] WHERE Id = @Id  AND Company = @Company";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Company", company);

            if (role.Equals("User", StringComparison.OrdinalIgnoreCase))
            {
                sql = sql + " AND Role = @Role";
                parameters.Add("Role", 2);
                
            }

            var result = await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, parameters);
            return result;
        }

        public async Task<int> UpdateAsync(int id, UserAddUpdate entity)
        {

            var sql = "UPDATE [dbo].[User] SET Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, Company = @Company, [Role] = @Role WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Username", entity.Username);
            parameters.Add("Password", entity.Password); 
            parameters.Add("FirstName", entity.FirstName);
            parameters.Add("LastName", entity.Lastname);
            parameters.Add("Company", entity.Company);
            parameters.Add("Role", entity.Role);
            parameters.Add("Id", id);

            var result = await _dbConnection.ExecuteAsync(sql, parameters);
            return result;
        }

        public async Task<string> Auth(UserLogin user)
        {
            var sql = "SELECT * FROM [dbo].[User] WHERE Username = @Username AND Password = @Password";

            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username);
            parameters.Add("Password", user.Password);

            var result = await _dbConnection.QueryAsync<User>(sql, parameters);

            if (result.Count() > 0)
            {
                return GenerateToken(result.FirstOrDefault());
            }


            return string.Empty;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, ((Role)user.Role).ToString()),
                new Claim("Company", user.Company)
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiry time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
