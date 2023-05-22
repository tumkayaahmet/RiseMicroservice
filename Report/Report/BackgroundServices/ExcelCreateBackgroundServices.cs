using ClosedXML.Excel;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Services;
using Shared.Dtos;
using System.Data;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Report.BackgroundServices
{
    public class ExcelCreateBackgroundServices : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        public ExcelCreateBackgroundServices(RabbitMQClientService rabbitMQClientService)
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
            return Task.CompletedTask;
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            await Task.Delay(5000);
            var reportDataEvent = JsonSerializer.Deserialize<ReportDto>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            using var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(GetExcelDataTable(reportDataEvent));
            

            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);

            MultipartFormDataContent multipartFormDataContent = new();

            multipartFormDataContent.Add(new ByteArrayContent(ms.ToArray()), "file", Guid.NewGuid().ToString() + ".xlsx");

            _channel.BasicAck(@event.DeliveryTag, false);

 
        }
        private DataTable GetExcelDataTable(ReportDto reportDto)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Location", typeof(string));
            table.Columns.Add("PersonCount", typeof(int));
             
                table.Rows.Add(new ReportDto()
                {
                    Location = reportDto.Location,
                    PersonCount= reportDto.PersonCount
                });
             

            return table;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
