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


namespace Fluency
{
	public static class Fluency
	{
		public static FluencyConfiguration Configuration = new FluencyConfiguration();


		/// <summary>
		/// Initializes Fluency with the specified configuration.
		/// </summary>
		/// <param name="configurationAction">The configuration action.</param>
		public static void Initialize( Action< InitializationExpression > configurationAction )
		{
			lock ( typeof ( Fluency ) )
			{
				// Execute the user-defined configuration code against the initialization expression and get the configuration data from it.
				var expression = new InitializationExpression();
				configurationAction( expression );

				Configuration = expression.GetConfiguration();
			}
		}
	}
}