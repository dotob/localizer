using System;
using System.IO;

namespace Localizer {
    public static class Util {
        public static string NameWithoutExtension(this FileInfo fi)
        {
            return fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length);
        }

        public static string NameWithoutAnyExtension(this FileInfo fi)
        {
            string[] parts = fi.Name.Split('.');
            if (parts.Length > 0) {
                return parts[0];
            }
            return string.Empty;
        }

        public static LanguageInfo.LANG WhichLANG(this FileInfo fi)
        {
            string[] parts = fi.Name.Split('.');
            foreach (string s in parts) {
                foreach (LanguageInfo.LANG val in Enum.GetValues(typeof (LanguageInfo.LANG))) {
                    if (s.ToLower().Equals(val.ToString().ToLower())) {
                        return val;
                    }
                    // test for part match
                    if (s.IndexOf('-') >= 0) {
                        string cult = s.Substring(0, s.IndexOf('-'));
                        if (cult.ToLower().Equals(val.ToString().ToLower())) {
                            return val;
                        }
                    }
                }
            }
            return LanguageInfo.LANG.NONE;
        }
    }
}