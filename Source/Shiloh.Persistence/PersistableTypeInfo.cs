using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace Shiloh.Persistence
{
	/// <summary>
	/// Information about an entity...its comparator, dependencies, etc.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PersistableTypeInfo< T > : IPersistableTypeInfo where T : class
	{
		/// <summary>
		/// List of lambda expressions that point to the properties on this entity referring to associated entities that should also be persisted when this one is.
		/// </summary>
		/// <value>The dependency expressions.</value>
		public IList< Expression< Func< T, object > > > AssociationExpressions { get; set; }

		/// <summary>
		/// Action delegate that can be used to persist this entity to the database.
		/// </summary>
		/// <value>The persist action.</value>
		public Action< T > PersistAction { get; set; }

		/// <summary>
		/// Comparator to determine if two entities are the same item (by identity...or database primary key)
		/// </summary>
		/// <value>The identity comparator.</value>
		public Func< T, T, bool > IdentityComparator { get; set; }

		public string TableName { get; set; }
	}
}