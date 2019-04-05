<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/WpfApplication58/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/WpfApplication58/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/WpfApplication58/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/WpfApplication58/MainWindow.xaml.vb))
<!-- default file list end -->
# How to deserialize custom properties using the CreateContentPropertyValue event


<p>This example demonstrates how to serialize and deserialize custom properties with a custom type. If a custom property is null when the deserialization process is invoked, it's necessary to handle the DXSerializer.CreateContentPropertyValue event. In the CreateContentPropertyValue event handler, create a new instance of a custom type and assign it to the XtraCreateContentPropertyValueEventArgs.PropertyValue property. </p>

<br/>


