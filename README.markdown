Fluency.NET
===========
Fluency is a framework for building fluent interfaces or DSLs in .NET. It simplifies building fluent interfaces for configuration, but also has facilities for creating interfaces to build anonymous (random) test data for integration tests.

Installing Fluency
------------------
You can install Fluency either by:

1. Using the NuGet package manager for Visual Studio typing `Install-Package Fluency` at the package manager console or searching for "Fluency" in the UI, or
2. Downloading the latest binaries from the [Fluency GitHub repository](http://github.com/ChrisEdwards/Fluency) by clicking the "Downloads" button.

Two types of builders
---------------------
There are two ways to use Fluency.

1. Use the standard dynamic builder by creating of instance of `DynamicFluentBuilder<T>` where `T` is the target type to build.
2. Use a custom builder by subclassing `FluentBuilder<T>` and creating the methods/properties you want in your fluent interface.


Let's say you want a fluent interface to build the follwing configuration class.

```CSharp
	public class MyConfiguration
	{
	    public string UserName { get; set; }
	    public bool AllowLogin { get; set; }
	    public int TimeoutMinutes { get; set; }
	}
```
	
###Using the Dynamic Builder

This is the simplest way to use Fluency. You simply (1) instantiate `DynamicFluentBuilder<T>` for your type, then (2) call the lambda-based config methods to set the values, and finally (3) call `build()` to build the resulting config.

```CSharp
	MyConfiguration config = 
	    new DynamicFluentBuilder<MyConfiguration>()
	        .For( x => x.UserName, "Bob" )
	        .With( x => x.AllowLogin, true )
	        .Having( x => x.TimeoutMinutes, 5 )
	        .build();
```
			
###Using a Custom Builder

Using a custom builder gives you full control of the fluent interface. To create a custom builder, you need to subclass `FluentBuilder<T>` and implement your fluent interface.

```CSharp
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
```

And here is how you would use the builder.

```CSharp
	MyConfiguration config =
		new MyConfigurationBuilder()
			.ForUser( "Bob" )
			.AllowedToLogin()
			.MinutesBeforeTimeout( 20 )
			.build();
```

While the custom builder requires more work to create, it gives you much more control over the interface and doesn't require the user to know the structure of the class being built.


Cleaning up the Syntax
----------------------
I don't know about you, but the "new" keyword and the builder's class name really muddle the code up. If I am creating these objects directly, I often create a helper class to create the builders.

```CSharp
	public static class a
	{
		public MyConfigurationBuilder Config 
		{
			get { return new MyConfigurationBuilder(); }
		}
	}
```

Now my syntax to create the builder looks like this	

```CSharp
	var config = a.Config
					.ForUser( "Bob" )
					.AllowedToLogin()
					.MinutesBeforeTimeout( 20 )
					.build();
```

I wanted to introduce this since I will be using this syntax going forward to simplify the code.


Nesting Builders
----------------
Nesting builders greatly simplifies building object graphs and creating a more expressive fluent interface. 


###By Passing a Builder Parameter

Here's an example that just passes a builder for the nested object.

```CSharp
	var config = a.Config
					.ForUser( a.User.FirstName( "Bob" )
									.LastName( "Smith " )
									.UserName( "bsmith" ) 
							)
					.MinutesBeforeTimeout( 20 )
					.build();
```

To do this, MyConfiguration would need a User property, we would create a`UserBuilder`. Then we modify the `MyConfigurationBuilder.ForUser()` method to accept a parameter of type `UserBuilder`:

```CSharp
	public MyConfigurationBuilder ForUser( UserBuilder userBuilder )
	{
		SetProperty( x => x.User, userBuilder );
		return this;
	}
```	
	
When using the `SetProperty()` method for reference types, it will accept either an instance of the type (i.e. `SetProperty( x => x.User, new User() )`), or a builder for that type (as we see above). If a builder is passed in, it will be built whenever `build()` is called on this builder. 


###By Exposing a Builder Through an Action Parameter

An alternative approach would be to accept an action as the parameter for the `ForUser()` method. This gives you the following.

```CSharp
	var config = a.Config
					.ForUser( u => u.FirstName( "Bob" )
									.LastName( "Smith " )
									.UserName( "bsmith" ) 
							)
					.MinutesBeforeTimeout( 20 )
					.build();
```

In this case, the `ForUser()` method would look like so:

```CSharp
	public MyConfigurationBuilder ForUser( Action< UserBuilder > userBuilderAction )
	{
		var userBuilder = new UserBuilder();
		userBuilderAction( userBuilder );
		SetProperty( x => x.User, userBuilder );
		return this;
	}
```
	
Setting Default Values
----------------------
Fluency allows you to configure the default values that we be built for each property if no value is provided through the fluent interface. To do this, simply set those values using `SetProperty()` in the constructor of your builder.

```CSharp
	public class MyConfigurationBuilder : FluentBuilder< MyConfiguration >
	{
		public MyConfigurationBuilder()
		{
			// Setup defaults.
			SetProperty( x => x.AllowLogin, false );
			SetProperty( x => x.TimeoutMinutes, 5 );
		}
	}
```

Your default values can also include defaults for nested builders as well, which would ensure that sub-objects get built even if the user didn't specify values for it while calling the fluent interface. This turns out to be a very powerful feature when you are tyring to build test data for integration tests.


Building Anonymous Objects for Testing
--------------------------------------
Anonymous objects are simply objects with random (but valid) data. Fluency contains an extensive random value generator for all types of data. Check out this example.

```CSharp
	public class UserBuilder : FluentBuilder< User >
	{
		public UserBuilder()
		{
			SetProperty( x => x.FirstName, ARandom.FirstName() );
			SetProperty( x => x.LastName, ARandom.LastName() );
			SetProperty( x => x.Phone, ARandom.StringPattern( "(999) 999-9999" );
			SetProperty( x => x.City, ARandom.City() );
			SetProperty( x => x.State, ARandom.State() );
			SetProperty( x => x.Zip, ARandom.ZipCode() );
			
			var birthDate = ARandom.BirthDate();
			SetProperty( x => x.BirthDate, birthDate );
			SetProperty( x => x.FirstLoginDate, ARandom.DateTimeInPastSince( birthDate ) );			
		}
	}
```

If I added this builder to the `a` static class I created above, the syntax to create a valid anonymous user would be:

```CSharp
	var user = a.User.build();
```

When you add the ability to nest objects, you can see how it would be easy to generate a large object graph of test data very quickly. This greatly increases the readability of your tests because you don't see all the unnecessary details about the random data in your test...you only see the values that directly affect your test. For instance:

```CSharp
	[Test]
	public void A_user_under_18_should_not_be_allowed_access()
	{
		var service = new SomeAuthenticationServiceWeAreTesting();
		
		var userUnder18 = a.User.WhoseAgeIs( 5 ).build();
		
		Assert.That( service.AuthenticateUser( userUnder18 ), Is.False() );
	}
```

This assumes we add the method `WhoseAgeIs()` to the `UserBuilder` like so...

```CSharp
	public UserBuilder WhoseAgeIs( int age )
	{
		SetProperty( x => x.BirthDate, age.YearsAgo() ); // YearsAgo is an exension method on int.
		return this;
	}
```

I hope this gives you a little taste as to what Fluency can do. I have been using it over a year and have found it invaluable in my unit and integration testing.