using FastTool.CalculationTool;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FastTool.WPF.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListView.xaml
    /// </summary>
    public partial class ListView : UserControl
    {
        double size = 0;

        public ListView()
        {
            InitializeComponent();
            scroll.DataContext = this;
        }

        #region Propertys

        public VerticalAlignment Aligment
        {
            get => (VerticalAlignment)GetValue(AligmentProperty);
            set => SetValue(AligmentProperty, value);
        }

        public static readonly DependencyProperty AligmentProperty = DependencyProperty.Register("Aligment", typeof(VerticalAlignment), typeof(ListView));

        public object Items
        {
            get => GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(object), typeof(ListView));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ListView));

        #endregion

        private void ItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (((ItemsControl)sender).ActualHeight > size)
                scroll.ScrollToEnd();
            size = ((ItemsControl)sender).ActualHeight;
        }
    }
}
