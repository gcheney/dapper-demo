using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DapperDemo.DataAccessLayer;
using DapperDemo.Models;

namespace DapperDemo.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly CustomerRespository _customerRespository;

        public CustomerController()
        {
            _customerRespository = new CustomerRespository();
        }

        // GET: api/Customer
        [Route("Customers")]
        [HttpGet]
        public List<Customer> Get()
        {
            return _customerRespository.GetCustomers(10, "ASC");
        }

        [Route("Customers/{amount}/{sort}")]
        [HttpGet]
        public List<Customer> Get(int amount, string sort)
        {
            return _customerRespository.GetCustomers(amount, sort);
        }

        // GET: api/Customer/5
        [Route("api/customers/{id}")]
        [HttpGet]
        public Customer GetCustomer(int id)
        {
            return _customerRespository.GetSingleCustomer(id);
        }

        // POST: api/customer
        [Route("api/customer")]
        [HttpPost]
        public bool CreateCustomer([FromBody]Customer customer)
        {
            return _customerRespository.InsertCustomer(customer);
        }

        // PUT: api/customer/5
        [Route("api/customers/{id}")]
        [HttpPut]
        public bool Put(int id, [FromBody]Customer customer)
        {
            var existingCustomer = _customerRespository.GetSingleCustomer(id);

            existingCustomer.CustomerFirstName = customer.CustomerFirstName;
            existingCustomer.CustomerLastName = customer.CustomerLastName;
            existingCustomer.IsActive = customer.IsActive;

            return _customerRespository.UpdateCustomer(existingCustomer);
        }

        // DELETE: api/customer/5
        [Route("api/customers/{id}")]
        [HttpDelete]
        public bool Delete(int id)
        {
            return _customerRespository.DeleteCustomer(id);
        }
    }
}
