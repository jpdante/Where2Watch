namespace Where2Watch.Core {
    public class Config {

        public DatabaseConfig Db;
        public SmtpConfig Smtp;
        public RedisConfig Redis;
        public RapidApiConfig RapidApi;
        public string CaptchaKey;

        public static Config NewConfig() {
            return new Config {
                Db = new DatabaseConfig {
                    Host = "127.0.0.1",
                    Port = 3306,
                    Database = "w2w",
                    Username = "root",
                    Password = "root"
                },
                Smtp = new SmtpConfig {
                    Host = "127.0.0.1",
                    Port = 465,
                    Username = "username",
                    Password = "password"
                },
                Redis = new RedisConfig {
                    ConnectionString = "localhost"
                },
                RapidApi = new RapidApiConfig {
                    Key = "<key>",
                },
                CaptchaKey = "0x0000000000000000000000000000000000000000",
            };
        }
    }

    public class DatabaseConfig {

        public string Host { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class SmtpConfig {

        public string Host;
        public int Port;
        public string Username;
        public string Password;

    }

    public class RedisConfig {

        public string ConnectionString;

    }

    public class RapidApiConfig {
        public string Key;
    }
}
