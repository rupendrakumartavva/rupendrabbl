using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RazorEngine;

namespace BusinessCenter.Api.Utility
{
    public class PdfResult : IHttpActionResult
    {
        private const string ViewDirectory = "PdfTemplates";
        private readonly string _view;
        private  dynamic _model;
        private readonly string _fileName;
        private readonly string _destinationFileName;
        public PdfResult(string viewName, dynamic model, string fileName, string destinationFileName)
        {
            _view = LoadView(viewName);
            _model = model;
            _fileName = fileName;
            _destinationFileName = destinationFileName;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {

            string headerBoday;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PdfTemplates/PageHeader.cshtml")))
            {
                headerBoday = reader.ReadToEnd();
            }
            var headerBodayModel = new
            {
                Title = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dc_logo.png"

            };
            string footerBoday;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PdfTemplates/PageFooter.cshtml")))
            {
                footerBoday = reader.ReadToEnd();
            }



            var response = new HttpResponseMessage(HttpStatusCode.OK);

           
            var htmlContent = Razor.Parse(_view, _model);
            var htmlHeaderContent = Razor.Parse(headerBoday, headerBodayModel);
            var htmlFooterContent = Razor.Parse(footerBoday, null);
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfStream = new MemoryStream();
            htmlToPdf.Margins.Bottom = 20;
            htmlToPdf.Margins.Top = 20;
            htmlToPdf.Margins.Left = 0;
            htmlToPdf.Margins.Right = 0;
          
            htmlToPdf.PageHeaderHtml = htmlHeaderContent;
            htmlToPdf.PageFooterHtml = htmlFooterContent;

            if (_destinationFileName == null)
            {
                htmlToPdf.GeneratePdf(htmlContent, null, pdfStream);

                //Build the response
               

                response.Content = new ByteArrayContent(pdfStream.ToArray());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _fileName
                };
                return Task.FromResult(response);
            }
            else
            {
                response.Content = new ByteArrayContent(pdfStream.ToArray());
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _fileName
                };
                string UploadDrive = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDrive"].ToString();
                string UploadFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFolder"].ToString();
                string destinationPath = UploadDrive + UploadFolder;
                htmlToPdf.GeneratePdf(htmlContent, null, destinationPath + _destinationFileName);
                return null;
            }
           
        }

        

        private static string LoadView(string name)
        {
            var view = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ViewDirectory, name + ".cshtml"));
            return view;
        }

        public byte[] FileStorageSection()
        {
            //string headerBoday;
            //using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PdfTemplates/PageHeader.cshtml")))
            //{
            //    headerBoday = reader.ReadToEnd();
            //}
            //var headerBodayModel = new
            //{
            //    Title = System.Configuration.ConfigurationManager.AppSettings["siteAddress"] + "/images/dc_logo.png"

            //};
            string footerBoday;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/PdfTemplates/PageFooter.cshtml")))
            {
                footerBoday = reader.ReadToEnd();
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            // Generate PDF from the rendered view content to a memory stream/images/dc_logo.png
            var htmlContent = Razor.Parse(_view, _model);
          //  var htmlHeaderContent = Razor.Parse(headerBoday, headerBodayModel);
            var htmlFooterContent = Razor.Parse(footerBoday, null);
            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            var pdfStream = new MemoryStream();
            htmlToPdf.Margins.Bottom = 20;
            htmlToPdf.Margins.Top = 10;
            htmlToPdf.Margins.Left = 0;
            htmlToPdf.Margins.Right = 0;
         
        //    htmlToPdf.PageHeaderHtml = htmlHeaderContent;
            htmlToPdf.PageFooterHtml = htmlFooterContent;
            string UploadDrive = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDrive"].ToString();
            string UploadFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UploadFolder"].ToString();
            string destinationPath = UploadDrive + UploadFolder;

            byte[] fileBytecode = htmlToPdf.GeneratePdf(htmlContent);
            
            //htmlToPdf.GeneratePdf(htmlContent, null, destinationPath + _destinationFileName);

            response.Content = new ByteArrayContent(pdfStream.ToArray());
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _fileName
            };
            string filename = string.Empty;
            filename = destinationPath + _destinationFileName;
            filename = filename.Replace("\\", "/");
            using (FileStream fs = new FileStream
            (filename, FileMode.Create))
            {
                fs.Write(fileBytecode, 0, fileBytecode.Length);
            }

            try
            {

                return fileBytecode;
            }
            catch (Exception )
            {

             //   throw ex;
                return fileBytecode;
            }



            //return fileBytecode;
        }

    }
}