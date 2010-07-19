using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Shiloh.Persistence
{
	/// <summary>
	/// Fluent interface to build PersistableTypeInfo objects.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PersistableTypeInfoBuilder< T > : TestDataBuilder< PersistableTypeInfo< T > > where T : class
	{
		Func< T, T, bool > _comparator;
		Expression< Func< T, object > >[] _pesistableAssociations = new Expression< Func< T, object > >[] {};
		Action< T > _persistAction;
		string _tableName;


		#region Fluent Methods

		/// <summary>
		/// Sets the comparator function used to determine if two entities are the same item (by identity...or database primary key)
		/// </summary>
		/// <param name="comparator">The comparator.</param>
		/// <returns></returns>
		public PersistableTypeInfoBuilder< T > MatchIdentityUsing( Func< T, T, bool > comparator )
		{
			_comparator = comparator;
			return this;
		}


		/// <summary>
		/// Sets the list of lambda expressions that point to the properties on this entity referring to associated entities that should also be persisted when this one is.
		/// </summary>
		/// <param name="persistableAssociations">The dependency expressions.</param>
		/// <returns></returns>
		public PersistableTypeInfoBuilder< T > PersistableAssociations( params Expression< Func< T, object > >[] persistableAssociations )
		{
			_pesistableAssociations = persistableAssociations;
			return this;
		}


		/// <summary>
		/// Sets the Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <param name="persistAction">The persist action.</param>
		/// <returns></returns>
		public PersistableTypeInfoBuilder< T > PersistUsing( Action< T > persistAction )
		{
			_persistAction = persistAction;
			return this;
		}


		/// <summary>
		/// Sets the Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <param name="tableName">Name of the table.</param>
		/// <returns></returns>
		public PersistableTypeInfoBuilder< T > TableName( string tableName )
		{
			_tableName = tableName;
			return this;
		}

		#endregion


		/// <summary>
		/// Builds an object of type T based on the specs specified in the builder.
		/// </summary>
		/// <returns></returns>
		protected override PersistableTypeInfo< T > _build()
		{
			return new PersistableTypeInfo< T >
			       	{
			       			IdentityComparator = _comparator,
			       			AssociationExpressions = new List< Expression< Func< T, object > > >( _pesistableAssociations ),
			       			PersistAction = _persistAction,
			       			TableName = _tableName
			       	};
		}
	}
}