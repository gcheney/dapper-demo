using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperDemo.Models;

namespace DapperDemo.DataAccessLayer
{
    internal interface ICustomerRespository
    {
        List<Customer> GetCustomers(int amount, string sort);

        Customer GetSingleCustomer(int customerId);

        bool InsertCustomer(Customer ourCustomer);

        bool DeleteCustomer(int customerId);

        bool UpdateCustomer(Customer ourCustomer);
    }
}