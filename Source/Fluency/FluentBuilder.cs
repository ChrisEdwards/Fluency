#region Usings

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Fluency.Conventions;
using Fluency.IdGenerators;
using Fluency.Utils;
using FluentObjectBuilder;
using JetBrains.Annotations;
using Rhino.Mocks;

#endregion


namespace Fluency
{
	/// <summary>
	/// Exposes a fluent interface to build Fluent objects.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class FluentBuilder< T > : IFluentBuilder< T > where T : class, new()
	{
		readonly Dictionary< string, IFluentBuilder > _builders = new Dictionary< string, IFluentBuilder >();
		readonly MockRepository _mocks;
		protected T _preBuiltResult;
		protected T _prototype;
		readonly IList< IDefaultConvention > _defaultConventions = new List< IDefaultConvention >();
		protected IIdGenerator IdGenerator;


		/// <summary>
		/// Initializes a new instance of the <see cref="FluentBuilder{T}"/> class.
		/// </summary>
		public FluentBuilder()
		{
			IdGenerator = Fluency.Configuration.GetIdGenerator< T >();
			_defaultConventions = Fluency.Configuration.DefaultValueConventions;

			_mocks = new MockRepository();
			_prototype = _mocks.Stub< T >();

			Initialize();
		}


		void Initialize()
		{
			// Specify default values for each property based on conventions.
			foreach ( PropertyInfo propertyInfo in typeof ( T ).GetProperties() )
			{
				// If the property is both Read and Write, set its value.
				if ( propertyInfo.CanWrite && propertyInfo.CanRead )
				{
					// Get the default value from the configured conventions and set the value.
					object defaultValue = GetDefaultValue( propertyInfo );
					_prototype.SetProperty( propertyInfo, defaultValue );
				}
			}

			// Allow the builder to specify its own default values.
			SetupDefaultValues();
		}


		#region IFluentBuilder<T> Members

		/// <summary>
		/// Either returns the override result, or builds this object.
		/// </summary>
		/// <returns></returns>
		public T build()
		{
			// If this is to return a pre-built result, go ahead in return it.
			if ( _preBuiltResult != null )
				return _preBuiltResult;

			// Populate the prototype with the result of executing each builder.
			PopulatePrototypeWithBuiltValues( _builders, _prototype );

			// Allow the client builder the opportunity to do some pre-processing.
			BeforeBuilding();

			T buildResult = _prototype.ShallowClone();

			// Alow the client builder the ability to do some post-processing.
			AfterBuilding( buildResult );

			// Don't rebuild, but return this built item after first build.
			_preBuiltResult = buildResult;

			return buildResult;
		}


		/// <summary>
		/// Populates the prototype with built values.
		/// </summary>
		/// <param name="fluentBuilders">The fluent builders.</param>
		/// <param name="prototype">The prototype.</param>
		static void PopulatePrototypeWithBuiltValues( Dictionary< string, IFluentBuilder > fluentBuilders, T prototype )
		{
			foreach ( KeyValuePair< string, IFluentBuilder > pair in fluentBuilders )
			{
				string propertyName = pair.Key;
				IFluentBuilder builder = pair.Value;

				object propertyValue = builder.InvokeMethod( "build" );
				prototype.SetProperty( propertyName, propertyValue );
			}
		}

		#endregion


		#region Events

		/// <summary>
		/// Allows the builder to specify default values for fields in the object.
		/// </summary>
		protected virtual void SetupDefaultValues() {}


		/// <summary>
		/// Event that fires after the object is built to allow the builder to do post-processing.
		/// </summary>
		protected virtual void BeforeBuilding() {}


		/// <summary>
		/// Event that fires after the object is built to allow the builder to do post-processing.
		/// </summary>
		/// <param name="buildResult">The build result.</param>
		protected virtual void AfterBuilding( T buildResult ) {}


		/// <summary>
		/// Builds an object of type T based on the specs specified in the builder.
		/// </summary>
		/// <returns></returns>
		[ Obsolete( "Use AfterBuild() method to perform post-processinig." ) ]
		[ UsedImplicitly ]
		protected virtual T BuildFrom( T values )
		{
			return new T();
		}

		#endregion


		#region Property Value Setters

		/// <summary>
		/// Sets the value to be built for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="propertyValue">The property value.</param>
		protected void SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			// If we try to change info after prebuilt result is set...throw error since the change wont be reflected in the prebuilt result.
			if ( _preBuiltResult != null )
				throw new FluencyException( "Cannot set property once a pre built result has been given. Property change will have no affect." );

			// Get the property to set
			PropertyInfo property = propertyExpression.GetPropertyInfo();

			RemoveBuilderFor( property );

			// Set the property on the prototype object.
			_prototype.SetProperty( propertyExpression, propertyValue );
		}


		/// <summary>
		/// Sets the builder to be used to construct the value for the specified propety.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		protected void SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder ) where TPropertyType : class, new()
		{
			// Due to lack of polymorphism in generic parameters.
			if ( !( builder is FluentBuilder< TPropertyType > ) )
			{
				throw new ArgumentException(
						"Invalid builder type. Builder type must be a builder of Property Type. \n  BuilderType='{0}'\n  PropertyType='{1}'".format_using( builder.GetType().FullName,
						                                                                                                                                   typeof ( TPropertyType ).FullName ) );
			}

			AddBuilderFor( propertyExpression, builder );
		}


		/// <summary>
		/// Sets the list builder.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
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

			AddBuilderFor( propertyExpression, builder );
		}


		/// <summary>
		/// Adds a new builder to a list of builders for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		protected void AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, FluentBuilder< TPropertyType > builder )
				where TPropertyType : class, new()
		{
			ListBuilderFor( propertyExpression ).Add( builder );
		}


		/// <summary>
		/// Adds a new value to the list of values to be built for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="value">The value.</param>
		protected void AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, TPropertyType value ) where TPropertyType : class, new()
		{
			ListBuilderFor( propertyExpression ).Add( value );
		}

		#endregion


		#region Builder Accessors

		/// <summary>
		/// Removes the builder for the specified property if it exists.
		/// </summary>
		/// <param name="property">The property.</param>
		void RemoveBuilderFor( PropertyInfo property )
		{
			// If a builder already exists for this type, remove it.
			if ( _builders.ContainsKey( property.Name ) )
				_builders.Remove( property.Name );
		}


		/// <summary>
		/// Adds the specified builder for the specified property...removing any builder that may have already been assigend.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		void AddBuilderFor< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder )
		{
			PropertyInfo property = propertyExpression.GetPropertyInfo();
			AddBuilderFor( property, builder );
		}


		/// <summary>
		/// Adds the specified builder for the specified property...removing any builder that may have already been assigend.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <param name="builder">The builder.</param>
		void AddBuilderFor( PropertyInfo property, IFluentBuilder builder )
		{
			// Since we are adding a new builder for this property, remove the existing one if it exists.
			RemoveBuilderFor( property );

			// Add the new builder.
			_builders.Add( property.Name, builder );
		}


		/// <summary>
		/// Gets the list builder for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public FluentListBuilder< TPropertyType > ListBuilderFor< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression ) where TPropertyType : class, new()
		{
			PropertyInfo property = propertyExpression.GetPropertyInfo();

			if ( !_builders.ContainsKey( property.Name ) )
				throw new ArgumentException( "List Builder does not exist for property [" + property.Name + "]" );

			return (FluentListBuilder< TPropertyType >)_builders[property.Name];
		}


		/// <summary>
		/// Gets the builder for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public FluentBuilder< TPropertyType > BuilderFor< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression ) where TPropertyType : class, new()
		{
			PropertyInfo property = propertyExpression.GetPropertyInfo();

			if ( !_builders.ContainsKey( property.Name ) )
				throw new ArgumentException( "Builder does not exist for property [" + property.Name + "]" );

			return (FluentBuilder< TPropertyType >)_builders[property.Name];
		}

		#endregion


		/// <summary>
		/// Casts this builder to the specified builder type.
		/// </summary>
		/// <typeparam name="BUILDERTYPE">The type of the UILDERTYPE.</typeparam>
		/// <returns></returns>
		public BUILDERTYPE As< BUILDERTYPE >() where BUILDERTYPE : FluentBuilder< T >
		{
			return (BUILDERTYPE)this;
		}


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
			return IdGenerator.GetNextId();
		}


		#region SetPrebuiltResult

		/// <summary>
		/// Overrides the build result with the specified object. If this is called, the builder will not perform the build, but will rather, return the prebuilt result.
		/// </summary>
		/// <param name="buildResult">
		/// The build result.
		/// </param>
		/// <returns>
		/// Builder that will return the specified result.
		/// </returns>
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

		#endregion


		#region Conventions

		/// <summary>
		/// Gets the default value for a specified property.
		/// </summary>
		/// <param name="propertyInfo">The property info.</param>
		/// <returns></returns>
		object GetDefaultValue( PropertyInfo propertyInfo )
		{
			object result = null;

			// Check each of the conventions and apply them if necessary.
			foreach ( IDefaultConvention defaultConvention in _defaultConventions )
			{
				// if more than one convention matches...last one wins.
				if ( defaultConvention.AppliesTo( propertyInfo ) )
					result = defaultConvention.DefaultValue( propertyInfo );
			}

			// Returns null if no convention matched.
			return result;
		}

		#endregion
	}
}