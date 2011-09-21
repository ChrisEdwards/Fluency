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
using System.Collections.Generic;
using System.Linq;


namespace Shiloh.DataGeneration
{
	public class AnonymousValue
	{
		public T From< T >( IList< T > items )
		{
			return items[Anonymous.Int.Between( 0, items.Count - 1 )];
		}


		public T From< T >( IEnumerable< T > items )
		{
			int randomItemPosition = Anonymous.Int.Between(0, items.Count() - 1 );
			return items.Skip( randomItemPosition - 1 ).First();
		}


		public T From< T >( params T[] items )
		{
			return From< T >( new List< T >( items ) );
		}
	}
}