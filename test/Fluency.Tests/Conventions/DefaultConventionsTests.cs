using System;
using Fluency.DataGeneration;
using FluentAssertions;
using Xunit;

namespace Fluency.Tests.Conventions
{
    public class DefaultConventionsTests
    {
        public class When_building_an_object_and_no_conventions_were_specified_for_fluency
        {
            private const string EmailAddressRegex = @"^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$";
            private const string ZipCodeRegex = @"\d\d\d\d\d";
            private const string SimpleAddressRegex = @"^\d+\w+[a-zA-Z0-9\.].*$";
            private const string PhoneNumberRegex = @"^(((\(\d{3}\)|\d{3})( |-|\.))|(\(\d{3}\)|\d{3}))?\d{3}( |-|\.)?\d{4}(( |-|\.)?([Ee]xt|[Xx])[.]?( |-|\.)?\d{4})?$";

            private FluentBuilder<ConventionsTestClass> builder;
            private readonly ConventionsTestClass result;

            public When_building_an_object_and_no_conventions_were_specified_for_fluency()
            {
                builder = new FluentBuilder<ConventionsTestClass>();
                result = builder.build();
            }

            [Fact]
            public void should_use_default_first_name_convention()
            {
                result.FirstName.Should().BeOneOf(RandomData.FirstNames);
            }

            [Fact]
            public void should_use_default_last_name_convention()
            {
                result.LastName.Should().BeOneOf(RandomData.LastNames);
            }

            [Fact]
            public void should_use_default_full_name_convention()
            {
                var names = result.FullName.Split(' ');
                names.Length.Should().Be(2);
                names[0].Should().BeOneOf(RandomData.FirstNames);
                names[1].Should().BeOneOf(RandomData.LastNames);
            }

            [Fact]
            public void should_use_default_city_convention()
            {
                result.City.Should().BeOneOf(RandomData.Cities);
            }

            [Fact]
            public void should_use_default_state_convention()
            {
                result.State.Should().BeOneOf(RandomData.StateCodes);
            }

            [Fact]
            public void should_use_default_zip_convention()
            {
                result.Zip.Should().MatchRegex(ZipCodeRegex);
                result.ZipCode.Should().MatchRegex(ZipCodeRegex);
                result.PostalCode.Should().MatchRegex(ZipCodeRegex);
            }

            [Fact]
            public void should_use_default_email_convention()
            {
                result.Email.Should().MatchRegex(EmailAddressRegex);
            }

            [Fact]
            public void should_use_default_address_convention()
            {
                result.Address.Should().MatchRegex(SimpleAddressRegex);
            }

            [Fact]
            public void should_use_default_phone_convention()
            {
                result.Phone.Should().MatchRegex(PhoneNumberRegex);
            }

            [Fact]
            public void should_use_default_home_phone_convention()
            {
                result.HomePhone.Should().MatchRegex(PhoneNumberRegex);
            }

            [Fact]
            public void should_use_default_business_phone_convention()
            {
                result.HomePhone.Should().MatchRegex(PhoneNumberRegex);
            }

            [Fact]
            public void should_use_default_work_phone_convention()
            {
                result.WorkPhone.Should().MatchRegex(PhoneNumberRegex);
            }

            [Fact]
            public void should_use_default_fax_convention()
            {
                result.Fax.Should().MatchRegex(PhoneNumberRegex);
            }

            [Fact]
            public void should_use_default_string_convention()
            {
                result.StringProperty.Should().NotBeEmpty();
            }

            [Fact]
            public void should_use_default_birth_date_convention()
            {
                result.BirthDate.Should().BeAfter(DateTime.Now.AddYears(-100));
            }

            [Fact]
            public void should_use_default_date_convention()
            {
                result.DateProperty.Should().NotBe(default(DateTime));
            }

            [Fact]
            public void should_use_default_integer_convention()
            {
                result.IntegerProperty.Should().NotBe(0);
            }

            [Fact]
            public void should_use_default_decimal_convention()
            {
                result.DecimalProperty.Should().NotBe(0);
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
}