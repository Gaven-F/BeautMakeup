using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

public class CustomerController
{
	private readonly List<Customer> data = [new() { Id = 1, Name = "Customer 1" }, new() { Id = 2, Name = "Customer 2" }];

	public ActionResult<IEnumerable<Customer>> Get() => data.ToArray();

	public ActionResult<Customer> Get([FromRoute] int key) => new Customer { Id = key, Name = $"Customer {key}" };
}

public class Customer
{
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
}
