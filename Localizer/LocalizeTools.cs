using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;

namespace Localizer {
    public class LocalizeTools {
        public static void addCSV(string f, ICollection<LanguageInfo> csvCollection, IDictionary<string, LanguageInfo> csvDic, LanguageInfo.LANG lang)
        {
            string[] csv = File.ReadAllLines(f);
            foreach (string s in csv) {
                LanguageInfo li;
                string[] line = s.Split(new[] {','});
                string ki = string.Format("{0},{1}", line[0], line[1]);
                if (!csvDic.TryGetValue(ki, out li)) {
                    li = new LanguageInfo();
                    csvCollection.Add(li);
                    csvDic.Add(ki, li);
                }
                li.ID = ki;
                li.Typ = line[2];
                li.OriginalText = s;
                string val = line[6];
                switch (lang) {
                    case LanguageInfo.LANG.de:
                        li.de = val;
                        break;
                    case LanguageInfo.LANG.en:
                        li.en = val;
                        break;
                    case LanguageInfo.LANG.fr:
                        li.fr = val;
                        break;
                    case LanguageInfo.LANG.es:
                        li.es = val;
                        break;
                    case LanguageInfo.LANG.it:
                        li.it = val;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lang");
                }
            }
        }

        public static void createCSVFile(string outFileName, IEnumerable<LanguageInfo> lc, LanguageInfo.LANG lang)
        {
            var list = new List<string>();
            string val;
            foreach (LanguageInfo li in lc) {
                string[] line = li.OriginalText.Split(new[] {','});
                var sb = new StringBuilder();
                for (int i = 0; i <= 5; i++) {
                    sb.Append(line[i]);
                    sb.Append(',');
                }
                switch (lang) {
                    case LanguageInfo.LANG.de:
                        val = li.de;
                        break;
                    case LanguageInfo.LANG.en:
                        val = li.en;
                        break;
                    case LanguageInfo.LANG.fr:
                        val = li.fr;
                        break;
                    case LanguageInfo.LANG.es:
                        val = li.es;
                        break;
                    case LanguageInfo.LANG.it:
                        val = li.it;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lang");
                }
                sb.Append(val);
                sb.Append(',');
                for (int i = 7; i < line.Length; i++) {
                    sb.Append(line[i]);
                    sb.Append(',');
                }
                list.Add(sb.ToString());
            }
            File.WriteAllLines(outFileName, list.ToArray());
        }

        public static void addResource(string resxFile, ICollection<LanguageInfo> langCollection, IDictionary<string, LanguageInfo> langDic, LanguageInfo.LANG lang)
        {
            var read = new ResXResourceReader(resxFile);
            IDictionaryEnumerator id = read.GetEnumerator();
            foreach (DictionaryEntry d in read) {
                LanguageInfo li;
                string ki = d.Key.ToString();
                if (!langDic.TryGetValue(ki, out li)) {
                    li = new LanguageInfo();
                    langCollection.Add(li);
                    langDic.Add(ki, li);
                }
                li.ID = ki;

                switch (lang) {
                    case LanguageInfo.LANG.de:
                        li.de = d.Value.ToString();
                        break;
                    case LanguageInfo.LANG.en:
                        li.en = d.Value.ToString();
                        break;
                    case LanguageInfo.LANG.fr:
                        li.fr = d.Value.ToString();
                        break;
                    case LanguageInfo.LANG.es:
                        li.es = d.Value.ToString();
                        break;
                    case LanguageInfo.LANG.it:
                        li.it = d.Value.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lang");
                }
            }
            read.Close();
        }

        public static void createResourceFile(string outFileName, IEnumerable<LanguageInfo> lc, LanguageInfo.LANG lang)
        {
            var writer = new ResXResourceWriter(outFileName);
            string ki = string.Empty;
            string val = string.Empty;
            foreach (LanguageInfo li in lc) {
                ki = li.ID;
                switch (lang) {
                    case LanguageInfo.LANG.de:
                        val = li.de;
                        break;
                    case LanguageInfo.LANG.en:
                        val = li.en;
                        break;
                    case LanguageInfo.LANG.fr:
                        val = li.fr;
                        break;
                    case LanguageInfo.LANG.es:
                        val = li.es;
                        break;
                    case LanguageInfo.LANG.it:
                        val = li.it;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("lang");
                }
                writer.AddResource(ki, val);
            }
            writer.Close();
        }


        public static Dictionary<string, Files4Language> findFilesExtension(string path, string extension, LanguageInfo.LANG neutralLang, bool isCSV)
        {
            var ret = new Dictionary<string, Files4Language>();
            string[] files = Directory.GetFiles(path, extension);
            foreach (string s in files) {
                var fi = new FileInfo(s);
                string fn = fi.NameWithoutAnyExtension();
                Files4Language f4l;
                if (!ret.TryGetValue(fn, out f4l)) {
                    f4l = new Files4Language();
                    f4l.Name = fn;
                    f4l.IsCSV = isCSV;
                    ret.Add(fn, f4l);
                }
                LanguageInfo.LANG lang = fi.WhichLANG() == LanguageInfo.LANG.NONE ? neutralLang : fi.WhichLANG();
                f4l.LangFiles.Add(lang, fi);
            }
            return ret;
        }
    }
}