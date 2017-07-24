using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using DapperDemo.Models;

namespace DapperDemo.DataAccessLayer
{
    public class CustomerRespository : ICustomerRespository
    {
        private readonly IDbConnection _conn;

        public CustomerRespository()
        {
            _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public List<Customer> GetCustomers(int amount, string sort)
        {
            return _conn.Query<Customer>(
                "SELECT TOP " + amount + " [CustomerID],[CustomerFirstName],[CustomerLastName],[IsActive] FROM [Customer] ORDER BY CustomerID " + sort).ToList();
        }

        public Customer GetSingleCustomer(int customerId)
        {
            return _conn.Query<Customer>("SELECT[CustomerID],[CustomerFirstName],[CustomerLastName],[IsActive] FROM [Customer] WHERE CustomerID =@CustomerID", 
                new { CustomerID = customerId }).SingleOrDefault();
        }

        public bool InsertCustomer(Customer customer)
        {
            var rowsAffected = _conn.Execute(@"INSERT Customer([CustomerFirstName],[CustomerLastName],[IsActive]) values (@CustomerFirstName, @CustomerLastName, @IsActive)", 
                new
                {
                    CustomerFirstName = customer.CustomerFirstName,
                    CustomerLastName = customer.CustomerLastName,
                    IsActive = true
                });

            return rowsAffected > 0;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var rowsAffected = _conn.Execute(
                "UPDATE [Customer] SET [CustomerFirstName] = @CustomerFirstName ,[CustomerLastName] = @CustomerLastName, [IsActive] = @IsActive WHERE CustomerID = " 
                + customer.CustomerID, customer);

            return rowsAffected > 0;
        }

        public bool DeleteCustomer(int customerId)
        {
            var rowsAffected = _conn.Execute(@"DELETE FROM [jeremy].[Customer] WHERE CustomerID = @CustomerID", new { CustomerID = customerId });

            return rowsAffected > 0;
        }
    }
}