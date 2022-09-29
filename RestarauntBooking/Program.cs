// See https://aka.ms/new-console-template for more information
using RestarauntBooking;

using System.Diagnostics;

Console.OutputEncoding = System.Text.Encoding.UTF8;

RestarauntService _service = new();




while(true)
{
	Console.WriteLine("Привет! Желаете забронировать столик?\n1 - мы уведомим Вас по смс(асинхронно)"+
					"\n2 - подождите на линии, мы Вас оповестим(синхронно)");


	_service.BookTable(Console.ReadLine());

	var stopWatch = new Stopwatch();
	stopWatch.Start();


	stopWatch.Stop();
	var ts = stopWatch.Elapsed;
	Console.WriteLine($"{ts.Seconds:000}:{ts.Milliseconds:000}"); 

}
