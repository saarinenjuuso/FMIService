using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using FMIService.Models;
using FMIService.Services;
using FMIService.Utils;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FMIService
{
    public class FMI
    {
        private readonly ILogger _logger;

        public FMI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FMI>();
        }

        [Function("Wind")]
        //[OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        //[OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        //[OpenApiParameter(name: "start", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Start** parameter")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        //public async Task<List<WindMeasurement>> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        { // HttpResponseData
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string start = req.Query["start"];
            string end = req.Query["end"];

            if (start == null || end == null)
            {
                HttpResponseData incompleteQuery = req.CreateResponse(HttpStatusCode.BadRequest);
                var incompleteQueryMessage = new { message = "Start and End parameters are required" };
                await incompleteQuery.WriteAsJsonAsync(incompleteQueryMessage);
                return incompleteQuery;
            }

            DateTime startDateTime;
            DateTime endDateTime;

            if (!DateTime.TryParseExact(start, new[] { "yyyy-MM-ddTHH:mm:ssZ" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateTime))
            {
                // Handle invalid start date
                HttpResponseData incorrectStartQuery = req.CreateResponse(HttpStatusCode.BadRequest);
                var incompleteQueryMessage = new { message = "Start param must be in yyyy-MM-ddTHH:mm:ssZ format" };
                await incorrectStartQuery.WriteAsJsonAsync(incompleteQueryMessage);
                return incorrectStartQuery;
            }

            if (!DateTime.TryParseExact(end, new[] { "yyyy-MM-ddTHH:mm:ssZ" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDateTime))
            {
                // Handle invalid end date
                HttpResponseData incorrectEndQuery = req.CreateResponse(HttpStatusCode.BadRequest);
                var incompleteQueryMessage = new { message = "End param must be in yyyy-MM-ddTHH:mm:ssZ format" };
                await incorrectEndQuery.WriteAsJsonAsync(incompleteQueryMessage);
                return incorrectEndQuery;
            }

            // Add the ' around the time separators to keep the culture info from being applied
            start = startDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH':'mm':'ss'Z'");
            end = endDateTime.ToUniversalTime().ToString("yyyy-MM-ddTHH':'mm':'ss'Z'");

            // String interpolation with the start and end parameters
            string url = $"https://opendata.fmi.fi/wfs?service=WFS&version=2.0.0&request=GetFeature&storedquery_id=fmi::observations::weather::multipointcoverage&place=H%C3%A4meenlinna&timestep=120&starttime={start:O}&endtime={end:O}&parameters=wd_10min,ws_10min,wg_10min";

            try
            {
                HttpResponseMessage xmlResponse = await HttpHelper.GetAsync(url);
                Debug.WriteLine("Broken not");
                
                string xmlString = await xmlResponse.Content.ReadAsStringAsync();
                Debug.WriteLine("Broken 222");
                List<WindMeasurement> windMeasurements = Utilities.ParseAndMergeData(xmlString);

                var responseToClient = req.CreateResponse(HttpStatusCode.OK);

                // Adding headers, not necessary
                //responseToClient.Headers.Add("Content-Type", "application/json; charset=utf-8");

                await responseToClient.WriteAsJsonAsync(windMeasurements);
                return responseToClient;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            var message = new { message = "Something went wrong" };
            var resp = req.CreateResponse(HttpStatusCode.InternalServerError);

            await resp.WriteAsJsonAsync(message);
            return resp;
        }
    }
}
