using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Localizer.Properties;
using HorizontalAlignment=System.Windows.HorizontalAlignment;

namespace Localizer {
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private readonly FolderBrowserDialog fbd = new FolderBrowserDialog();
        private LanguageInfo.LANG neutralLang = LanguageInfo.LANG.de;
        private List<GridInfo> allData = new List<GridInfo>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            fbd.SelectedPath = Settings.Default.lastFolder;
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                Settings.Default.lastFolder = fbd.SelectedPath;
                collectData(fbd.SelectedPath);
                Settings.Default.Save();
            }
        }

        private void collectData(string path)
        {
            // find data
            Dictionary<string, Files4Language> res = LocalizeTools.findFilesExtension(path, "*.resx", neutralLang, false);
            Dictionary<string, Files4Language> csvs = LocalizeTools.findFilesExtension(path, "*.csv", neutralLang, true);

            // remove previous data
            translatetabs.Items.Clear();

            // display data
            addToDisplay(res);
            addToDisplay(csvs);
        }

        private void addToDisplay(Dictionary<string, Files4Language> res)
        {
            foreach (var re in res) {
                var langCollection = new ObservableCollection<LanguageInfo>();
                var langDic = new Dictionary<string, LanguageInfo>(); // remember lang keys, no doubles please...

                foreach (var info in re.Value.LangFiles) {
                    if (re.Value.IsCSV) {
                        LocalizeTools.addCSV(info.Value.FullName, langCollection, langDic, info.Key);
                    } else {
                        LocalizeTools.addResource(info.Value.FullName, langCollection, langDic, info.Key);
                    }
                }
                var ti = new TabItem();
                ti.Header = re.Key;
                var red = new ResEditControl();
                GridInfo gi = new GridInfo { Items = langCollection, SourceInfo = re.Value , ShowType = re.Value.IsCSV};
                red.DataContext = gi;
                allData.Add(gi);
                ti.Content = red;
                ti.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                ti.VerticalContentAlignment = VerticalAlignment.Stretch;
                translatetabs.Items.Add(ti);
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var gi in allData) {
                foreach (var f4l in gi.SourceInfo.LangFiles) {
                    if (gi.SourceInfo.IsCSV) {
                        LocalizeTools.createCSVFile(f4l.Value.FullName, gi.Items, f4l.Key);
                    }
                    else
                    {
                        LocalizeTools.createResourceFile(f4l.Value.FullName, gi.Items, f4l.Key);
                    }
                }
            }
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // consider to save
            Close();
        }
    }
}