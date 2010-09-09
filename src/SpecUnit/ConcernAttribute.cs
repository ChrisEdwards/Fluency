using System;
using NUnit.Framework;


namespace SpecUnit
{
	public class ConcernAttribute : TestFixtureAttribute
	{
		readonly string _name;

		public string Name
		{
			get { return _name; }
		}


		public ConcernAttribute( Type type )
		{
			_name = type.Name;
		}


		public ConcernAttribute( Type type, string clarification )
		{
			_name = type.Name + " (" + clarification + ")";
		}


		public ConcernAttribute( string name )
		{
			_name = name;
		}
	}
}