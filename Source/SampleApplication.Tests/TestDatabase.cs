using System;
using System.Collections.Generic;
using NHibernate;
using SampleApplication.Domain;
using SampleApplication.Tests.Utils;


namespace SampleApplication.Tests
{
	public class TestDatabase
	{
		private readonly IList< Project > _projects = new List< Project >();
		private readonly IList<Employee> _employees = new List<Employee>();
		private readonly DbHelper _dbHelper;
		private readonly ISession _session;


		public TestDatabase( ISession session )
		{
			_session = session;
			_dbHelper = new DbHelper( session );
		}


		public TestDatabase Then
		{
			get { return this; }
		}


		public void Persist()
		{
			// Insert data in order of dependency.

			foreach ( Project project in _projects )
				_dbHelper.Insert( project );

			foreach ( Employee employee in _employees )
				_dbHelper.Insert( employee );
		}


		public TestDatabase Add( Project project )
		{
			// Exit if null or if this has already been added.
			if (_projects.AddIfUnique(project, x => x.Id == project.Id))
			{
				// Add parents.
				// Add children.
			}
			return this;
		}




		public TestDatabase Add( Employee employee )
		{
			// Exit if null or if this has already been added.
			if (_employees.AddIfUnique(employee, x => x.Id == employee.Id))
			{
				// Add parents.
				// Add children.
			}
			return this;
		}
	}
}