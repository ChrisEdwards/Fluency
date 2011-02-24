using System;
using Shiloh.DataGeneration.ValueConstraints;


namespace Shiloh.DataGeneration
{
	public abstract class AnonymousBase< TAnonymousType >
	{
		protected static readonly Random _random = new Random();
		readonly TAnonymousType _defaultValue;


		public AnonymousBase()
		{
			_defaultValue = GetRandomValue();
		}


		public static implicit operator TAnonymousType( AnonymousBase< TAnonymousType > anonymous )
		{
			return anonymous._defaultValue;
		}


		protected abstract TAnonymousType GetRandomValue();

		protected IValueConstraints ValueConstraints
		{
			get { return Anonymous.ValueConstraints; }
		}
	}
}