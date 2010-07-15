using System;


namespace Shiloh.DataGeneration.ValueConstraints
{
	public interface IValueConstraints
	{
		DateTime MaxDateTime { get; }
		DateTime MinDateTime { get; }
	}
}