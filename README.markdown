Fluency.NET
===========
Fluency is a framework for building fluent interfaces or DSLs in .NET. It simplifies building fluent interfaces for configuration, but also has facilities for creating interfaces to build anonymous (random) test data for integration tests.

There are two ways to use Fluency.

1. Use the standard dynamic builder by creating of instance of DynamicFluentBuilder<T> where T is the target type to build.
2. Use a custom builder by subclassing FluentBuilder<T> and creating the methods/properties you want in your fluent interface.

Examples
--------
Let's say you want a fluent interface to build the follwing configuration class.

	public class MyConfiguration
	{
	    public string UserName { get; set; }
	    public bool AllowLogin { get; set; }
	    public int TimeoutMinutes { get; set; }
	}

###Using the dynamic builder
This is the simplest way to use Fluency. You simply (1) instantiate `DynamicFluentBuilder<T>` for your type, then (2) call the lambda-based config methods to set the values, and finally (3) call build() to build the resulting config.
	MyConfiguration config = 
	    new DynamicFluentBuilder<MyConfiguration>()
	        .For( x => x.UserName, "Bob" )
	        .With( x => x.AllowLogin, true )
	        .Having( x => x.TimeoutMinutes, 5 )
	        .build();

###Using a custom builder
Using a custom builder gives you full control of the fluent interface. To create a custom builder, you need to subclass `FluentBuilder<T>` and implement your fluent interface.
	public class MyConfigurationBuilder : FluentBuilder< MyConfiguration >
	{
	    public MyConfigurationBuilder ForUser( string userName )
		{
			SetProperty( x => x.UserName, userName );
			return this;
		}
		
		public MyConfigurationBuilder IsAllowedToLogin()
		{
			SetProperty( x => x.AllowLogin, true );
			return this;
		}
		
		public MyConfigurationBuilder IsNotAllowedToLogin()
		{
			SetProperty( x => x.AllowLogin, false );
			return this;
		}
		
		public MyConfigurationBuilder MinutesBeforeTimeout( int timeoutMinutes )
		{
			SetProperty( x => x.TimeoutMinutes, timeoutMinutes );
			return this;
		}
	}

And here is how you would use the builder.
	MyConfiguration config =
		new MyConfigurationBuilder()
			.ForUser( "Bob" )
			.AllowedToLogin()
			.MinutesBeforeTimeout( 20 )
			.build();

While the custom builder requires more work to create, it gives you much more control over the interface. It also doesn't require the user of the interface to know the structure of the class being built (because we aren't using lambdas).
	