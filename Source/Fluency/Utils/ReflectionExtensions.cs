using System;
using System.Linq;
using System.Reflection;


namespace Fluency.Utils
{
	public static class ReflectionExtensions
	{
		public static PropertyInfo[] GetPublicGetProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}


		public static PropertyInfo[] GetPublicReadOnlyProperties( this Type type )
		{
			return type.FindMembers( MemberTypes.Property,
			                         BindingFlags.Public | BindingFlags.Instance,
			                         ( m, f ) => ( (PropertyInfo)m ).CanRead && !( (PropertyInfo)m ).CanWrite,
			                         null )
					.Cast< PropertyInfo >()
					.ToArray();
		}


		public static void SetProperty( this object prototype, string propertyName, object propertyValue )
		{
			PropertyInfo property = prototype.GetType().GetProperty( propertyName );
			property.SetValue( prototype, propertyValue, null );
		}


		public static object InvokeMethod( this object target, string methodName, params object[] parameters )
		{
			MethodInfo buildMethod = target.GetType().GetMethod( methodName );
			return buildMethod.Invoke( target, parameters );
		}
	}
}