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


namespace Shiloh.Persistence
{
	/// <summary>
	/// Marker Interface to work around .NET strong typing issues.
	/// </summary>
	public interface IInstanceQueueBuilder
	{
		/// <summary>
		/// Gets the entity type (.NET type) this PersistableTypeInfoBuilder refers to.
		/// </summary>
		/// <returns></returns>
		Type GetInstanceType();


		/// <summary>
		/// Builds an InstnceQueue strongly typed against the persistable type.
		/// </summary>
		/// <returns></returns>
		IInstanceQueue build();
	}
}