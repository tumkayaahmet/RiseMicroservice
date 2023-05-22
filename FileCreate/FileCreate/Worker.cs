using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using FileCreate.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Dtos;
using System.Data;
using System.Text;
using System.Text.Json;

namespace FileCreate
{
    public class Worker : BackgroundService
    {
        static string mqBody = "";

        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        public Worker(RabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            //consumer.Received += async (model, ea) =>
            //{
            //    //var reportDataEvent = JsonSerializer.Deserialize<ReportDto>(Encoding.UTF8.GetString(ea.Body.ToArray()));
            //    var body = ea.Body.ToArray();
            //      mqBody = Encoding.UTF8.GetString(body);
            //    var reportDataEvent = JsonSerializer.Deserialize<ReportDto>(mqBody);
            //    if (reportDataEvent != null)
            //    {
            //        using var ms = new MemoryStream();
            //        var wb = new XLWorkbook();
            //        var ds = new DataSet();
            //        ds.Tables.Add(GetExcelDataTable(reportDataEvent));


            //        wb.Worksheets.Add(ds);
            //        wb.SaveAs(ms);

            //        MultipartFormDataContent multipartFormDataContent = new();

            //        multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            //        var baseUrl = "https://localhost:5222/api/file";

            //        using (var httpClient = new HttpClient())
            //        {

            //            var response = await httpClient.PostAsync($"{baseUrl}?reportDetailId={reportDataEvent.ReportDetailId}", multipartFormDataContent);

            //            if (response.IsSuccessStatusCode)
            //            {
            //                _channel.BasicAck(ea.DeliveryTag, false);
            //            }
            //        }
            //    }
            //    else
            //    {

            //    }

            //};


            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                //var reportDataEvent = JsonSerializer.Deserialize<ReportDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                var body = @event.Body.ToArray();
                mqBody = Encoding.UTF8.GetString(body);
                var reportDataEvent = JsonConvert.DeserializeObject<List<ReportDto>>(mqBody);

                using var ms = new MemoryStream();
                var wb = new XLWorkbook();
                var ds = new DataSet();
                ds.Tables.Add(GetExcelDataTable(reportDataEvent));


                wb.Worksheets.Add(ds);
                wb.SaveAs(ms);

                MultipartFormDataContent multipartFormDataContent = new();

                multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

                var baseUrl = "http://localhost:5222/api/File";
                 using (var httpClient = new HttpClient())
                {

                    var response = await httpClient.PostAsync($"{baseUrl}?reportDetailId={reportDataEvent[0].ReportDetailId}", multipartFormDataContent);

                    if (response.IsSuccessStatusCode)
                    {
                        _channel.BasicAck(@event.DeliveryTag, false);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        private DataTable GetExcelDataTable(List<ReportDto> reportDto)
        {
            DataTable table = new DataTable();
            table.Columns.Add("ReportDetailId", typeof(string));
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("PersonCount", typeof(int));

            //table.Rows.Add(new ReportDto()
            //{
            //    Location = reportDto.Location,
            //    PersonCount = reportDto.PersonCount
            //});

            reportDto.ForEach(x =>
            {
                table.Rows.Add(x.ReportDetailId,x.Location, x.PersonCount);

            });

            return table;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}