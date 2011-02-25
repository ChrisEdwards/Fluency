using System;
using System.Collections.Generic;
using Shiloh.DataGeneration.ValueConstraints;
using UMMO.TestingUtils.RandomData;


namespace Shiloh.DataGeneration
{
	public abstract class AnonymousBase< TAnonymousType >
	{
		protected static readonly ExtendedRandom Random = new ExtendedRandom();
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


		public TAnonymousType From(IList<TAnonymousType> list)
		{
			return Anonymous.Value.From(list);
		}


		public TAnonymousType From(params TAnonymousType[] list)
		{
			return Anonymous.Value.From(list);
		}
	}
}