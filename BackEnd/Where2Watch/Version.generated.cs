using System;
using System.Collections.Generic;
using System.Text;

namespace Where2Watch {
    public static class Version {

        public static readonly int Major = 0;
        public static readonly int Minor = 1;
        public static readonly int Patch = 0;
        public static readonly int Build = 156;

        public static string GetVersion() { return $"{Major}.{Minor}.{Patch} Build {Build}"; }

    }
}
