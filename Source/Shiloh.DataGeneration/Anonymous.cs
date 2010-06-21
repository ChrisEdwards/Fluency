using System;


namespace Shiloh.DataGeneration
{
	public static class Anonymous
	{
		public static AnonymousInteger Integer
		{
			get { return new AnonymousInteger(); }
		}

		public static AnonymousInteger Int
		{
			get { return new AnonymousInteger(); }
		}

		public static AnonymousValue Value
		{
			get { return new AnonymousValue(); }
		}
	}
}