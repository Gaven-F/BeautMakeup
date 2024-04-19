﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Server.Controllers;

[Route("[controller]/[action]")]
public class CustomerController
{
    public ActionResult<IEnumerable<Customer>> Get() => new List<Customer>
    {
        new() { Id = 1, Name = "Customer 1" },
        new() { Id = 2, Name = "Customer 2" }
    };

    //public ActionResult<Customer> Get([FromRoute] int key)
    //{
    //    return new Customer { Id = key, Name = $"Customer {key}" };
    //}

    public ActionResult<Customer> GetById([FromRoute] int key)
    {
        return new Customer { Id = key, Name = $"Customer {key}" };
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}