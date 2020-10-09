using DIPO_INVESTAMA.App_Start;
using DIPO_INVESTAMA.Logic;
using DIPO_INVESTAMA.Models;
using DIPO_INVESTAMA.Utils;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DIPO_INVESTAMA.Controllers
{
    public class HomeController : Controller
    {
        [CheckAuthorizeAttribute()]
        public ActionResult Index()
        {
            ViewBag.FilterBankList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterCashInList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(), "ID", "NAME", string.Empty);
            ViewBag.FilterCashOutList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(), "ID", "NAME", string.Empty);
            return View();
        }

        [HttpPost]
        public ActionResult Index(string Submit, DashboardViewModels model)
        {
            if (!string.IsNullOrEmpty(Submit))
            {
                if (Submit.Equals("Excel_ReportBank"))
                {
                    ExcelReportBank(model);
                }
                else if (Submit.Equals("Excel_ReportAccount"))
                {
                    ExcelReportAccount(model);
                }
                else if (Submit.Equals("Excel_CashIn"))
                {
                    ExcelReportCashIn(model);
                }
                else if (Submit.Equals("Excel_CashOut"))
                {
                    ExcelReportCashOut(model);
                }
            }

            ViewBag.FilterBankList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportBank);
            ViewBag.FilterAccountList = common.ToSelectList(BankFacilityBusinessLogic.getInstance().getBankFacilityDDL(), "ID", "NAME", model.FilterReportAccount);
            ViewBag.FilterCashInList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(), "ID", "NAME", model.FilterReportCashIn);
            ViewBag.FilterCashOutList = common.ToSelectList(AccountDetailsBusinessLogic.getInstance().getAccountDetailDDL(), "ID", "NAME", model.FilterReportCashOut);
            return View(model);
        }

        [HttpPost]
        public ActionResult ReportOfBank(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfBank(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccount(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfAccount(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccountCashIn(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfAccountCashIn(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult ReportOfAccountCashOut(DashboardViewModels model)
        {
            try
            {
                return Json(DashboardBusinessLogic.getInstance().getReportOfCashOut(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Logging.getInstance().CreateLogError(e);
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error403()
        {
            return View();
        }

        private void ExcelReportBank(DashboardViewModels model)
        {
            ExcelPackage Package = new ExcelPackage();
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Data");
            ExcelWorksheet wsComChart = Package.Workbook.Worksheets.Add("Chart");

            OfficeOpenXml.Style.ExcelBorderStyle DefaultBorder = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#00b0f0");

            ws.Cells["A1"].LoadFromText("Month");
            ws.Cells["A1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["B1"].LoadFromText("Cash In");
            ws.Cells["B1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["C1"].LoadFromText("Cash Out");
            ws.Cells["C1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            int idx = 1;
            var list = DashboardBusinessLogic.getInstance().getReportOfBank(model);
            foreach(var item in list)
            {
                idx++;
                ws.Cells["A" + idx.ToString()].LoadFromText(item.Month);
                ws.Cells["A" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["A" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["B" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Cash_In)).ToString());
                ws.Cells["B" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["B" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["B" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["C" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Cash_Out)).ToString());
                ws.Cells["C" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["C" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["C" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;
            }

            List<DataSerie> SeriesList = new List<DataSerie>();
            SeriesList.Add(new DataSerie() { name = "Month", Series = ws.Cells["A" + (idx - 1) + ":A" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "CashIn", Series = ws.Cells["B" + (idx - 1) + ":B" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "CashOut", Series = ws.Cells["C" + (idx - 1) + ":C" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });

            ExcelChart chart = AddBarChart(wsComChart, "ReportOfBank", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered, SeriesList);
            chart.Title.Text = "Report Of Bank";
            chart.SetPosition(0, 0, 0, 0);
            chart.SetSize(BarOptions.width, BarOptions.Height);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReportOfBank_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
            Response.BinaryWrite(Package.GetAsByteArray());
            Response.End();
        }
        private void ExcelReportAccount(DashboardViewModels model)
        {
            ExcelPackage Package = new ExcelPackage();
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Data");
            ExcelWorksheet wsComChart = Package.Workbook.Worksheets.Add("Chart");

            OfficeOpenXml.Style.ExcelBorderStyle DefaultBorder = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#00b0f0");

            ws.Cells["A1"].LoadFromText("Month");
            ws.Cells["A1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["B1"].LoadFromText("Cash In");
            ws.Cells["B1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["C1"].LoadFromText("Cash Out");
            ws.Cells["C1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            int idx = 1;
            var list = DashboardBusinessLogic.getInstance().getReportOfAccount(model);
            foreach (var item in list)
            {
                idx++;
                ws.Cells["A" + idx.ToString()].LoadFromText(item.Month);
                ws.Cells["A" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["A" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["B" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Cash_In)).ToString());
                ws.Cells["B" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["B" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["B" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["C" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Cash_Out)).ToString());
                ws.Cells["C" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["C" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["C" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;
            }

            List<DataSerie> SeriesList = new List<DataSerie>();
            SeriesList.Add(new DataSerie() { name = "Month", Series = ws.Cells["A" + (idx - 1) + ":A" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "CashIn", Series = ws.Cells["B" + (idx - 1) + ":B" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "CashOut", Series = ws.Cells["C" + (idx - 1) + ":C" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });

            ExcelChart chart = AddBarChart(wsComChart, "ReportOfAccount", OfficeOpenXml.Drawing.Chart.eChartType.Line, SeriesList);
            chart.Title.Text = "Report Of Account";
            chart.SetPosition(0, 0, 0, 0);
            chart.SetSize(BarOptions.width, BarOptions.Height);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReportOfAccount_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
            Response.BinaryWrite(Package.GetAsByteArray());
            Response.End();
        }

        private void ExcelReportCashIn(DashboardViewModels model)
        {
            ExcelPackage Package = new ExcelPackage();
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Data");
            ExcelWorksheet wsComChart = Package.Workbook.Worksheets.Add("Chart");

            OfficeOpenXml.Style.ExcelBorderStyle DefaultBorder = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#00b0f0");

            ws.Cells["A1"].LoadFromText("Month");
            ws.Cells["A1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["B1"].LoadFromText("Operation");
            ws.Cells["B1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["C1"].LoadFromText("Accounting");
            ws.Cells["C1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            int idx = 1;
            var list = DashboardBusinessLogic.getInstance().getReportOfAccountCashIn(model);
            foreach (var item in list)
            {
                idx++;
                ws.Cells["A" + idx.ToString()].LoadFromText(item.Month);
                ws.Cells["A" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["A" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["B" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Accounting)).ToString());
                ws.Cells["B" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["B" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["B" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["C" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Operation)).ToString());
                ws.Cells["C" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["C" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["C" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;
            }

            List<DataSerie> SeriesList = new List<DataSerie>();
            SeriesList.Add(new DataSerie() { name = "Month", Series = ws.Cells["A" + (idx - 1) + ":A" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "Accounting", Series = ws.Cells["B" + (idx - 1) + ":B" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "Operation", Series = ws.Cells["C" + (idx - 1) + ":C" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });

            ExcelChart chart = AddBarChart(wsComChart, "ReportOfAccountCashIn", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered, SeriesList);
            chart.Title.Text = "Report Of Account Cash In";
            chart.SetPosition(0, 0, 0, 0);
            chart.SetSize(BarOptions.width, BarOptions.Height);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReportOfAccountCashIn_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
            Response.BinaryWrite(Package.GetAsByteArray());
            Response.End();
        }

        private void ExcelReportCashOut(DashboardViewModels model)
        {
            ExcelPackage Package = new ExcelPackage();
            ExcelWorksheet ws = Package.Workbook.Worksheets.Add("Data");
            ExcelWorksheet wsComChart = Package.Workbook.Worksheets.Add("Chart");

            OfficeOpenXml.Style.ExcelBorderStyle DefaultBorder = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#00b0f0");

            ws.Cells["A1"].LoadFromText("Month");
            ws.Cells["A1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["B1"].LoadFromText("Operation");
            ws.Cells["B1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            ws.Cells["C1"].LoadFromText("Accounting");
            ws.Cells["C1"].Style.Font.SetFromFont(new Font("Cambria", 10));
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

            int idx = 1;
            var list = DashboardBusinessLogic.getInstance().getReportOfAccountCashIn(model);
            foreach (var item in list)
            {
                idx++;
                ws.Cells["A" + idx.ToString()].LoadFromText(item.Month);
                ws.Cells["A" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["A" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["A" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["A" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["B" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Accounting)).ToString());
                ws.Cells["B" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["B" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["B" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["B" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["B" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;

                ws.Cells["C" + idx.ToString()].LoadFromText((Convert.ToDecimal(item.Operation)).ToString());
                ws.Cells["C" + idx.ToString()].Style.Numberformat.Format = "#,##";
                ws.Cells["C" + idx.ToString()].Style.Font.SetFromFont(new Font("Cambria", 10));
                ws.Cells["C" + idx.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                ws.Cells["C" + idx.ToString()].Style.Border.Bottom.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Left.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Top.Style = DefaultBorder;
                ws.Cells["C" + idx.ToString()].Style.Border.Right.Style = DefaultBorder;
            }

            List<DataSerie> SeriesList = new List<DataSerie>();
            SeriesList.Add(new DataSerie() { name = "Month", Series = ws.Cells["A" + (idx - 1) + ":A" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "Accounting", Series = ws.Cells["B" + (idx - 1) + ":B" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });
            SeriesList.Add(new DataSerie() { name = "Operation", Series = ws.Cells["C" + (idx - 1) + ":C" + idx], xSeries = ws.Cells["B" + (idx - 1) + ":C" + idx] });

            ExcelChart chart = AddBarChart(wsComChart, "ReportOfAccountCashOut", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered, SeriesList);
            chart.Title.Text = "Report Of Account Cash Out";
            chart.SetPosition(0, 0, 0, 0);
            chart.SetSize(BarOptions.width, BarOptions.Height);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ReportOfAccountCashOut_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx");
            Response.BinaryWrite(Package.GetAsByteArray());
            Response.End();
        }
        private class ChartOptions
        {
            public int width { get; set; }
            public int Height { get; set; }
        }
        private ChartOptions BarOptions = new ChartOptions() { width = 600, Height = 500 };

        private ExcelChart AddBarChart(ExcelWorksheet ws, string NameChart, eChartType type, List<DataSerie> ListSeries)
        {
            ExcelBarChart chart = ws.Drawings.AddChart(NameChart, eChartType.ColumnClustered) as ExcelBarChart;

            chart.Title.Font.Size = 18;
            chart.Title.Font.Bold = true;

            chart.Legend.Remove();

            chart.YAxis.Border.Fill.Style = eFillStyle.NoFill;

            chart.YAxis.Font.Size = 9;

            chart.XAxis.MajorTickMark = eAxisTickMark.None;
            chart.XAxis.MinorTickMark = eAxisTickMark.None;

            chart.YAxis.MajorTickMark = eAxisTickMark.None;
            chart.YAxis.MinorTickMark = eAxisTickMark.None;

            for (int i = 0; i < ListSeries.Count; i++)
            {
                DataSerie dat = ListSeries[i];
                ExcelBarChartSerie ser = (ExcelBarChartSerie)(chart.Series.Add(dat.Series, dat.xSeries));
                ser.DataLabel.ShowValue = true;
                ser.DataLabel.Position = eLabelPosition.OutEnd;
                ser.Header = dat.name;

                if (dat.name == "Move")
                {
                    ser.DataLabel.Border.Fill.Color = Color.Red;
                    ser.DataLabel.Border.LineStyle = eLineStyle.Solid;
                }
            }

            chart.YAxis.Format = string.Format("{0};{1}", "#,##0", "(#,##0)");

            return chart;
        }

        public class DataSerie
        {
            public ExcelRangeBase Series { get; set; }

            public ExcelRangeBase xSeries { get; set; }

            public string name { get; set; }
        }
    }
}