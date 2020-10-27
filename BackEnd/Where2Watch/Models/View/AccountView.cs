using System.Globalization;
using System.Text.Json.Serialization;

namespace Where2Watch.Models.View {
    public class AccountView {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("accountType")]
        public AccountType AccountType { get; set; }

        [JsonPropertyName("lastAccess")]
        public string LastAccess { get; set; }

        public AccountView(Account account) {
            Id = account.Id.ToString();
            Guid = account.Guid;
            Email = account.Email;
            Username = account.Username;
            AccountType = account.AccountType;
            LastAccess = account.LastAccess.ToString(CultureInfo.InvariantCulture);
        }
    }
}