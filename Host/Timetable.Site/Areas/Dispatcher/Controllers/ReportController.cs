using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Timetable.Site.Models.ResponseModels;
using Timetable.Site.Models.ViewModels;
using Timetable.Site.Areas.Dispatcher.Models.RequestModels;


namespace Timetable.Site.Areas.Dispatcher.Controllers
{
    public class ReportController : AuthorizedController
    {
        private static DataTable CreateDataTable(int rowCount, int colCount)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < colCount; i++)
                dt.Columns.Add(i.ToString());


            for (int i = 0; i < rowCount; i++)
            {
                DataRow dr = dt.NewRow();
                foreach (DataColumn dc in dt.Columns)
                    dr[dc.ToString()] = i;

                dt.Rows.Add(dr);
            }
            return dt;
        }


        public Byte[] CreateTableBySchedulesAndTimes(string title, IList<TimeViewModel> times, IList<ScheduleViewModel> schedules)
        {
            var days = new List<string>() { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота" };

            var p = new ExcelPackage();
            p.Workbook.Properties.Author = "Система составления расписания";
            p.Workbook.Properties.Title = title;
            p.Workbook.Worksheets.Add("Расписание");
            ExcelWorksheet ws = p.Workbook.Worksheets[1];

            ws.Name = "Расписание";
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            DataTable dt = CreateDataTable(2 + times.Count(), 1 + days.Count());

            ws.Cells[1, 1].Value = title;
            ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
            ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
            ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells[1, 1, 1, dt.Columns.Count].AutoFitColumns();
            ws.Cells[2, 1].Value = "Время";

            ws.Column(1).Width = 10;

            var border1 = ws.Cells[2, 1].Style.Border;
            border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Thin;
            border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Thin;

            border1 = ws.Cells[1, 1, 1, dt.Columns.Count].Style.Border;
            border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Thin;
            border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Thin;

            for (int i = 0; i < times.Count(); ++i)
            {
                for (int j = 0; j < days.Count(); ++j)
                {
                    var cell = ws.Cells[3 + i, 2 + j];
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;
                }
            }


            for (int i = 0; i < times.Count(); ++i)
            {
                ws.Row(3 + i).Height = 50;
                var cell = ws.Cells[3 + i, 1];
                cell.Value = times[i].Start + "-" + times[i].End;
                var border = cell.Style.Border;
                cell.Style.WrapText = true;
                border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;
            }

            for (int i = 0; i < days.Count(); ++i)
            {
                ws.Column(2 + i).Width = 20;
                var cell = ws.Cells[2, 2 + i];
                cell.Value = days[i];
                cell.Style.WrapText = true;
                var border = cell.Style.Border;
                border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;
            }

            foreach (var schedule in schedules)
            {
                var timeViewId = 0;
                var i = 1;
                foreach (var t in times)
                {
                    if (schedule.TimeId == t.Id)
                    {
                        timeViewId = i;
                        break;
                    }
                    i++;
                }
                if (timeViewId > 0)
                {
                    var cell = ws.Cells[2 + timeViewId, 1 + schedule.DayOfWeek];
                    var value = schedule.WeekTypeName + " " + schedule.LecturerName + " " +
                                schedule.TutorialName + " (" + schedule.TutorialTypeName + ") " + schedule.GroupCodes.Aggregate((x, y) => x + ", " + y);
                    cell.Value = value;
                    cell.Style.WrapText = true;
                }
            }
            return p.GetAsByteArray();
        }

        public FileResult GetReportForAuditorium(ReportForAuditoriumRequest request)
        {
            var auditorium = DataService.GetAuditoriumById(request.AuditoriumId);

            var schedules = DataService
                 .GetSchedulesForAuditorium(request.AuditoriumId, request.StudyYearId, request.Semester)
                 .ToList()
                 .Select(x => new ScheduleViewModel(x)).ToList();

            var timeIds = schedules.Select(x => x.TimeId).Distinct().ToList();
            var times = DataService.GetTimesByIds(timeIds).ToList().Select(x => new TimeViewModel(x)).ToList();

            var fileName = string.Format("Расписание для Ауд. №" + auditorium.Number + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

            var memoryStream = CreateTableBySchedulesAndTimes(fileName, times, schedules);

            return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName +".xlsx");
        }


        public FileResult GetReportForLecturer(ReportForLecturerRequest request)
        {
            var currentLecturer = DataService.GetLecturerBySearchQuery(request.LecturerQuery);

            if (currentLecturer != null)
            {
                var schedules = DataService.GetSchedulesForLecturer(currentLecturer.Id, request.StudyYearId, request.Semester)
                   .ToList()
                   .Select(x => new ScheduleViewModel(x)).ToList();

                var timeIds = schedules.Select(x => x.TimeId).Distinct().ToList();
                var times = DataService.GetTimesByIds(timeIds).ToList().Select(x => new TimeViewModel(x)).OrderBy(x => x.Start).ToList();

                var fileName = string.Format("Расписание для " + request.LecturerQuery + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

                var memoryStream = CreateTableBySchedulesAndTimes(fileName, times, schedules);

                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }

            return null;
        }
    }
}
