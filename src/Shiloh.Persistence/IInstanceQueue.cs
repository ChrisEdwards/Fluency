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
namespace Shiloh.Persistence
{
	/// <summary>
	/// Non-Generic interface for an InstanceQueue.
	/// </summary>
	public interface IInstanceQueue
	{
		/// <summary>
		/// The parent TypeQueue this InstanceQueue is a member of.
		/// </summary>
		/// <value>The type queue.</value>
		TypeQueue ParentQueue { get; set; }


		/// <summary>
		/// Persists the instances to the database using the defined PersistAction.
		/// </summary>
		void Persist();


		/// <summary>
		/// Adds the instance from the non-strongly typed interface...thus, we need to cast..
		/// This property is only visible if you are explicitly looking at the object as an IInstanceQueue.
		/// </summary>
		/// <param name="item">The instance item to add.</param>
		void AddInstance( object item );
	}
}