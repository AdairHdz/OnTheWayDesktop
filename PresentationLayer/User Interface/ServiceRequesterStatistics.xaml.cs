using BusinessLayer.BusinessEntities;
using LiveCharts;
using LiveCharts.Wpf;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ServiceRequesterStatistics.xaml
    /// </summary>
    public partial class ServiceRequesterStatistics : Window
    {
        public Func<ChartPoint, string> PointLabel { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }
        private Statistics _statistics = new Statistics();
        public ServiceRequesterStatistics()
        {
            InitializeComponent();
            DataContext = this;
            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Solicitudes de servicio",
                    Values = new ChartValues<int>{0,0,0,0,0,0,0}
                },
            };
            YFormatter = value => value.ToString("N");
            Labels = new[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" };            
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            ServiceRequesterMenu serviceRequesterMenu = new ServiceRequesterMenu();
            serviceRequesterMenu.Show();
            Close();
        }

        private void PopulateCharts()
        {
            PopulateLineChart();
            PopulatePieChart();
        }

        private void PopulateLineChart()
        {
            IList<object> requestedServices = new List<object>();
            _statistics.RequestedServicesPerWeekday.ForEach(weekday =>
            {
                requestedServices.Add(weekday.RequestedServices);
            });
            SeriesCollection[0].Values.Clear();
            SeriesCollection[0].Values.AddRange(requestedServices);
        }

        private void PopulatePieChart()
        {
            PieChart.Series.Clear();
            _statistics.RequestedServicesPerKindOfService.ForEach(kindOfService =>
            {                
                PieSeries pieSeries = new PieSeries
                {
                    Title = CreateKindOfService(kindOfService.KindOfService),
                    Values = new ChartValues<int> { kindOfService.RequestedServices },
                    DataLabels = true,
                    LabelPoint = PointLabel
                };                
                PieChart.Series.Add(pieSeries);
            });            
        }

        private static string CreateKindOfService(int kindOfService)
        {
            string kindOfServiceName;
            switch (kindOfService)
            {
                case 0:
                    kindOfServiceName = "Pago de servicios";
                    break;
                case 1:
                    kindOfServiceName = "Entrega";
                    break;
                case 2:
                    kindOfServiceName = "Compra de fármacos";
                    break;
                case 3:
                    kindOfServiceName = "Compra de víveres";
                    break;
                default:
                    kindOfServiceName = "Otro";
                    break;
            }
            return kindOfServiceName;
        }

        private void SearchButtonClicked(object sender, RoutedEventArgs e)
        {            
            try
            {
                DateTime startingDate = DatePickerStartingDate.SelectedDate.Value.Date;
                DateTime endingDate = DatePickerEndingDate.SelectedDate.Value.Date;
                if (DatePickerStartingDate.SelectedDate == null || DatePickerEndingDate.SelectedDate == null)
                {
                    throw new FormatException();
                }
                startingDate = DatePickerStartingDate.SelectedDate.Value.Date;
                endingDate = DatePickerEndingDate.SelectedDate.Value.Date;                
                Dictionary<string, string> queryParameters = new Dictionary<string, string>
                {
                    ["startingDate"] = startingDate.ToString("yyyy-MM-dd"),
                    ["endingDate"] = endingDate.ToString("yyyy-MM-dd"),
                };
                ServiceRequester serviceRequester = new ServiceRequester();
                _statistics = serviceRequester.GetStatistics(queryParameters);
                PopulateCharts();
            }
            catch (FormatException)
            {
                NotificationWindow.ShowErrorWindow("Fecha no válida", "Por favor, ingrese una fecha con formato válido.");
            }
            catch (NetworkRequestException networkRequestException)
            {
                string exceptionMessage;
                switch (networkRequestException.StatusCode)
                {
                    case 400:
                        exceptionMessage = "Los datos que ha ingresado tienen un formato no válido. Favor de verificar e intentar de nuevo.";
                        break;
                    case 404:
                        exceptionMessage = "No se encontraron datos para el periodo especificado.";
                        SeriesCollection[0].Values.Clear();
                        PieChart.Series.Clear();
                        break;
                    case 409:
                    case 500:
                        exceptionMessage = "Ha ocurrido un error en el servidor al intentar procesar su solicitud. Por favor, intente más tarde.";
                        break;
                    default:
                        exceptionMessage = "Ha ocurrido un error desconocido. Por favor, intente más tarde.";
                        break;
                }
                NotificationWindow.ShowErrorWindow("Error", exceptionMessage);
            }
        }
    }
}
