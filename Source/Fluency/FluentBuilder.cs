using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Fluency.IdGenerators;
using Fluency.Utils;
using FluentNHibernate.Utils;
using FluentObjectBuilder;
using Rhino.Mocks;


namespace Fluency
{
	public abstract class FluentBuilder< T > : IFluentBuilder< T >
	{
		private static bool _executedBuildOnceAlready;
		private static int _uniqueId = -10;
		private readonly Dictionary< string, IFluentBuilder > _builders = new Dictionary< string, IFluentBuilder >();
		private readonly MockRepository _mocks;
		protected T _preBuiltResult;
		protected T _prototype;
		protected IIdGenerator IdGenerator = new StaticValueIdGenerator( 0 );


		/// <summary>
		/// Initializes a new instance of the <see cref="FluentBuilder{T}"/> class.
		/// </summary>
		public FluentBuilder()
		{
			_mocks = new MockRepository();
			_prototype = _mocks.Stub< T >();
			SetupDefaultValues();
		}


		#region IFluentBuilder<T> Members

		/// <summary>
		/// Either returns the override result, or builds this object.
		/// </summary>
		/// <returns></returns>
		public T build()
		{
			if ( _executedBuildOnceAlready )
				throw new Exception( "Cannot build more than once [" + GetType().FullName + "]." );

			// Populate the prototype with the result of executing each builder.
			foreach ( var pair in _builders )
			{
				string propertyName = pair.Key;
				IFluentBuilder builder = pair.Value;

				object propertyValue = builder.InvokeMethod( "build" );
				_prototype.SetProperty( propertyName, propertyValue );
			}

			//_executedBuildOnceAlready = true;

			return (T)( (object)_preBuiltResult ?? BuildFrom( _prototype ) ); //BuildFromPrototype( _prototype );
		}

		#endregion


		/// <summary>
		/// Setups the default values.
		/// </summary>
		/// <param name="defaults">The defaults.</param>
		protected abstract void SetupDefaultValues();


//		private T BuildFromPrototype( T prototype )
//		{
//			// User Automapper to copy values
//
//			//IMappingExpression< T, T > map = Mapper.CreateMap< T, T >();
//			// TODO: Must fix... readonly properties break.
//			//map.IgnoreReadOnlyProperties();
//			//var result = new T();
//			T result = Mapper.DynamicMap< T, T >( prototype );
//			return result;
//		}


		/// <summary>
		/// Sets the value to be built for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="propertyValue">The property value.</param>
		protected void SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			Accessor accessor = ReflectionHelper.GetAccessor( propertyExpression );
			accessor.SetValue( _prototype, propertyValue );
		}


		/// <summary>
		/// Sets the builder to be used to construct the value for the specified propety.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		protected void SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder ) where TPropertyType : class
		{
			// Due to lack of polymorphism in generic parameters.
			if ( !( builder is FluentBuilder< TPropertyType > ) )
			{
				throw new ArgumentException(
						"Invalid builder type. Builder type must be a builder of Property Type. \n  BuilderType='{0}'\n  PropertyType='{1}'".format_using( builder.GetType().FullName,
						                                                                                                                                   typeof ( TPropertyType ).FullName ) );
			}

			PropertyInfo property = ReflectionHelper.GetProperty( propertyExpression );

			if ( _builders.ContainsKey( property.Name ) )
				_builders.Remove( property.Name );

			_builders.Add( property.Name, builder );
		}


		protected void SetList< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder ) where TPropertyType : class
		{
			if ( !typeof ( TPropertyType ).FullName.Contains( "IList" ) )
				throw new ArgumentException( "PropertyType must derive from IList" );

			// Due to lack of polymorphism in generic parameters.
			if ( !( builder is IFluentBuilder< TPropertyType > ) )
			{
				throw new ArgumentException(
						"Invalid builder type. Builder type must be a FluentListBuilder of Property Type. \n  BuilderType='{0}'\n  PropertyType='{1}'".format_using( builder.GetType().FullName,
						                                                                                                                                             typeof ( TPropertyType ).FullName ) );
			}

			PropertyInfo property = ReflectionHelper.GetProperty( propertyExpression );

			if ( _builders.ContainsKey( property.Name ) )
				_builders.Remove( property.Name );

			_builders.Add( property.Name, builder );
		}


		/// <summary>
		/// Adds a new builder to a list of builders for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		protected void AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, FluentBuilder< TPropertyType > builder )
				where TPropertyType : class
		{
			GetListBuilderFor( propertyExpression ).Add( builder );
		}


		/// <summary>
		/// Adds a new value to the list of values to be built for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="value">The value.</param>
		protected void AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, TPropertyType value ) where TPropertyType : class
		{
			GetListBuilderFor( propertyExpression ).Add( value );
		}


		/// <summary>
		/// Gets the list builder for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		private FluentListBuilder< TPropertyType > GetListBuilderFor< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression )
		{
			PropertyInfo property = ReflectionHelper.GetProperty( propertyExpression );

			if ( !_builders.ContainsKey( property.Name ) )
				throw new ArgumentException( "List Builder does not exist for property [" + property.Name + "]" );

			return (FluentListBuilder< TPropertyType >)_builders[property.Name];
		}


		/// <summary>
		/// Builds an object of type T based on the specs specified in the builder.
		/// </summary>
		/// <returns></returns>
		protected abstract T BuildFrom( T values );


		/// <summary>
		/// Performs an implicit conversion from <see cref="BancVue.Tests.Common.TestDataBuilders.FluentBuilder&lt;T&gt;"/> to <see cref="T"/>.
		/// This is so we don't have to explicitly call "build()" in the code.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator T( FluentBuilder< T > builder )
		{
			return builder.build();
		}


		/// <summary>
		/// Gets a unique id with a negative value so that it does not conflict with existing data in the database.
		/// </summary>
		/// <returns></returns>
		protected int GenerateNewId()
		{
			//return _uniqueId--;
			return 0;
		}


		/// <summary>
		/// Overrides the build result with the specified object. If this is called, the builder will not perform the build, but will rather, return the prebuilt result.
		/// </summary>
		/// <param name="buildResult">The build result.</param>
		public FluentBuilder< T > AliasFor( T buildResult )
		{
			return UsePreBuiltResult( buildResult );
		}


		/// <summary>
		/// Overrides the build result with the specified object. If this is called, the builder will not perform the build, but will rather, return the prebuilt result.
		/// </summary>
		/// <param name="buildResult">The build result.</param>
		public FluentBuilder< T > UsePreBuiltResult( T buildResult )
		{
			_preBuiltResult = buildResult;
			return this;
		}
	}
}