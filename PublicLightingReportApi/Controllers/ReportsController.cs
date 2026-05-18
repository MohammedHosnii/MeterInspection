using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
 

namespace PublicLightingReportApi.Controllers
{
    // Enable CORS for cross-origin requests
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]

    public class ReportsController : ApiController
    {

        private static readonly ConcurrentDictionary<string, ReportDocument> _reportCache = new ConcurrentDictionary<string, ReportDocument>();

        

        // POST method for generating reports
        [HttpPost]
        [Route("Reports/viewerOld")]
        public IHttpActionResult Index([FromBody] ReportInput reportInput)
        {
            try
            {
                using (ReportDocument reportDocument = new ReportDocument())
                {

             
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Reports", reportInput.ReportName + ".rpt");

  
                
                    reportDocument.Load(reportPath);

                    // Deserialize the input data
                    string ReportDataJson = reportInput.ReportData;
                    string ReportArgsJson = reportInput.ReportArgs;

                    // Convert JSON data to DataTable and List
                    DataTable dt_ReportDataJson = JsonConvert.DeserializeObject<DataTable>(ReportDataJson);
                    List<string> list_ReportArgsJson = JsonConvert.DeserializeObject<List<string>>(ReportArgsJson);

                    // Set data source for the report
                    reportDocument.SetDataSource(dt_ReportDataJson);

                    reportDocument.DataDefinition.FormulaFields[$"arg0"].Text =   "'" + ConfigurationManager.AppSettings["CompanyName"] + "'";
                    reportDocument.DataDefinition.FormulaFields[$"imgUrl"].Text =   "'" + Path.Combine( AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogoImgUrl"]) + "'";
                    // Set report arguments (Formula fields)
                    for (int i = 0; i < list_ReportArgsJson.Count; i++)
                    {
                        reportDocument.DataDefinition.FormulaFields[$"arg{i + 1}"].Text = $"'{list_ReportArgsJson[i]}'";
                    }

                    var exportOptions = reportDocument.ExportOptions;

                    exportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOptions.FormatOptions = new PdfRtfWordFormatOptions
                    {
                        UsePageRange = false
                    };

                    // Export the report to a stream (PDF format)
                    var stream = reportDocument.ExportToStream(ExportFormatType.PortableDocFormat);
                    stream.Seek(0, SeekOrigin.Begin);

                    // Create the HttpResponseMessage with the PDF content
                    var response = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(stream)
                    };
                    response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                    response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
                    {
                        FileName = reportInput.ReportName + ".pdf"
                    };

                    // Return the response
                    return ResponseMessage(response);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex); // Return an internal server error if something goes wrong
            }
        }

      

        [HttpGet]
        [Route("")]
        public IHttpActionResult Index()
        {
            // Path to the HTML file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.html");

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                return NotFound(); // Return 404 if the file doesn't exist
            }

            // Read the content of the HTML file
            string htmlContent = File.ReadAllText(filePath);

            // Create an HttpResponseMessage
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(htmlContent)
            };

            // Set the content type to 'text/html'
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

            // Return the response
            return ResponseMessage(response);
        }

        [Route("api/TestPage")]  
        [HttpGet]
        public IHttpActionResult TestPage()
        {
            // Construct the full file path to the PDF
            string pdfTestFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "test.pdf");

            // Check if the file exists
            if (!System.IO.File.Exists(pdfTestFilePath))
            {
                return NotFound(); // Return 404 if the file doesn't exist
            }

            // Read the file bytes
            var fileBytes = System.IO.File.ReadAllBytes(pdfTestFilePath);

            // Create a response message to return the file with the proper MIME type
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(fileBytes)
            };

            // Set the content type to 'application/pdf' to indicate this is a PDF file
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

            // Remove or set Content-Disposition to inline to view in the browser
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
            {
                FileName = "test.pdf"  // Optionally set the file name (this is not necessary for inline display)
            };

            return ResponseMessage(response);
        }


        [HttpPost]
        [Route("api/GenerateExcel")]
        public HttpResponseMessage GenerateExcel([FromBody] ReportInput input)
        {
            try
            {
                // Parse the JSON reportData into a list of dictionaries
                var reportData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(input.ReportData);
                if (reportData == null || !reportData.Any())
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid report data.");
                }

                // Create an Excel package
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(input.ReportName);

                    // Add headers
                    var headers = reportData.First().Keys.ToList();
                    for (int i = 0; i < headers.Count; i++)
                    {
                        var cell = worksheet.Cells[1, i + 1];
                        cell.Value = TranslationDictionary.Ara(headers[i]);

                        // Style the header cell
                        cell.Style.Font.Bold = true; // Make text bold
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; // Set fill pattern
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen); // Set background color to gray


                    }

                    // Add data rows
                    for (int row = 0; row < reportData.Count; row++)
                    {
                        var dataRow = reportData[row];
                        for (int col = 0; col < headers.Count; col++)
                        {
                            // Check if the key exists before accessing
                            if (dataRow.ContainsKey(headers[col]))
                            {
                                worksheet.Cells[row + 2, col + 1].Value = dataRow[headers[col]]?.ToString();
                            }
                            else
                            {
                                worksheet.Cells[row + 2, col + 1].Value = ""; // Default value for missing keys
                            }



                        }
                    }

                    // Adjust column widths
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Convert to a memory stream
                    var stream = new MemoryStream(package.GetAsByteArray());
                    var result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(stream)
                    };

                    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                    {
                        FileName = $"{input.ReportName}.xlsx"
                    };

                    return result;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, new { Message = "An error occurred while generating the Excel file.", Details = ex.ToString() }.ToString());
            }
        }


        [HttpPost]
        [Route("Reports/viewer")]
        public async Task<IHttpActionResult> IndexV2([FromBody] ReportInput reportInput)
        {
            try
            {
                if (!IsValidJson(reportInput.ReportData))
                {
                    return InternalServerError(new Exception("Report Data Not ValidJson [X] ."));
                }

                // Generate the report path
                //string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", reportInput.ReportName + ".rpt");
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ,"bin", "Reports", reportInput.ReportName + ".rpt");


                if (!File.Exists(reportPath))
                {
                    return InternalServerError(new Exception($"Report File Not Found [{Path.GetFileName(reportPath)}]"));
                }


                // Try to get the report document from the cache
                if (!_reportCache.TryGetValue(reportPath, out var reportDocument))
                {
                    // If not in cache, load the report document
                    reportDocument = new ReportDocument();
                    await Task.Run(() => reportDocument.Load(reportPath)); // Load asynchronously
                    _reportCache[reportPath] = reportDocument; // Add to cache
                }

                // Deserialize JSON data asynchronously
                var dt_ReportDataJson = await Task.Run(() => JsonConvert.DeserializeObject<DataTable>(reportInput.ReportData));
                var list_ReportArgsJson = await Task.Run(() => JsonConvert.DeserializeObject<List<string>>(reportInput.ReportArgs));

                // Set data source for the report
                if (dt_ReportDataJson != null)
                {
                    if (dt_ReportDataJson.Rows.Count == 0)
                    {
                        throw new Exception("Empty data found.");
                    }
                    else
                    {
                        reportDocument.SetDataSource(dt_ReportDataJson);
                    }
                   
                }               
                else
                {
                    throw new Exception("Invalid report data [NULL].");
                }
                 

                // Set static formula fields
                reportDocument.DataDefinition.FormulaFields["arg0"].Text = "'" + ConfigurationManager.AppSettings["CompanyName"] + "'";
                reportDocument.DataDefinition.FormulaFields["imgUrl"].Text = "'" + Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["LogoImgUrl"]) + "'";

                // Set dynamic formula fields
                for (int i = 0; i < list_ReportArgsJson.Count; i++)
                {
                    try
                    {
                        reportDocument.DataDefinition.FormulaFields[$"arg{i + 1}"].Text = $"'{list_ReportArgsJson[i]}'";
                    }
                    catch (Exception) { }
                    
                    
                }

                // Export the report to a stream asynchronously
                var stream = await Task.Run(() => reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
                stream.Seek(0, SeekOrigin.Begin);

                // Create the HttpResponseMessage with the PDF content
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(stream)
                };
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("inline")
                {
                    FileName = reportInput.ReportName + ".pdf"
                };

                // Return the response
                return ResponseMessage(response);
            }
            catch (FileNotFoundException ex)
            {
                return InternalServerError(new Exception($"{ex.Message} [{Path.GetFileName(ex.FileName)}]"));  
            }
            catch (Exception ex)
            {
               
                return InternalServerError(new Exception(ex.Message));
            }
        }


        private  bool IsValidJson(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return false;

            // Quick check for common JSON patterns
            jsonString = jsonString.Trim();
            if ((jsonString.StartsWith("{") && jsonString.EndsWith("}")) || // Object
                (jsonString.StartsWith("[") && jsonString.EndsWith("]")))   // Array
            {
                try
                {
                    JToken.Parse(jsonString);
                    return true;
                }
                catch (JsonReaderException)
                {
                    return false;
                }
            }

            return false;
        }


    }
}