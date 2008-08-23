using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Localizer {
    public class GridInfo {
        public ObservableCollection<LanguageInfo> Items { get; set; }
        public bool ShowType { get; set; }
        public Files4Language SourceInfo { get; set; }
    }
}