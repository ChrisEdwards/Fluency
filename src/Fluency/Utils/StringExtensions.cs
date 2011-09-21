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
namespace Fluency.Utils
{
	public static class StringExtensions
	{
		/// <summary>
		/// Extension for string.Format, since I always forget to type format till the end of the string.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="args">The args.</param>
		/// <returns></returns>
		public static string format_using( this string format, params object[] args )
		{
			return string.Format( format, args );
		}
	}
}