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
using Timetable.Site.Areas.Students.Models.RequestModels;
using Timetable.Site.Infrastructure;

namespace Timetable.Site.Areas.Students.Controllers
{
    public class ReportController : BaseController
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

        public Byte[] CreateTableBySchedulesAndPairNumbers(string title, IList<ScheduleViewModel> schedules, int[] pairNumbers)
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

            DataTable dt = CreateDataTable(2 + pairNumbers.Count(), 1 + days.Count());

            ws.Cells[1, 1].Value = title;
            ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
            ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
            ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //ws.Cells[1, 1, 1, dt.Columns.Count].AutoFitColumns();
            ws.Cells[2, 1].Value = "№ пары";

            ws.Column(1).Width = 10;

            var border1 = ws.Cells[2, 1].Style.Border;
            border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Thin;
            border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Thin;

            border1 = ws.Cells[1, 1, 1, dt.Columns.Count].Style.Border;
            border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Thin;
            border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Thin;

            for (int i = 0; i < pairNumbers.Count(); ++i)
            {
                for (int j = 0; j < days.Count(); ++j)
                {
                    var cell = ws.Cells[3 + i, 2 + j];
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;
                }
            }


            for (int i = 0; i < pairNumbers.Count(); ++i)
            {
                ws.Row(3 + i).Height = 50;
                var cell = ws.Cells[3 + i, 1];
                cell.Value = pairNumbers[i];
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
                var pairNumberViewId = 0;
                var i = 1;
                foreach (var pairNumber in pairNumbers)
                {
                    if (schedule.Pair == pairNumber)
                    {
                        pairNumberViewId = i;
                        break;
                    }
                    i++;
                }
                if (pairNumberViewId > 0)
                {
                    var cell = ws.Cells[2 + pairNumberViewId, 1 + schedule.DayOfWeek];

                    var timeStr = string.Format("{0}:{1}", schedule.Time.Start.Hours, schedule.Time.Start.Minutes) + "-"
                        + string.Format("{0}:{1}", schedule.Time.End.Hours, schedule.Time.End.Minutes);

                    var value = timeStr + " " + schedule.WeekTypeName + " " + schedule.LecturerName + " " +
                                schedule.TutorialName + " (" + schedule.TutorialTypeName + ") " + schedule.GroupCodes.Aggregate((x, y) => x + ", " + y);
                    cell.Value = value;
                    cell.Style.WrapText = true;
                }
            }
            return p.GetAsByteArray();
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

        public FileResult GetReportForAuditorium(int? auditoriumId, int? buildingId)
        {

            if (auditoriumId.HasValue && buildingId.HasValue)
            {
                var auditorium = DataService.GetAuditoriumById(auditoriumId.Value);

                var studyYear = DataService.GetStudyYear(DateTime.Now);
                var semester = DataService.GetSemesterForTime(DateTime.Now);

                var schedules = new List<ScheduleViewModel>();
                if (studyYear != null && semester != null)
                {
                    schedules = DataService
                         .GetSchedulesForAuditorium(auditorium.Id, studyYear.Id, semester.Id)
                         .ToList()
                         .Select(x => new ScheduleViewModel(x)).ToList();
                }

                //теперь выводятся все звонки для здания

                var times = DataService.GetTimes(buildingId.Value).ToList().Select(x => new TimeViewModel(x)).ToList();
                //раньше были звонки(строки) у запланированных предметов (пустые строки пропускались в отчете)
                //var timeIds = schedules.Select(x => x.TimeId).Distinct().ToList();
                //var times = DataService.GetTimesByIds(timeIds).ToList().Select(x => new TimeViewModel(x)).ToList();

                var fileName = string.Format("Расписание для Ауд. №" + auditorium.Number + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

                var memoryStream = CreateTableBySchedulesAndTimes(fileName, times, schedules);

                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }
            return null;
        }


        public FileResult GetReportForLecturer(int lecturerId)
        {
            var studyYear = DataService.GetStudyYear(DateTime.Now);
            var semester = DataService.GetSemesterForTime(DateTime.Now);

            var schedules = DataService.GetSchedulesForLecturer(lecturerId, studyYear.Id, semester.Id)
               .ToList()
               .Select(x => new ScheduleViewModel(x)).ToList();

            var fileName = string.Format("Расписание для преподавателя" + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

            var memoryStream = CreateTableBySchedulesAndPairNumbers(fileName, schedules, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });

            return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");

        }


        public FileResult GetReportForGroup(int? facultyId, int? courseId, int? groupId)
        {

            if (facultyId.HasValue && courseId.HasValue && groupId.HasValue)
            {
                var group = DataService.GetGroupsByIds(new int[] { groupId.Value }).FirstOrDefault();
                var studyYear = DataService.GetStudyYear(DateTime.Now);
                var semester = DataService.GetSemesterForTime(DateTime.Now);

                if (studyYear != null && semester != null)
                {
                    var schedules = DataService.GetSchedulesForGroups(facultyId.Value, courseId.Value, new int[]{groupId.Value}, studyYear.Id, semester.Id)
                       .ToList()
                       .Select(x => new ScheduleViewModel(x)).ToList();

                    var fileName = string.Format("Расписание для группы " + group.Code + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

                    var memoryStream = CreateTableBySchedulesAndPairNumbers(fileName, schedules, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });

                    return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
                }
            }

            return null;
        }
    }
}
