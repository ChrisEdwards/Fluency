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

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Fluency")]
[assembly: AssemblyDescription("Fluency is a factory that creates object graphs populated with fake data. Fluency exposes a customizeable fluent interface that constructs test objects (including the full graph of dependencies). Yet it only requires you to specify the few things your test is actually concerned with--everything else is automatically populated with random (yet valid) data. Fluency comes complete with a very functional set of realistic random data generators that can be configured on a case-by-case basis or by convention to give you complete control over the randomness of your data.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Chris Edwards")]
[assembly: AssemblyProduct("Fluency")]
[assembly: AssemblyCopyright("Copyright ©2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Short Description: Fluency simplifies testing by creating anonymous test objects and their dependencies from a simple fluent specification that only includes the few things your test is actually interested in. Everything else is random (yet valid) based on your conventions.


// The following GUID is for the ID of the typelib if this project is exposed to COM

[assembly: Guid("8ea43609-1872-4ad5-a296-c990239a9bf8")]
[assembly: InternalsVisibleTo("Fluency.Tests.Deprecated")]
[assembly: InternalsVisibleTo("Fluency.Tests")]



[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]