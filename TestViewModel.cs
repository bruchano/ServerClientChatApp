using Pie.Commands;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Pie.ViewModels
{
    public class TestViewModel : MainPanelViewModel
    {
        public ICommand TestCommand { get; private set; }

        public TestViewModel(WindowViewModel m)
        {
            windowViewModel = m;
            TestCommand = new TestCommand(FooFunc);
        }

        public void FooFunc(string i, string j)
        {
            MessageBox.Show(i + " " + j);
        }

    }

    public class TestCon : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
