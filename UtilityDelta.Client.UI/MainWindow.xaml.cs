using System;
using System.Net;
using System.Windows;
using UtilityDelta.Client.ServiceLocator;
using UtilityDelta.Shared.Common;
using UtilityDelta.Shared.Dto;
using UtilityDelta.Shared.Interface;

namespace UtilityDelta.Client.UI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BindingClass m_binding = new BindingClass()
        {
            IsAdd = true,
            Value1 = 5,
            Value2 = 10,
            IsSubtract = false,
            Password = "password",
            Server = Dns.GetHostEntry(Environment.MachineName).HostName,
            Username = "admin",
            Results = "Results go here."
        };
        
        private DynamicProxyCache m_dynamicProxyCache;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = m_binding;

            //Could also use a client side autofac dependency resolver for this
            m_dynamicProxyCache = new DynamicProxyCache(new DynamicProxyBuilder(), new ServiceWrapper(new Serializer(), m_binding));
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            m_binding.Results += System.Environment.NewLine + "Processing...";
            var service = m_dynamicProxyCache.GetProxyInstance<ITestService>();
            var result = await service.PerformOperation(new DtoPerformOperation()
            {
                NumberOne = m_binding.Value1,
                NumberTwo = m_binding.Value2,
                OperationType = m_binding.IsAdd ? EnumOperationType.Add : EnumOperationType.Subtract
            });
            m_binding.Results += System.Environment.NewLine + "Result: " + result.Result;
            m_binding.Results += System.Environment.NewLine + "Executed on server by: " + result.ExecutedBy;
            m_binding.Results += System.Environment.NewLine;
        }
    }
}