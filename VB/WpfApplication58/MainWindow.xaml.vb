Imports DevExpress.Data
Imports DevExpress.Utils.Serializing
Imports DevExpress.Xpf.Core.Serialization
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes

Namespace WpfApplication58
    ''' <summary>
    ''' Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Public Sub New()
            InitializeComponent()

            Dim customers As New ObservableCollection(Of Customer)()
            For i As Integer = 1 To 29
                customers.Add(New Customer() With {.ID = i, .Name = "Name" & i})
            Next i
            grid.ItemsSource = customers
            grid.Columns("Name").AddHandler(DXSerializer.CreateContentPropertyValueEvent, New XtraCreateContentPropertyValueEventHandler(AddressOf OnCreateContentPropertyValue))
            'nameColumn.SomeCustomProperty = new CustomObject() { };
        End Sub
        Private Sub OnCreateContentPropertyValue(ByVal sender As Object, ByVal e As XtraCreateContentPropertyValueEventArgs)
            e.PropertyValue = New CustomObject()
            CType(e.Owner, MyGridColumn).SomeCustomProperty = CType(e.PropertyValue, CustomObject)
        End Sub
        Private Sub Button_Click_1(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.RestoreLayoutFromXml("..\..\layout.xml")
        End Sub

        Private Sub Button_Click_2(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.SaveLayoutToXml("..\..\layout.xml")
        End Sub

        Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            grid.RestoreLayoutFromXml("..\..\layout.xml")
        End Sub
    End Class

    Public Class MyGridColumn
        Inherits GridColumn

        <XtraSerializableProperty(XtraSerializationVisibility.Content, True)> _
        Public Property SomeCustomProperty() As CustomObject
            Get
                Return CType(GetValue(SomeCustomPropertyProperty), CustomObject)
            End Get
            Set(ByVal value As CustomObject)
                SetValue(SomeCustomPropertyProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SomeCustomPropertyProperty As DependencyProperty = DependencyProperty.Register("SomeCustomProperty", GetType(CustomObject), GetType(MyGridColumn), Nothing)


    End Class
    Public Class CustomObject
        Implements INotifyPropertyChanged


        Private propertyA_Renamed As String

        Private propertyB_Renamed As String
        <XtraSerializableProperty> _
        Public Property PropertyA() As String
            Get
                Return propertyA_Renamed
            End Get
            Set(ByVal value As String)
                propertyA_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PropertyA"))
            End Set
        End Property
        <XtraSerializableProperty> _
        Public Property PropertyB() As String
            Get
                Return propertyB_Renamed
            End Get
            Set(ByVal value As String)
                propertyB_Renamed = value
                RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs("PropertyB"))
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Sub RaisePropertyChanged(ByVal propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class


    Public Class Customer
        Public Property ID() As Integer

        Public Property Name() As String
    End Class
End Namespace
