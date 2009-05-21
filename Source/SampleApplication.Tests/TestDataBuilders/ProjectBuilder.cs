using FluentObjectBuilder;
using FluentObjectBuilder.DataGeneration;
using SampleApplication.Domain;


namespace SampleApplication.Tests.TestDataBuilders
{
	public class ProjectBuilder :TestDataBuilder<Project >
	{
		protected override Project _build()
		{
			return new Project
			       	{
			       			Id = GetUniqueId(),
			       			Name = ARandom.FullName()
			       	};
		}
	}
}