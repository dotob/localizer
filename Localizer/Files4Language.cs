using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Localizer
{
    public class Files4Language
    {
        private Dictionary<LanguageInfo.LANG, FileInfo> langFiles = new Dictionary<LanguageInfo.LANG, FileInfo>();

        public Dictionary<LanguageInfo.LANG, FileInfo> LangFiles
        {
            get { return langFiles; }
            set { langFiles = value; }
        }

        public string Name { get; set; }
        public bool IsCSV { get; set; }
    }
}
