// Copyright 2011 Chris Edwards
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;
using Fluency.Conventions;
using Fluency.IdGenerators;
using Fluency.Utils;


namespace Fluency
{
	/// <summary>
	/// Exposes a fluent interface to build Fluent objects.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class FluentBuilder< T > : IFluentBuilder< T > where T : class
	{
		private readonly Dictionary< string, IFluentBuilder > _builders = new Dictionary< string, IFluentBuilder >();
		protected T _preBuiltResult;
		protected readonly ListDictionary _properties;
		private readonly IList< IDefaultConvention > _defaultConventions = new List< IDefaultConvention >();
		protected IIdGenerator IdGenerator;
// ReSharper disable StaticFieldInGenericType
		private static ConstructorInfo _typeConstructor;
		private static bool _triedToGetConstructor;
// ReSharper restore StaticFieldInGenericType


		/// <summary>
		/// Initializes a new instance of the <see cref="FluentBuilder{T}"/> class.
		/// </summary>
		public FluentBuilder()
		{
			IdGenerator = Fluency.Configuration.GetIdGenerator< T >();
			_defaultConventions = Fluency.Configuration.DefaultValueConventions;

			_properties = new ListDictionary();

			Initialize();
		}


		private void Initialize()
		{
			// Specify default values for each property based on conventions.
			foreach ( var propertyInfo in typeof ( T ).GetProperties() )
			{
				// If the property is both Read and Write, set its value.
				if ( propertyInfo.CanWrite && propertyInfo.CanRead )
				{
					// Get the default value from the configured conventions and set the value.
					var defaultValue = GetDefaultValue( propertyInfo );
					_properties.Add( propertyInfo.Name, defaultValue );
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
			foreach ( var pair in _builders )
			{
				var propertyName = pair.Key;
				var builder = pair.Value;

				var propertyValue = builder.InvokeMethod( "build" );
				_properties[propertyName] = propertyValue;
			}

			// Allow the client builder the opportunity to do some pre-processing.
			BeforeBuilding();

			var result = GetNewInstance();
			foreach ( DictionaryEntry entry in _properties )
				result.SetProperty( entry.Key.ToString(), entry.Value );

			// Alow the client builder the ability to do some post-processing.
			AfterBuilding( result );

			// Don't rebuild, but return this built item after first build.
			_preBuiltResult = result;

			return result;
		}


		protected virtual T GetNewInstance()
		{
			if ( _triedToGetConstructor == false )
			{
				// Check for a parameterless constructor
				_typeConstructor = typeof ( T ).GetConstructor( Type.EmptyTypes );
				_triedToGetConstructor = true;
			}

			if ( _typeConstructor == null )
				throw new FluencyException( "Unable to invoke default constructor.  Override GetNewInstance() in builder class." );

			return _typeConstructor.Invoke( new object[] {} ) as T;
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
		protected virtual T BuildFrom( T values )
		{
			return GetNewInstance();
		}

		#endregion


		/// <summary>
		/// Gets the value the builder will create for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public TPropertyType GetValue< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression )
		{
			return (TPropertyType)_properties[propertyExpression.GetPropertyInfo().Name];
		}


		#region Property Value Setters

		/// <summary>
		/// Sets the value to be built for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="propertyValue">The property value.</param>
		/// <exception cref="FluencyException">Cannot set property once a pre built result has been given. Property change will have no affect.</exception>
		protected internal FluentBuilder< T > SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, TPropertyType propertyValue )
		{
			// If we try to change info after prebuilt result is set...throw error since the change wont be reflected in the prebuilt result.
			if ( _preBuiltResult != null )
				throw new FluencyException( "Cannot set property once a pre built result has been given. Property change will have no affect." );

			// Get the property to set
			var property = propertyExpression.GetPropertyInfo();

			RemoveBuilderFor( property );

			// Set the property.
			_properties[property.Name] = propertyValue;

			return this;
		}

        /// <summary>
        /// Mark a property to be ignored when setting of default values
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the property type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <exception cref="FluencyException"></exception>
	    protected internal FluentBuilder<T> IgnoreProperty<TPropertyType>(
	        Expression<Func<T, TPropertyType>> propertyExpression)
        {
            if (_preBuiltResult != null)
                throw new FluencyException("Cannot ignore property once a pre built result has been given. Property change will have no affect.");

            var property = propertyExpression.GetPropertyInfo();

            _properties.Remove(property.Name);
            return this;
        }

        /// <summary>
        /// Mark a all properties to be ignored when setting of default values
        /// </summary>
        /// <exception cref="FluencyException"></exception>
        protected internal FluentBuilder<T> IgnoreAllProperties()
        {
            if (_preBuiltResult != null)
                throw new FluencyException("Cannot ignore properties once a pre built result has been given. Property change will have no affect.");
            
            _properties.Clear();            
            return this;
        }

        /// <summary>
        /// Sets the builder to be used to construct the value for the specified propety.
        /// </summary>
        /// <typeparam name="TPropertyType">The type of the property type.</typeparam>
        /// <param name="propertyExpression">The property expression.</param>
        /// <param name="builder">The builder.</param>
        protected internal FluentBuilder< T > SetProperty< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder )
				where TPropertyType : class, new()
		{
			// Due to lack of polymorphism in generic parameters.
			if ( !( builder is FluentBuilder< TPropertyType > ) )
			{
				throw new ArgumentException(
						"Invalid builder type. Builder type must be a builder of Property Type. \n  BuilderType='{0}'\n  PropertyType='{1}'".format_using( builder.GetType().FullName,
						                                                                                                                                   typeof ( TPropertyType ).FullName ) );
			}

			AddBuilderFor( propertyExpression, builder );

			return this;
		}


		/// <summary>
		/// Sets the list builder for the specified property. Use this to assign a <see cref="FluentBuilder{IList{TPropertyType}}"/> to build the list for the property whose type is IList{PropertyType}.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		/// <exception cref="ArgumentException">Invalid builder type. Builder type must be a FluentListBuilder of the Property Type</exception>
		/// <exception cref="ArgumentException">PropertyType must derive from IList</exception>
		protected internal FluentBuilder< T > SetList< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder ) where TPropertyType : class
		{
			if ( !IsListType( typeof ( TPropertyType ) ) )
				throw new ArgumentException( "PropertyType must derive from IList" );

			// Due to lack of polymorphism in generic parameters.
			if ( !( builder is IFluentBuilder< TPropertyType > ) )
			{
				throw new ArgumentException(
						"Invalid builder type. Builder type must be a FluentListBuilder of Property Type. \n  BuilderType='{0}'\n  PropertyType='{1}'".format_using( builder.GetType().FullName,
						                                                                                                                                             typeof ( TPropertyType ).FullName ) );
			}

			AddBuilderFor( propertyExpression, builder );

			return this;
		}


		private static bool IsListType( Type type )
		{
			return type.FullName.Contains( "IList" );
		}


		/// <summary>
		/// Adds a new builder to a list of builders for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		[ Obsolete("This method is deprecated. Please use AddToList() instead.") ]
		protected internal FluentBuilder< T > AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, IFluentBuilder< TPropertyType > builder )
				where TPropertyType : class, new()
		{
			return AddToList( propertyExpression, builder );
		}


		/// <summary>
		/// Adds a new value to the list of values to be built for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="value">The value.</param>
		[ Obsolete("This method is deprecated. Please use AddToList() instead.") ]
		protected internal FluentBuilder< T > AddListItem< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, TPropertyType value )
				where TPropertyType : class, new()
		{
			return AddToList( propertyExpression, value );
		}


		/// <summary>
		/// Adds a new builder to a list of builders for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="builder">The builder.</param>
		protected internal FluentBuilder<T> AddToList<TPropertyType>(Expression<Func<T, IList<TPropertyType>>> propertyExpression, params IFluentBuilder<TPropertyType>[] builders)
				where TPropertyType : class, new()
		{
			var listBuilder = ListBuilderFor( propertyExpression );

			foreach (var builder in builders)
				listBuilder.Add(builder);

			return this;
		}


		/// <summary>
		/// Adds a one or more values to the list of values to be built for the specified list property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <param name="values">The value or values to add to the list.</param>
		protected internal FluentBuilder< T > AddToList< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression, params TPropertyType[] values )
				where TPropertyType : class, new()
		{
			var listBuilder = ListBuilderFor( propertyExpression );

			foreach (var value in values)
				listBuilder.Add( value );

			return this;
		}

		#endregion


		#region Builder Accessors

		/// <summary>
		/// Removes the builder for the specified property if it exists.
		/// </summary>
		/// <param name="property">The property.</param>
		private void RemoveBuilderFor( PropertyInfo property )
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
		private void AddBuilderFor< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression, IFluentBuilder builder )
		{
			var property = propertyExpression.GetPropertyInfo();
			AddBuilderFor( property, builder );
		}


		/// <summary>
		/// Adds the specified builder for the specified property...removing any builder that may have already been assigend.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <param name="builder">The builder.</param>
		private void AddBuilderFor( PropertyInfo property, IFluentBuilder builder )
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
		public IFluentListBuilder< TPropertyType > ListBuilderFor< TPropertyType >( Expression< Func< T, IList< TPropertyType > > > propertyExpression )
				where TPropertyType : class, new()
		{
			var property = propertyExpression.GetPropertyInfo();

			if ( !_builders.ContainsKey( property.Name ) )
				AddBuilderFor( property, new FluentListBuilder< TPropertyType >() );

			return (IFluentListBuilder< TPropertyType >)_builders[property.Name];
		}


		/// <summary>
		/// Gets the builder for the specified property.
		/// </summary>
		/// <typeparam name="TPropertyType">The type of the property type.</typeparam>
		/// <param name="propertyExpression">The property expression.</param>
		/// <returns></returns>
		public FluentBuilder< TPropertyType > BuilderFor< TPropertyType >( Expression< Func< T, TPropertyType > > propertyExpression ) where TPropertyType : class, new()
		{
			var property = propertyExpression.GetPropertyInfo();

			if ( !_builders.ContainsKey( property.Name ) )
				throw new ArgumentException( "Builder does not exist for property [" + property.Name + "]" );

			return (FluentBuilder< TPropertyType >)_builders[property.Name];
		}

		#endregion


		/// <summary>
		/// Casts this builder to the specified builder type.
		/// </summary>
		/// <typeparam name="TBuilderType">The type of the builder.</typeparam>
		/// <returns></returns>
		public TBuilderType As< TBuilderType >() where TBuilderType : FluentBuilder< T >
		{
			return (TBuilderType)this;
		}


		/// <summary>
		/// Performs an implicit conversion from <see cref="FluentBuilder&lt;T&gt;"/> to <see cref="T"/>.
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
		private object GetDefaultValue( PropertyInfo propertyInfo )
		{
			// Check each of the conventions and apply them if necessary.
			foreach ( var defaultConvention in _defaultConventions )
			{
				// first convention match wins...
				if ( defaultConvention.AppliesTo( propertyInfo ) )
					return defaultConvention.DefaultValue( propertyInfo );
			}

			// Returns null if no convention matched.
			return null;
		}

		#endregion
	}
}