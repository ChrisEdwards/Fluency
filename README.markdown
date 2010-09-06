Fluency.NET
===========
Fluency is a framework for building fluent interfaces or DSLs in .NET. It simplifies building fluent interfaces for configuration, but also has facilities for creating interfaces to build anonymous (random) test data for integration tests.

There are two ways to use Fluency.

1. Use the standard dynamic builder by creating of instance of DynamicFluentBuilder<T> where T is the target type to build.
2. Use a custom builder by subclassing FluentBuilder<T> and creating the methods/properties you want in your fluent interface.

Using the Dynamic Builder
-------------------------
The simplest way to use Fluency it to use the dynamic builder.

###Target Class
	public class MyConfiguration
	{
	    public string UserName { get; set; }
	    public bool AllowLogin { get; set; }
	    public int TimeoutMinutes { get; set; }
	}

###Code to use the builder
	MyConfiguration config = 
	    new DynamicFluentBuilder<MyConfiguration>()
	        .For( x => x.UserName, "Bob" )
	        .With( x => x.AllowLogin, true )
	        .Having( x => x.TimeoutMinutes, 5 )
	        .build();
	
