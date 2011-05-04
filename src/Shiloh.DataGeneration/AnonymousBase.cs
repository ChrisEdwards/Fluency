using System.Reflection;


namespace Shiloh.DataGeneration
{
	public class AnonymousBase
	{
		public static readonly DictionaryList Dictionaries = new DictionaryList( Assembly.GetExecutingAssembly(), "Shiloh.DataGeneration.Dictionaries" );
	}
}