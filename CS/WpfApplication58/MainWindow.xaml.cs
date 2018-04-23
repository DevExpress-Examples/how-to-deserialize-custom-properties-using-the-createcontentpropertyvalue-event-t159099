using DevExpress.Data;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace WpfApplication58
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();
            for (int i = 1; i < 30; i++)
            {
                customers.Add(new Customer() { ID = i, Name = "Name" + i });
            }
            grid.ItemsSource = customers;
            grid.Columns["Name"].AddHandler(DXSerializer.CreateContentPropertyValueEvent, new XtraCreateContentPropertyValueEventHandler(OnCreateContentPropertyValue));
            //nameColumn.SomeCustomProperty = new CustomObject() { };
        }
        void OnCreateContentPropertyValue(object sender, XtraCreateContentPropertyValueEventArgs e)
        {
            e.PropertyValue = new CustomObject();
            ((MyGridColumn)e.Owner).SomeCustomProperty = (CustomObject)e.PropertyValue;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            grid.RestoreLayoutFromXml("..\\..\\layout.xml");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            grid.SaveLayoutToXml("..\\..\\layout.xml");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            grid.RestoreLayoutFromXml("..\\..\\layout.xml");
        }
    }

    public class MyGridColumn : GridColumn
    {
        [XtraSerializableProperty(XtraSerializationVisibility.Content, true)]
        public CustomObject SomeCustomProperty
        {
            get { return (CustomObject)GetValue(SomeCustomPropertyProperty); }
            set { SetValue(SomeCustomPropertyProperty, value); }
        }

        public static readonly DependencyProperty SomeCustomPropertyProperty =
DependencyProperty.Register("SomeCustomProperty", typeof(CustomObject), typeof(MyGridColumn), null);


    }
    public class CustomObject : INotifyPropertyChanged
    {
        string propertyA;
        string propertyB;
        [XtraSerializableProperty]
        public string PropertyA
        {
            get
            {
                return propertyA;
            }
            set
            {
                propertyA = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PropertyA"));
            }
        }
        [XtraSerializableProperty]
        public string PropertyB
        {
            get
            {
                return propertyB;
            }
            set
            {
                propertyB = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PropertyB"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class Customer
    {
        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
