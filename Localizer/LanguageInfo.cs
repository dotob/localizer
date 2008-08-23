using System.ComponentModel;

namespace Localizer {
    public class LanguageInfo {
        #region LANG enum

        public enum LANG {
            NONE,
            de,
            en,
            fr,
            es,
            it
        }

        #endregion

        [DisplayName("Key")]
        public string ID { get; set; }        
        
        [DisplayName("Typ")]
        public string Typ { get; set; }


        [DisplayName("deutsch")]
        public string de { get; set; }

        [DisplayName("englisch")]
        public string en { get; set; }

        [DisplayName("französisch")]
        public string fr { get; set; }

        [DisplayName("spanisch")]
        public string es { get; set; }

        [DisplayName("italienisch")]
        public string it { get; set; }

        [DisplayName("Kommentar")]
        public string Comment { get; set; }

        [DisplayName("Original"), Browsable(false)]
        public string OriginalText { get; set; }
    }
}