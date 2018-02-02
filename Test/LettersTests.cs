using System.Net;
using System.Threading.Tasks;
using Lob;
using Lob.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class LettersTests
    {
        private static readonly Credentials TestCredentials = new Credentials("test_fd34e1b5ea86a597ec89f7f2e46940c874d");
        private static readonly LobClient LobClient = new LobClient("test-app", new CredentialStore(TestCredentials));

        [TestMethod]
        public void TestCreate()
        {
            object fromAddress = new {
                name = "Test Sender",
                address_line1 = "185 Berry st",
                address_city = "San Francisco",
                address_state = "CA",
                address_zip = "94107",
                address_country = "US"
            };

            object toAddress = new {
                name = "Test Recipient",
                address_line1 = "185 Berry st",
                address_city = "San Francisco",
                address_state = "CA",
                address_zip = "94107",
                address_country = "US"
            };

            NewLetter letter = new NewLetter(
                description: "test letter",
                to: toAddress,
                from: fromAddress,
                file: "url"
            );

            LobClient.Letter.Create(letter);
        }

        [TestMethod]
        public async Task TestEmptyToAddress()
        {
            object fromAddress = new
            {
                name = "Test Sender",
                address_line1 = "185 Berry st",
                address_city = "San Francisco",
                address_state = "CA",
                address_zip = "94107",
                address_country = "US"
            };

            object toAddress = new
            {
                name = "Test Recipient",
                address_line1 = "",
                address_city = "San Francisco",
                address_state = "CA",
                address_zip = "94107",
                address_country = "US"
            };

            NewLetter letter = new NewLetter(
                description: "test letter",
                to: toAddress,
                from: fromAddress,
                file: "<!doctype html><meta charset=utf-8><body>Test</body></html>"
            );

            var exception = await Assert.ThrowsExceptionAsync<LobException>(() => LobClient.Letter.Create(letter));
            Assert.AreEqual(exception.HttpStatusCode, (HttpStatusCode) 422);
            StringAssert.Contains(exception.Message, "to.address_line1 is required");
        }
    }
}
