using DIPO_INVESTAMA.App_Start;
using DIPO_INVESTAMA.Logic;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class OutputController : Controller
    {
        private string fontPath = ConfigurationManager.AppSettings["FontPath"];
        // GET: Output
        [CheckAuthorizeAttribute()]
        public ActionResult Index()
        {
            OutputViewModels model = new OutputViewModels();
            model.TodaysJournal = Journals(model);
            ViewBag.AccountList = common.ToSelectList(InputBusinessLogic.getInstance().getAccountDDL(), "ID", "NAME", string.Empty);
            ViewBag.BankFacilityList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.BankAccount);
            ViewBag.SortByList = common.ToSelectList(OutputBusinessLogic.getInstance().getSortByDDL(), "ID", "NAME", string.Empty);
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string Submit, OutputViewModels model)
        {
            model.TodaysJournal = Journals(model);
            ViewBag.AccountList = common.ToSelectList(InputBusinessLogic.getInstance().getAccountDDL(), "ID", "NAME", model.Account);
            ViewBag.BankFacilityList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.BankAccount);
            ViewBag.SortByList = common.ToSelectList(OutputBusinessLogic.getInstance().getSortByDDL(), "ID", "NAME", model.SortBy);

            if (!String.IsNullOrEmpty(Submit))
            {
                if (Submit.Equals("PDF"))
                {
                    GeneratePdf(model);
                }
                else if (Submit.Equals("Excel"))
                {
                    GenerateExcel(model);
                }
                else if (Submit.Equals("Print"))
                {

                }
            }

            return View(model);
        }

        private PagedList<OutputViewModels> Journals(OutputViewModels model)
        {
            var page = new PagedList<OutputViewModels>();
            page.Content = OutputBusinessLogic.getInstance().TodaysJournal(model);
            return page;
        }

        private void GeneratePdf(OutputViewModels model)
        {
            MemoryStream ms = new MemoryStream();
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            Document doc = new Document(rec);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);
            doc.Open();

            BaseFont arial = BaseFont.CreateFont(Server.MapPath(fontPath) + "ARIALN.TTF", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk("DIPO INVESTAMA", new iTextSharp.text.Font(arial, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            doc.Add(prgHeading);

            prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(" ", new iTextSharp.text.Font(arial, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
            doc.Add(prgHeading);

            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            table.SetWidthPercentage(new float[] { 15, 15, 15, 25, 10, 10, 10 }, new iTextSharp.text.Rectangle(doc.PageSize.Width / 6, doc.PageSize.Height));

            PdfPCell cell = new PdfPCell();
            cell.Phrase = new Phrase("Date"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Account"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Bank"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Description"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Cash Out"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Cash In"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            cell = new PdfPCell();
            cell.Phrase = new Phrase("Balance"
                        , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 1;
            table.AddCell(cell);

            foreach (var item in model.TodaysJournal.Content)
            {
                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.Date
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.Account
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.BankAccount
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.Description
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.CashIn
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.CashOut
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);

                cell = new PdfPCell();
                cell.Phrase = new Phrase(item.Balance
                            , new iTextSharp.text.Font(arial, 9, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 1;
                table.AddCell(cell);
            }

            doc.Add(table);
            doc.Close();

            byte[] result = ms.ToArray();

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.Buffer = true;
            response.Charset = "";
            response.ContentType = MimeMapping.GetMimeMapping("Journals_.pdf");
            response.AddHeader("content-disposition", "attachment;filename=Journals_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".pdf");
            response.BinaryWrite(result);
            response.End();
        }

        private void GenerateExcel(OutputViewModels model)
        {
            ExcelPackage Package = new ExcelPackage();
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Data");
            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#00b0f0");
            OfficeOpenXml.Style.ExcelBorderStyle DefaultBorder = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells["A1"].LoadFromText("Date");
            ws.Cells["A1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["A1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["A1"].Style.Font.Bold = true;
            ws.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["A1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["A1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["A1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["A1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["B1"].LoadFromText("Account");
            ws.Cells["B1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["B1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["B1"].Style.Font.Bold = true;
            ws.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["B1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["B1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["B1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["B1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["B1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["C1"].LoadFromText("Bank");
            ws.Cells["C1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["C1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["C1"].Style.Font.Bold = true;
            ws.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["C1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["C1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["C1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["C1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["C1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["D1"].LoadFromText("Description");
            ws.Cells["D1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["D1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["D1"].Style.Font.Bold = true;
            ws.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["D1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["D1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["D1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["D1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["D1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["E1"].LoadFromText("Cash Out");
            ws.Cells["E1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["E1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["E1"].Style.Font.Bold = true;
            ws.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["E1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["E1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["E1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["E1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["E1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["E1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["E1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["F1"].LoadFromText("Cash In");
            ws.Cells["F1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["F1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["F1"].Style.Font.Bold = true;
            ws.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["F1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["F1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["F1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["F1"].Style.Border.Right.Style = DefaultBorder;

            ws.Cells["G1"].LoadFromText("Balance");
            ws.Cells["G1"].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
            ws.Cells["G1"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["G1"].Style.Font.Bold = true;
            ws.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["G1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
            ws.Cells["G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells["G1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            ws.Cells["G1"].Style.Border.Bottom.Style = DefaultBorder;
            ws.Cells["G1"].Style.Border.Left.Style = DefaultBorder;
            ws.Cells["G1"].Style.Border.Top.Style = DefaultBorder;
            ws.Cells["G1"].Style.Border.Right.Style = DefaultBorder;

            int idx = 1;
            foreach (var item in model.TodaysJournal.Content)
            {
                idx++;
                ws.Cells["A" + idx.ToString()].LoadFromText(item.Date);
                ws.Cells["A" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["A" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["B" + idx.ToString()].LoadFromText(item.Account);
                ws.Cells["B" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["B" + idx.ToString()].Style.Numberformat.Format = "dd MMM yy HH:mm";
                ws.Cells["B" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["C" + idx.ToString()].LoadFromText(item.BankAccount);
                ws.Cells["C" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["C" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["D" + idx.ToString()].LoadFromText(item.Description);
                ws.Cells["D" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["D" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["D" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["D" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["D" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["D" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["D" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["E" + idx.ToString()].LoadFromText(item.CashOut);
                ws.Cells["E" + idx.ToString()].Style.Numberformat.Format = "##0";
                ws.Cells["E" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["E" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["E" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["E" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["E" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["E" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["E" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["F" + idx.ToString()].LoadFromText(item.CashIn);
                ws.Cells["F" + idx.ToString()].Style.Numberformat.Format = "##0";
                ws.Cells["F" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["F" + idx.ToString()].Style.Numberformat.Format = "#,#0 Liter";
                ws.Cells["F" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["F" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["F" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["F" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["F" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["F" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["G" + idx.ToString()].LoadFromText(item.Balance);
                ws.Cells["G" + idx.ToString()].Style.Numberformat.Format = "##0";
                ws.Cells["G" + idx.ToString()].Style.Font.SetFromFont(new System.Drawing.Font("Cambria", 10));
                ws.Cells["G" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["G" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["G" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["G" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["G" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["G" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;
            }

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Journals_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
            Response.BinaryWrite(Package.GetAsByteArray());
            Response.End();
        }
    }
}