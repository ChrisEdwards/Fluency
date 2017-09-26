// ReSharper disable InconsistentNaming


using System;
using Fluency.DataGeneration;
using Machine.Specifications;

namespace Fluency.Tests.Deprecated.Conventions
{
	public class DefaultConventionsSpecs
	{
		[ Subject( typeof ( FluentBuilder< > ) ) ]
		public class When_building_an_object_and_no_conventions_were_specified_for_fluency
		{
			private const string EmailAddressRegex = @"^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$";
			private const string ZipCodeRegex = @"\d\d\d\d\d";
			private const string SimpleAddressRegex = @"^\d+\w+[a-zA-Z0-9\.].*$";
			private const string PhoneNumberRegex = @"^(((\(\d{3}\)|\d{3})( |-|\.))|(\(\d{3}\)|\d{3}))?\d{3}( |-|\.)?\d{4}(( |-|\.)?([Ee]xt|[Xx])[.]?( |-|\.)?\d{4})?$";

			private Establish context = () => builder = new FluentBuilder< ConventionsTestClass >();

			private Because of = () => result = builder.build();

			private It should_use_default_first_name_convention = () => RandomData.FirstNames.ShouldContain( result.FirstName );
			private It should_use_default_last_name_convention = () => RandomData.LastNames.ShouldContain( result.LastName );

			private It should_use_default_full_name_convention = () =>
			                                                     	{
			                                                     		var names = result.FullName.Split( ' ' );
			                                                     		names.Length.ShouldEqual( 2 );
			                                                     		RandomData.FirstNames.ShouldContain( names[0] );
			                                                     		RandomData.LastNames.ShouldContain( names[1] );
			                                                     	};

			private It should_use_default_city_convention = () => RandomData.Cities.ShouldContain( result.City );
			private It should_use_default_state_convention = () => RandomData.StateCodes.ShouldContain( result.State );

			private It should_use_default_zip_convention = () =>
			                                               	{
			                                               		result.Zip.ShouldMatch( ZipCodeRegex );
			                                               		result.ZipCode.ShouldMatch( ZipCodeRegex );
			                                               		result.PostalCode.ShouldMatch( ZipCodeRegex );
			                                               	};

			private It should_use_default_email_convention = () => result.Email.ShouldMatch( EmailAddressRegex );
			private It should_use_default_address_convention = () => result.Address.ShouldMatch( SimpleAddressRegex );
			private It should_use_default_phone_convention = () => result.Phone.ShouldMatch( PhoneNumberRegex );
			private It should_use_default_home_phone_convention = () => result.HomePhone.ShouldMatch( PhoneNumberRegex );
			private It should_use_default_business_phone_convention = () => result.BusinessPhone.ShouldMatch( PhoneNumberRegex );
			private It should_use_default_work_phone_convention = () => result.WorkPhone.ShouldMatch( PhoneNumberRegex );
			private It should_use_default_fax_convention = () => result.Fax.ShouldMatch( PhoneNumberRegex );
			private It should_use_default_string_convention = () => result.StringProperty.ShouldNotBeEmpty();
			private It should_use_default_birth_date_convention = () => result.BirthDate.ShouldBeGreaterThan(DateTime.Now.AddYears( -100 ));
			private It should_use_default_date_convention = () => result.DateProperty.ShouldNotBeNull();
			private It should_use_default_integer_convention = () => result.IntegerProperty.ShouldNotEqual( 0 );
			private It should_use_default_decimal_convention = () => result.DecimalProperty.ShouldNotEqual( 0 );



			private static FluentBuilder< ConventionsTestClass > builder;
			private static ConventionsTestClass result;
		}
	}


	internal class ConventionsTestClass
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FullName { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public string ZipCode { get; set; }
		public string PostalCode { get; set; }
		public string Email { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string HomePhone { get; set; }
		public string BusinessPhone { get; set; }
		public string Fax { get; set; }
		public string StringProperty { get; set; }
		public DateTime BirthDate { get; set; }
		public DateTime DateProperty { get; set; }
		public int IntegerProperty { get; set; }
		public decimal DecimalProperty { get; set; }

		public string WorkPhone { get; set; }
	}
}


// ReSharper restore InconsistentNaming