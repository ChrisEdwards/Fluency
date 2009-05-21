using FluentObjectBuilder.DataGeneration;
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


		public Project Project()
		{
			var project = new Project
			              	{
			              			Id = GetUniqueId(),
			              			Name = ARandom.Title( 100 )
			              	};
			return project;
		}
	}
}