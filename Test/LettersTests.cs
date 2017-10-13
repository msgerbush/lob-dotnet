using Lob;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LobTests
{
    [TestClass]
    public class LettersTests
    {
        [TestMethod]
        public void TestCreate()
        {
            Credentials testCredentials = new Credentials("test_fd34e1b5ea86a597ec89f7f2e46940c874d");
            var lobClient = new LobClient("test-app", new CredentialStore(testCredentials));

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

            lobClient.Letter.Create(letter);
        }
    }
}
