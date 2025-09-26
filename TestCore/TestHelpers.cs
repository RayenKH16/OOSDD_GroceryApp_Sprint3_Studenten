using Grocery.Core.Helpers;
namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        {
        }

        // Correcte wachtwoorden
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        //Unhappy flow - Verkeerde wachtwoorden
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {
            // Test met verkeerd wachtwoord
            string correctPassword = "user3";
            string wrongPassword = "wrongpassword";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";

            // Het verkeerde wachtwoord zou false moeten returnen
            Assert.IsFalse(PasswordHelper.VerifyPassword(wrongPassword, passwordHash));
        }

        // Deze TestCases zijn EXPRES fout gemaakt (verkeerde hashes zonder '=' aan het eind)
        // Om te testen dat PasswordHelper correct omgaat met ongeldige hashes
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            // Deze test verwacht dat de verificatie faalt vanwege ongeldige hash formatting
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        // Extra edge case tests voor betere test coverage
        [Test]
        public void TestPasswordHelper_NullPassword_ReturnsFalse()
        {
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword(null, passwordHash));
        }

        [Test]
        public void TestPasswordHelper_EmptyPassword_ReturnsFalse()
        {
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword("", passwordHash));
        }

        [Test]
        public void TestPasswordHelper_NullHash_ReturnsFalse()
        {
            Assert.IsFalse(PasswordHelper.VerifyPassword("user3", null));
        }

        [Test]
        public void TestPasswordHelper_EmptyHash_ReturnsFalse()
        {
            Assert.IsFalse(PasswordHelper.VerifyPassword("user3", ""));
        }

        [Test]
        public void TestPasswordHelper_InvalidHashFormat_ReturnsFalse()
        {
            // Hash zonder punt (.) separator
            Assert.IsFalse(PasswordHelper.VerifyPassword("user3", "invalidhashwithoutdotseparator"));
        }

        [TestCase("user1", "wronghash")]
        [TestCase("user2", "anotherwronghash")]
        [TestCase("user3", "completelywrongformat")]
        public void TestPasswordHelper_WrongPasswords_ReturnsFalse(string password, string wrongHash)
        {
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, wrongHash));
        }
    }
}