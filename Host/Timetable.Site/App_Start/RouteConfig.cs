using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Timetable.Site
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           
            routes.MapHttpRoute(
              name: "LecturerGetApi",
              routeTemplate: "api/v1.0/lecturer/get/{tutorialid}",
              defaults: new { controller = "Lecturer", action = "Get", tutorialid = UrlParameter.Optional }
            );
            

            routes.MapHttpRoute(
                name: "FacultyGetApi",
                routeTemplate: "api/v1.0/faculty/getall",
                defaults: new { controller = "Faculty", action = "GetAll" }
            );

            routes.MapHttpRoute(
              name: "CourseGetApi",
              routeTemplate: "api/v1.0/course/getall",
              defaults: new { controller = "Course", action = "GetAll" }
            );

            routes.MapHttpRoute(
              name: "TimeGetApi",
              routeTemplate: "api/v1.0/time/getall",
              defaults: new { controller = "Time", action = "GetAll" }
            );

            
            routes.MapHttpRoute(
             name: "TutorialGetApi",
             routeTemplate: "api/v1.0/tutorial/getall",
             defaults: new { controller = "Tutorial", action = "GetAll" }
           );

            routes.MapHttpRoute(
               name: "SpecialityGetApi",
               routeTemplate: "api/v1.0/speciality/Getbyfaculty/{facultyid}",
               defaults: new { controller = "Speciality", action = "GetByFaculty" }
           );
            
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1.0/{controller}/{action}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );
            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}