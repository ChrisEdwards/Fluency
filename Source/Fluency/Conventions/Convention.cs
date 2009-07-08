using System;
using System.Reflection;
using Fluency.DataGeneration;


namespace Fluency.Conventions
{
    public static class Convention
    {
        public static LambdaConvention ByName<T>( string propertyName, Func<PropertyInfo, object> defaultValue)
        {
            return new LambdaConvention(
                p => p.PropertyType == typeof(T) && p.Name.ToLower().Contains(propertyName.ToLower()),
                defaultValue);
        }

        public static LambdaConvention ByType<T>( Func<PropertyInfo, object> defaultValue)
        {
            return new LambdaConvention(
                p => p.PropertyType == typeof(T),
                defaultValue);
        }


    	/// <summary>
    	/// Convention to generate a random First Name when the property name contains the string "firstname"
    	/// </summary>
    	/// <returns></returns>
		public static IDefaultConvention FirstName()
    	{
    		return ByName< string >( "FirstName", p => ARandom.FirstName());
    	}


    	/// <summary>
    	/// Convention to generate a random string when the datatype of the property is string.
    	/// </summary>
    	/// <returns></returns>
		public static IDefaultConvention String(int length)
    	{
    		int stringLength = length;
    		return ByType<string>( p => ARandom.String( stringLength ) );
		}


		/// <summary>
		/// Convention to generate a random First Name when the property name contains the string "firstname"
		/// </summary>
		/// <returns></returns>
		public static IDefaultConvention LastName()
		{
			return ByName<string>("LastName", p => ARandom.LastName());
		}


		/// <summary>
		/// Convention to generate a random Date when the property is of type DateTime
		/// </summary>
		/// <returns></returns>
    	public static IDefaultConvention DateType()
    	{
    		return ByType< DateTime >( p => ARandom.DateTime() );
		}


		/// <summary>
		/// Convention to generate a random Integer when the property is of type int
		/// </summary>
		/// <returns></returns>
    	public static IDefaultConvention IntegerType()
    	{
    		return ByType< int >( p => ARandom.Int() );
    	}
    }
}