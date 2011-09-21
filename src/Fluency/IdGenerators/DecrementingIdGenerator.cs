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
namespace Fluency.IdGenerators
{
	/// <summary>
	/// Generates Id values starting at a specific number, decrementing subsequent ids by 1.
	/// </summary>
	public class DecrementingIdGenerator : IIdGenerator
	{
		int _id = -1;


		/// <summary>
		/// Initializes a new instance of the <see cref="DecrementingIdGenerator"/> class. By default, the first Id value that will be used is -1.
		/// </summary>
		public DecrementingIdGenerator() {}


		/// <summary>
		/// Initializes a new instance of the <see cref="DecrementingIdGenerator"/> class providing the first value to use as an Id.
		/// </summary>
		/// <param name="startingValue">The first Id value that will be used.</param>
		public DecrementingIdGenerator( int startingValue )
		{
			_id = startingValue;
		}


		#region IIdGenerator Members

		/// <summary>
		/// Gets the next Id.
		/// </summary>
		/// <returns></returns>
		public int GetNextId()
		{
			return _id--;
		}

		#endregion
	}
}