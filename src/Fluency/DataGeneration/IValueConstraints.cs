using System;


namespace Fluency.DataGeneration
{
	public interface IValueConstraints
	{
		DateTime MaxDateTime { get; }
		DateTime MinDateTime { get; }
	}
}