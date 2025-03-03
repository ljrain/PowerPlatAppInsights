using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.DataContracts;

namespace TrackAvailability
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Usage: dotnet run <duration> <status> <message> <system>");
                return;
            }

            int duration = int.Parse(args[0]);
            string status = args[1];
            string message = args[2];
            string system = args[3];

            var telemetryClient = new TelemetryClient(new TelemetryConfiguration("YOUR_INSTRUMENTATION_KEY"));

            var availabilityTelemetry = new AvailabilityTelemetry
            {
                Duration = TimeSpan.FromMilliseconds(duration),
                Success = status.Equals("Success", StringComparison.OrdinalIgnoreCase),
                Message = message,
                Name = system,
                Timestamp = DateTimeOffset.UtcNow
            };

            telemetryClient.TrackAvailability(availabilityTelemetry);
            telemetryClient.Flush();

            Console.WriteLine("Availability tracked successfully.");
        }
    }
}
