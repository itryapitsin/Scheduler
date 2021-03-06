﻿using System;
using System.Collections.Generic;
using Timetable.Data.Models.Scheduler;

namespace Timetable.Data.Models.Personalization
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public int RoleId { get; set; }

        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }

        public virtual Branch CreatorSelectedBranch { get; set; }
        public int? CreatorSelectedBranchId { get; set; }
        public virtual Faculty CreatorSelectedFaculty { get; set; }
        public int? CreatorSelectedFacultyId { get; set; }
        public virtual Course CreatorSelectedCourse { get; set; }
        public int? CreatorSelectedCourseId { get; set; }
        public virtual List<Group> CreatorSelectedGroups { get; set; }
        public virtual Building CreatorSelectedBuilding { get; set; }
        public int? CreatorSelectedBuildingId { get; set; }
        public virtual StudyYear CreatorSelectedStudyYear { get; set; }
        public int? CreatorSelectedStudyYearId { get; set; }
        public virtual Semester CreatorSelectedSemester { get; set; }
        public int? CreatorSelectedSemesterId { get; set; }
        public virtual Auditorium CreatorSelectedAuditorium { get; set; }
        public int? CreatorSelectedAuditoriumId { get; set; }
        public virtual StudyType CreatorSelectedStudyType { get; set; }
        public int? CreatorSelectedStudyTypeId { get; set; }


        public virtual ScheduleType PlanningModalSelectedScheduleType { get; set; }
        public int? PlanningModalSelectedScheduleTypeId { get; set; }
        public virtual Building PlanningModalSelectedBuilding { get; set; }
        public int? PlanningModalSelectedBuildingId { get; set; }
        public virtual WeekType PlanningModalSelectedWeekType { get; set; }
        public int? PlanningModalSelectedWeekTypeId { get; set; }
        public virtual Auditorium PlanningModalSelectedAuditorium { get; set; }
        public int? PlanningModalSelectedAuditoriumId { get; set; }
        public string PlanningModalSelectedSubGroup { get; set; }

        public virtual Building AuditoriumScheduleSelectedBuilding { get; set; }
        public int? AuditoriumScheduleSelectedBuildingId { get; set; }
        public virtual Auditorium AuditoriumScheduleSelectedAuditorium { get; set; }
        public int? AuditoriumScheduleSelectedAuditoriumId { get; set; }
        public virtual StudyYear AuditoriumScheduleSelectedStudyYear { get; set; }
        public int? AuditoriumScheduleSelectedStudyYearId { get; set; }

        public virtual Time AuditoriumScheduleSelectedTime { get; set; }
        public int? AuditoriumScheduleSelectedTimeId { get; set; }

        public DateTime? AuditoriumScheduleSelectedDate { get; set; }

        public virtual Semester AuditoriumScheduleSelectedSemester { get; set; }
        public int? AuditoriumScheduleSelectedSemesterId { get; set; }
        public virtual List<AuditoriumType> AuditoriumScheduleSelectedAuditoriumTypes { get; set; }

        public virtual Lecturer LecturerScheduleSelectedLecturer { get; set; }
        public int? LecturerScheduleSelectedLecturerId { get; set; }
        public virtual StudyYear LecturerScheduleSelectedStudyYear { get; set; }
        public int? LecturerScheduleSelectedStudyYearId { get; set; }
        public virtual Semester LecturerScheduleSelectedSemester { get; set; }
        public int? LecturerScheduleSelectedSemesterId { get; set; }

        public virtual Building SettingsDataEditSelectedBuilding { get; set; }
        public int? SettingsDataEditSelectedBuildingId { get; set; }

        public int? SettingsDataEditSelectedTabId { get; set; }
    }
}
