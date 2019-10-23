using Initial.Api.Models.Attributes;
using NUnit.Framework;

namespace Initial.Api.Tests.Models.Attributes
{
    [TestFixture]
    public class PasswordStrengthAttributeTest
    {
        [TestCase(null, PasswordStrengthLevel.Blank)]
        [TestCase("asdfg", PasswordStrengthLevel.Blank)]
        [TestCase("12345", PasswordStrengthLevel.VeryWeak)]
        [TestCase("1Sdfg", PasswordStrengthLevel.Weak)]
        [TestCase("1S_fg", PasswordStrengthLevel.Medium)]
        [TestCase("1S_fghjkl", PasswordStrengthLevel.Strong)]
        public void IsValid(string password, PasswordStrengthLevel level)
        {
            // Arrange

            var attr = new PasswordStrengthAttribute(level);

            // Act

            var isValid = attr.IsValid(password);

            // Assert

            Assert.IsTrue(isValid);
        }

        [TestCase("asdfg", PasswordStrengthLevel.VeryWeak)]
        [TestCase("12345", PasswordStrengthLevel.Weak)]
        [TestCase("1Sdfg", PasswordStrengthLevel.Medium)]
        [TestCase("1S_fg", PasswordStrengthLevel.Strong)]
        public void IsNotValid(string password, PasswordStrengthLevel level)
        {
            // Arrange

            var attr = new PasswordStrengthAttribute(level);

            // Act

            var isValid = attr.IsValid(password);

            // Assert

            Assert.IsFalse(isValid);
        }
    }
}
