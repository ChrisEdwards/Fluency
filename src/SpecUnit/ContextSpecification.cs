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
using System.Diagnostics;
using NUnit.Framework;


namespace SpecUnit
{
	public abstract class ContextSpecification
	{
		[ TestFixtureSetUp ]
		public virtual void TestFixtureSetUp()
		{
			Context_BeforeAllSpecs();
		}


		[ TestFixtureTearDown ]
		public void TestFixtureTearDown()
		{
			CleanUpContext_AfterAllSpecs();
		}


		[ SetUp ]
		public virtual void SetUp()
		{
			SharedContext();
			Context();
			Because();
		}


		[ TearDown ]
		public virtual void TearDown()
		{
			Because_After();
			CleanUpContext();
		}


		protected void Pending()
		{
			Assert.Ignore();
		}


		protected void Pending( string message )
		{
			Assert.Ignore( message );
		}


		protected virtual void SharedContext()
		{
			Debug.WriteLine( "WARNING: Shared context setup not implemented" );
		}


		protected virtual void Context() {}
		protected virtual void CleanUpContext() {}
		protected virtual void Context_BeforeAllSpecs() {}
		protected virtual void CleanUpContext_AfterAllSpecs() {}
		protected virtual void Because() {}
		protected virtual void Because_After() {}
	}
}