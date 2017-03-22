using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFStudy
{
	/// <summary>
	/// Window1.xaml 的交互逻辑
	/// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.tvDirectories.ItemsSource = DirectoryTree.InitRoot();
        }
    }

   public class Directory2ImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            String path = value as String;
            //if (System.IO.Directory.Exists(path))
            //    return ExtractIconAndType.GetIcon(path, false, true);
            //else if (System.IO.File.Exists(path))
            //    return ExtractIconAndType.GetIcon(path, false, true);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }


}