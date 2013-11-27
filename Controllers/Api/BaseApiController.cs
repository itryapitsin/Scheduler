using System;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using Timetable.Logic.Interfaces;
using Timetable.Logic.Services;

namespace Timetable.Site.Controllers.Api
{
    public abstract class BaseApiController : ApiController
    {
        //protected readonly IDataService DataService;
        public readonly IDataService NewDataService;
        protected HttpResponseMessage Response;

        protected BaseApiController(IDataService dataService)
        {
            NewDataService = new SchedulerService();
        }

        protected HttpResponseMessage CreateResponse<T1, T2>(Func<T1, T2> func, T1 arg)
        {
            try
            {
                var result = func(arg);

                Response = Request.CreateResponse<T2>(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Response;
        }

        protected HttpResponseMessage CreateResponse<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            try
            {
                var result = func(arg1, arg2, arg3, arg4);

                Response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Response;
        }

        protected HttpResponseMessage CreateResponse<T1, T2, T3, T4, T5, T6, T7>(Func<T1, T2, T3, T4, T5, T6, T7> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            try
            {
                var result = func(arg1, arg2, arg3, arg4, arg5, arg6);

                Response = Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Response;
        }

        protected HttpResponseMessage CreateResponse<T>(Action<T> action, T arg)
        {
            try
            {
                action(arg);

                Response = Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                Response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

            return Response;
        }
    }
}
