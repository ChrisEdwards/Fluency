namespace Shiloh.DataGeneration
{
	public class AnonymousAddress : AnonymousBase
	{
		public string StreetName()
		{
			return Anonymous.Value.From( Dictionaries["StreetNames"] );
		}
	}
}