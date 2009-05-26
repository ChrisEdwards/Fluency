using System;
using System.Linq;
using System.Reflection;


namespace FluentObjectBuilder
{
	public static class ReflectionExtensions
	{
		public static PropertyInfo[] GetPublicGetProperties( this Type type )
		{
			return type.FindMembers(MemberTypes.Property,
					BindingFlags.Public | BindingFlags.Instance,
					( m, f ) => ( (PropertyInfo)m ).CanRead,
					null )
					.Cast< PropertyInfo >()
					.ToArray();
		}
		public static PropertyInfo[] GetPublicReadOnlyProperties( this Type type )
		{
			return type.FindMembers(MemberTypes.Property,
					BindingFlags.Public | BindingFlags.Instance,
					( m, f ) => ( (PropertyInfo)m ).CanRead && !((PropertyInfo)m).CanWrite,
					null )
					.Cast< PropertyInfo >()
					.ToArray();
		}
	}
}