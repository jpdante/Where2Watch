using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Where2Watch.Extensions {
    public static class ValidationExtension {

        private static readonly Regex UsernameRegex = new Regex("^[a-zA-Z0-9_.-]*$");
        private static readonly Regex GuidRegex = new Regex("^[a-zA-Z0-9]*$");

        private const int GuidLength = 32;

        private const int MinPasswordLength = 8;
        private const int MinCommentLength = 10;

        private const int MaxUsernameLength = 20;
        private const int MaxChannelNameLength = 32;
        private const int MaxChannelLinkLength = 16;

        public static bool ValidateUsername(this string username) {
            return username.Length <= MaxUsernameLength && UsernameRegex.IsMatch(username);
        }

        public static bool ValidateChannelName(this string channelName) {
            return channelName.Length <= MaxChannelNameLength && UsernameRegex.IsMatch(channelName);
        }

        public static bool ValidateChannelLink(this string channelLink) {
            return channelLink.Length <= MaxChannelLinkLength && GuidRegex.IsMatch(channelLink);
        }

        public static bool ValidateEmail(this string email) {
            try {
                _ = new MailAddress(email);
                return true;
            } catch {
                return false;
            }
        }

        public static bool ValidateBirthDate(this DateTime birthDate) {
            return birthDate >= (new DateTime(DateTime.UtcNow.Year - 120, 1, 1)) && birthDate < DateTime.UtcNow;
        }

        public static bool ValidatePasswordLength(this string password) {
            return password.Length >= MinPasswordLength;
        }

        public static bool ValidateGuid(this string guid) {
            return guid.Length == GuidLength && GuidRegex.IsMatch(guid);
        }

        public static bool ValidateLanguage(this string language) {
            return language.Length == 2;
        }

        public static bool ValidateComment(this string comment) {
            return comment.Length >= MinCommentLength;
        }

        public static string EscapeComment(this string comment) {
            return comment
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }
    }
}