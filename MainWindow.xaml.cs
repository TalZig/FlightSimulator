using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.ViewModel vm;
        public MainWindow() {
            InitializeComponent();
            DataContext = vm;
        }

        public double ThrottleValue
        {
            get { return (double)GetValue(ThrottleValueProperty); }
            set { SetValue(ThrottleValueProperty, value); }
        }

        public static readonly DependencyProperty ThrottleValueProperty =
            DependencyProperty.Register("ThrottleValue", typeof(double), typeof(Slider));

        public double AileronValue
        {
            get { return (double)GetValue(AileronValueProperty); }
            set { SetValue(AileronValueProperty, value); }
        }

        public static readonly DependencyProperty AileronValueProperty =
            DependencyProperty.Register("AileronValue", typeof(double), typeof(Slider));
    }
}
