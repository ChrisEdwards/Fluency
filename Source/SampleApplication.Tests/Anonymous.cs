using Fluency.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests
{
	public class Anonymous
	{
		private int _uniqueId = -1;


		public int GetUniqueId()
		{
			return _uniqueId--;
		}


		public Order Order()
		{
			var order = new Order
			            	{
			            			Id = GetUniqueId(),
			            			Customer = Customer()
			            	};
			return order;
		}


		public Customer Customer()
		{
			var customer = new Customer
			               	{
			               			Id = GetUniqueId(),
			               			FirstName = ARandom.FirstName(),
			               			LastName = ARandom.LastName()
			               	};
			return customer;
		}
	}
}