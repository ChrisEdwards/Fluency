using System;


namespace FluentObjectBuilder.DataGeneration
{
	public interface IValueConstraints
	{
		DateTime MaxDateTime { get; }
		DateTime MinDateTime { get; }
	}
}