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
            ws.Cells[2, 1].Value = "Время";

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
                    var value = schedule.WeekTypeName + " " + schedule.LecturerName + " " +
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


        public Byte[] CreateTableBySchedulesAndPairNumbersForGroups(string title, IList<ScheduleViewModel> schedules, int[] pairNumbers, IList<GroupViewModel> groups, IList<CourseViewModel> courses, int facultyId, int studyYearId, int semesterId)
        {
            var days = new List<string>() { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб" };

            var p = new ExcelPackage();
            
            p.Workbook.Properties.Author = "Система составления расписания";
            p.Workbook.Properties.Title = title;

            var loadGroups = groups == null ? true : false;
            var loadSchedules = schedules == null ? true : false;

            for (var ii = 1; ii <= courses.Count(); ++ii)
            {
                if (loadGroups == true)
                {
                    groups = DataService.GetGroupsForFaculty(facultyId, courses[ii - 1].Id)
                             .Select(x => new GroupViewModel(x)).ToList();
                }
                if (loadSchedules == true)
                {
                    schedules = DataService.GetSchedulesForFaculty(facultyId, courses[ii - 1].Id, studyYearId, semesterId)
                                .Select(x => new ScheduleViewModel(x)).ToList();
                }

                p.Workbook.Worksheets.Add(courses[ii-1].Name);
                ExcelWorksheet ws = p.Workbook.Worksheets[ii];

                ws.Name = courses[ii-1].Name;
                ws.Cells.Style.Font.Size = 11;
                ws.Cells.Style.Font.Name = "Calibri";

                DataTable dt = CreateDataTable(2 + days.Count() * pairNumbers.Count(), 2 + groups.Count());

                ws.Cells[1, 1].Value = title;
                ws.Cells[1, 1, 1, dt.Columns.Count].Merge = true;
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.Font.Bold = true;
                ws.Cells[1, 1, 1, dt.Columns.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //ws.Cells[1, 1, 1, dt.Columns.Count].AutoFitColumns();

                ws.Cells[2, 1].Value = "День";
                ws.Cells[2, 2].Value = "№ пары";

                ws.Column(1).Width = 5;
                ws.Column(2).Width = 10;


                //Разметка таблицы
                var border1 = ws.Cells[2, 1].Style.Border;
                border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Medium;
                border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Medium;

                border1 = ws.Cells[2, 2].Style.Border;
                border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Medium;
                border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Medium;

                border1 = ws.Cells[1, 1, 1, dt.Columns.Count].Style.Border;
                border1.Left.Style = border1.Right.Style = ExcelBorderStyle.Medium;
                border1.Bottom.Style = border1.Top.Style = ExcelBorderStyle.Medium;


                for (int i = 1; i <= 1 + days.Count() * pairNumbers.Count(); ++i)
                {
                    for (int j = 1; j <= groups.Count(); ++j)
                    {
                        var cell = ws.Cells[1 + i, 2 + j];
                        var border = cell.Style.Border;
                        border.Left.Style = border.Right.Style = ExcelBorderStyle.Medium;
                        border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;

                        if ((i - 1) % pairNumbers.Count() == 0)
                        {
                            border.Bottom.Style = ExcelBorderStyle.Medium;
                        }

                        cell.Style.WrapText = true;
                    }
                }

                for (int i = 0; i < groups.Count; ++i)
                {
                    ws.Column(3 + i).Width = 20;
                    ws.Cells[2, 3 + i].Value = groups[i].Code;
                }

                for (int i = 1; i <= days.Count(); ++i)
                {
                    ws.Cells[3 + pairNumbers.Count() * (i - 1), 1, 3 + pairNumbers.Count() * i - 1, 1].Merge = true;
                    var cell = ws.Cells[3 + pairNumbers.Count() * (i - 1), 1, 3 + pairNumbers.Count() * i - 1, 1];
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Medium;
                    border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Medium;
                    cell.Value = days[i - 1];
                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                for (int i = 1; i <= days.Count() * pairNumbers.Count(); ++i)
                {
                    var cell = ws.Cells[2 + i, 2];
                    var border = cell.Style.Border;
                    border.Left.Style = border.Right.Style = ExcelBorderStyle.Medium;

                    cell.Value = (i - 1) % pairNumbers.Count() + 1;
                    ws.Row(2 + i).Height = 15;

                    border.Bottom.Style = border.Top.Style = ExcelBorderStyle.Thin;
                    if (i % pairNumbers.Count() == 0)
                    {
                        border.Bottom.Style = ExcelBorderStyle.Medium;
                    }
                }


                var pairInCellCount = new int[days.Count() * pairNumbers.Count() + 10, groups.Count() + 10];
                var scheduleInCell = new ScheduleViewModel[5, days.Count() * pairNumbers.Count() + 10, groups.Count() + 10];
                var pairInColumnCount = new int[groups.Count() + 10];
                var pairInRowCount = new int[days.Count() * pairNumbers.Count() + 10];

                //Заполнение таблицы занятиями
                foreach (var schedule in schedules)
                {
                    int day = schedule.DayOfWeek;
                    int pairNumber = schedule.Pair;


                    int rowIndex = 2 + (day - 1) * pairNumbers.Count() + pairNumber;
                    int columnIndex = 0;
                    for (int j = 0; j < groups.Count(); ++j)
                    {
                        if (schedule.GroupCodes.Any(x => x == groups[j].Code))
                        {
                            columnIndex = 3 + j;

                            pairInCellCount[rowIndex, columnIndex]++;
                            if (30 * pairInCellCount[rowIndex, columnIndex] > ws.Row(rowIndex).Height)
                                ws.Row(rowIndex).Height = 30 * pairInCellCount[rowIndex, columnIndex];

                            var cell = ws.Cells[rowIndex, columnIndex];

                            var value = "";
                            if (pairInCellCount[rowIndex, columnIndex] > 1)
                                value = " / ";

                            value += schedule.WeekTypeName + " Ауд №" + schedule.AuditoriumNumber + " " + schedule.LecturerName + " " +
                                          schedule.TutorialName + " (" + schedule.TutorialTypeName + ") ";// +schedule.GroupCodes.Aggregate((x, y) => x + ", " + y);

                            cell.Value += value;
                            cell.Style.WrapText = true;


                            pairInColumnCount[columnIndex]++;
                            pairInRowCount[rowIndex]++;

                            if (schedule.WeekTypeName == "Ч")
                            {
                                scheduleInCell[1, rowIndex, columnIndex] = schedule;
                                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                            }
                            if (schedule.WeekTypeName == "З")
                            {
                                scheduleInCell[2, rowIndex, columnIndex] = schedule;
                                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Bottom;
                            }
                            if (schedule.WeekTypeName == "Л")
                            {
                                scheduleInCell[3, rowIndex, columnIndex] = schedule;
                                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            }
                        }
                    }
                }

                //Сжатие похожих клеток в таблице.
                for (int i = 1; i <= days.Count() * pairNumbers.Count(); ++i)
                {
                    var rowIndex = 2 + i;
                    var currentColumn = 3;
                    for (var k = 4; k <= 4 + groups.Count(); ++k)
                    {
                        var currentEveryWeekSchedule = scheduleInCell[3, rowIndex, currentColumn];
                        var nextEveryWeekSchedule = scheduleInCell[3, rowIndex, k];
                        var currentEvenWeekSchedule = scheduleInCell[1, rowIndex, currentColumn];
                        var nextEvenWeekSchedule = scheduleInCell[1, rowIndex, k];
                        var currentOddWeekSchedule = scheduleInCell[2, rowIndex, currentColumn];
                        var nextOddWeekSchedule = scheduleInCell[2, rowIndex, k];

                        if (!(currentOddWeekSchedule == null && nextOddWeekSchedule == null &&
                            nextEvenWeekSchedule == null && currentEvenWeekSchedule == null &&
                            currentEveryWeekSchedule != null && nextEveryWeekSchedule != null))
                        {
                            if (currentEveryWeekSchedule != null && nextEveryWeekSchedule != null)
                            {
                            if (!(nextEveryWeekSchedule.AuditoriumNumber == currentEveryWeekSchedule.AuditoriumNumber))
                            {
                                if (currentColumn < k - 1)
                                {
                                    ws.Cells[rowIndex, currentColumn, rowIndex, k-1].Merge = true;
                                }
                                currentColumn = k;
                            }
                            }else{
                                if (currentColumn < k - 1)
                                {
                                    ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                }
                                currentColumn = k;
                            }
                        }
                    }
                    currentColumn = 3;
                    for (var k = 4; k <= 4 + groups.Count(); ++k)
                    {
                        var currentEveryWeekSchedule = scheduleInCell[3, rowIndex, currentColumn];
                        var nextEveryWeekSchedule = scheduleInCell[3, rowIndex, k];
                        var currentEvenWeekSchedule = scheduleInCell[1, rowIndex, currentColumn];
                        var nextEvenWeekSchedule = scheduleInCell[1, rowIndex, k];
                        var currentOddWeekSchedule = scheduleInCell[2, rowIndex, currentColumn];
                        var nextOddWeekSchedule = scheduleInCell[2, rowIndex, k];

                        if (!(currentOddWeekSchedule == null && nextOddWeekSchedule == null &&
                            nextEveryWeekSchedule == null && currentEveryWeekSchedule == null &&
                            currentEvenWeekSchedule != null && nextEvenWeekSchedule != null))
                        {
                            if (currentEvenWeekSchedule != null && nextEvenWeekSchedule != null)
                            {
                                if (!(nextEvenWeekSchedule.AuditoriumNumber == currentEvenWeekSchedule.AuditoriumNumber))
                                {
                                    if (currentColumn < k - 1)
                                    {
                                        ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                    }
                                    currentColumn = k;
                                }
                            }
                            else
                            {
                                if (currentColumn < k - 1)
                                {
                                    ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                }
                                currentColumn = k;
                            }
                        }
                    }
                    currentColumn = 3;
                    for (var k = 4; k <= 4 + groups.Count(); ++k)
                    {
                        var currentEveryWeekSchedule = scheduleInCell[3, rowIndex, currentColumn];
                        var nextEveryWeekSchedule = scheduleInCell[3, rowIndex, k];
                        var currentEvenWeekSchedule = scheduleInCell[1, rowIndex, currentColumn];
                        var nextEvenWeekSchedule = scheduleInCell[1, rowIndex, k];
                        var currentOddWeekSchedule = scheduleInCell[2, rowIndex, currentColumn];
                        var nextOddWeekSchedule = scheduleInCell[2, rowIndex, k];

                        if (!(currentOddWeekSchedule != null && nextOddWeekSchedule != null &&
                            nextEveryWeekSchedule == null && currentEveryWeekSchedule == null &&
                            currentEvenWeekSchedule == null && nextEvenWeekSchedule == null))
                        {
                            if (currentOddWeekSchedule != null && nextOddWeekSchedule != null)
                            {
                                if (!(nextOddWeekSchedule.AuditoriumNumber == currentOddWeekSchedule.AuditoriumNumber))
                                {
                                    if (currentColumn < k - 1)
                                    {
                                        ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                    }
                                    currentColumn = k;
                                }
                            }
                            else
                            {
                                if (currentColumn < k - 1)
                                {
                                    ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                }
                                currentColumn = k;
                            }
                        }
                    }
                    currentColumn = 3;
                    for (var k = 4; k <= 4 + groups.Count(); ++k)
                    {
                        var currentEveryWeekSchedule = scheduleInCell[3, rowIndex, currentColumn];
                        var nextEveryWeekSchedule = scheduleInCell[3, rowIndex, k];
                        var currentEvenWeekSchedule = scheduleInCell[1, rowIndex, currentColumn];
                        var nextEvenWeekSchedule = scheduleInCell[1, rowIndex, k];
                        var currentOddWeekSchedule = scheduleInCell[2, rowIndex, currentColumn];
                        var nextOddWeekSchedule = scheduleInCell[2, rowIndex, k];

                        if (!(currentOddWeekSchedule != null && nextOddWeekSchedule != null &&
                            nextEveryWeekSchedule == null && currentEveryWeekSchedule == null &&
                            currentEvenWeekSchedule != null && nextEvenWeekSchedule != null))
                        {
                            if (currentOddWeekSchedule != null && nextOddWeekSchedule != null && nextEveryWeekSchedule != null && currentEveryWeekSchedule != null)
                            {
                                if (!(nextEvenWeekSchedule.AuditoriumNumber == currentEvenWeekSchedule.AuditoriumNumber &&
                                      nextOddWeekSchedule.AuditoriumNumber == currentOddWeekSchedule.AuditoriumNumber))
                                {
                                    if (currentColumn < k - 1)
                                    {
                                        ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                    }
                                    currentColumn = k;
                                }
                            }
                            else
                            {
                                if (currentColumn < k - 1)
                                {
                                    ws.Cells[rowIndex, currentColumn, rowIndex, k - 1].Merge = true;
                                }
                                currentColumn = k;
                            }
                        }
                    }
                }
               

                //Сжатие пустых строк и столбцов для экономии места
                /*
                for (int i = 1; i <= days.Count() * pairNumbers.Count(); ++i)
                {
                    var rowIndex = 2 + i;
                    if(pairInRowCount[rowIndex] == 0)
                        ws.Row(rowIndex).Height = 1;
                }

                for (int i = 1; i <= groups.Count(); ++i)
                {
                    var columnIndex = 2 + i;
                    ws.Cells[2, columnIndex].Value = "";
                    if (pairInColumnCount[columnIndex] == 0)
                        ws.Column(columnIndex).Width = 0;
                }*/
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
                var times = DataService.GetTimesByIds(timeIds).ToList().Select(x => new TimeViewModel(x)).ToList();

                var fileName = string.Format("Расписание для " + request.LecturerQuery + "-{0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);

                var memoryStream = CreateTableBySchedulesAndTimes(fileName, times, schedules);

                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }

            return null;
        }

        public FileResult GetReportForFaculty(int? branchId, int? facultyId, int? studyYearId, int? semesterId)
        {
            if (facultyId.HasValue && branchId.HasValue && studyYearId.HasValue && semesterId.HasValue)
            {
                var pairNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
         
                var courses = DataService.GetCources(branchId.Value)
                  .Select(x => new CourseViewModel(x)).ToList();

                var faculty = DataService.GetFacultyById(facultyId.Value);

                var fileName = string.Format("Расписание для факультета: " + faculty.Name + " - {0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);
                var memoryStream = CreateTableBySchedulesAndPairNumbersForGroups(fileName, null, pairNumbers, null, courses, facultyId.Value, studyYearId.Value, semesterId.Value);
                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }

            return null;
        }

        public FileResult GetReportForCourse(int? branchId, int? facultyId, int? studyTypeId, int? courseId, int? studyYearId, int? semesterId)
        {
            if (branchId.HasValue && facultyId.HasValue && courseId.HasValue && studyYearId.HasValue && semesterId.HasValue && studyTypeId.HasValue)
            {
                var groups = DataService.GetGroupsForFaculty(facultyId.Value, courseId.Value, studyTypeId.Value).Select(x => new GroupViewModel(x)).ToList();

                var groupIdsList = groups.Select(x => x.Id).ToArray();

                var pairNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                var schedules = DataService
                    .GetSchedules(facultyId.Value,
                                  courseId.Value,
                                  groupIdsList,
                                  studyYearId.Value,
                                  semesterId.Value)
                    .Select(x => new ScheduleViewModel(x)).ToList();

                var courses = DataService.GetCources(branchId.Value)
                  .Where(x => x.Id == courseId.Value)
                  .Select(x => new CourseViewModel(x)).ToList();

                var fileName = string.Format("Расписание для курса: " + courses[0].Name + " - {0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);
                var memoryStream = CreateTableBySchedulesAndPairNumbersForGroups(fileName, schedules, pairNumbers, groups, courses, facultyId.Value, studyYearId.Value, semesterId.Value);
                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }

            return null;
        }

        public FileResult GetReportForGroups(int? branchId, int? facultyId, int? courseId, string groupIds, int? studyYearId, int? semesterId)
        {
            
            if (branchId.HasValue && facultyId.HasValue && courseId.HasValue && studyYearId.HasValue && semesterId.HasValue)
            {

                var groupIdsList = GetListFromString(groupIds).ToArray();
                var groups = DataService.GetGroupsByIds(groupIdsList).Select(x => new GroupViewModel(x)).ToList();
                var pairNumbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                var schedules = DataService
                    .GetSchedules(facultyId.Value,
                                  courseId.Value,
                                  groupIdsList,
                                  studyYearId.Value,
                                  semesterId.Value)
                    .Select(x => new ScheduleViewModel(x)).ToList();

                var courses = DataService.GetCources(branchId.Value)
                    .Where(x => x.Id == courseId.Value)
                    .Select(x => new CourseViewModel(x)).ToList();

                var groupCodes = "";
                foreach (var group in groups)
                    groupCodes += group.Code + " ";

                var fileName = string.Format("Расписание для групп: " + groupCodes + " - {0:yyyy-MM-dd-HH-mm-ss}", DateTime.UtcNow);
                var memoryStream = CreateTableBySchedulesAndPairNumbersForGroups(fileName, schedules, pairNumbers, groups, courses, facultyId.Value, studyYearId.Value, semesterId.Value);
                return base.File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName + ".xlsx");
            }

            return null;
        }
    }
}
