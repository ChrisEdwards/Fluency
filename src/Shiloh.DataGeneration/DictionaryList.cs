using System.Collections.Generic;
using System.Linq;
using Shiloh.Utils;


namespace Shiloh.DataGeneration
{
	public class DictionaryList
	{
		readonly Dictionary< string, string[] > _dictionaries = new Dictionary< string, string[] >();


		public string[] this[ string dictionaryName ]
		{
			get
			{
				if ( _dictionaries.ContainsKey( dictionaryName ) )
					return _dictionaries[dictionaryName];

				string[] values = IO.ForEachLineIn( dictionaryName + ".txt" ).ToArray();
				_dictionaries.Add( dictionaryName, values );

				return values;
			}
		}
	}
}