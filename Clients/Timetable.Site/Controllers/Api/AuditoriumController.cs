using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Timetable.Site.Models;
using Timetable.Site.NewDataService;

namespace Timetable.Site.Controllers.Api
{
    public class AuditoriumController : BaseApiController
    {
        public AuditoriumController(IDataService dataService) : base(dataService)
        {
        }

        public HttpResponseMessage GetByBuilding(
            int buildingId, 
            int auditoriumTypeId)
        {
            var result = NewDataService
                .GetAuditoriums(
                    new BuildingDataTransfer {Id = buildingId},
                    new AuditoriumTypeDataTransfer {Id = auditoriumTypeId})
                .Select(x => new AuditoriumViewModel(x));

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        public HttpResponseMessage GetFree(
            int buildingId,
            int weekTypeId,
            int day,
            int timeId,
            int tutorialTypeId,
            int auditoriumTypeId,
            string startTime,
            string endTime)
        {

            var startDate = new DateTime();
            var endDate = new DateTime();

            if (!string.IsNullOrEmpty(startTime))
                startDate = DateTime.ParseExact(startTime, "yyyy-MM-dd", null);

            if (!string.IsNullOrEmpty(endTime))
                endDate = DateTime.ParseExact(endTime, "yyyy-MM-dd", null);

            var capacity = 0;

            var result = NewDataService
                .GetFreeAuditoriums(
                    new BuildingDataTransfer{Id = buildingId}, 
                    day, 
                    new WeekTypeDataTransfer{Id= weekTypeId}, 
                    new TimeDataTransfer{Id = timeId},
                    null, 
                    new AuditoriumTypeDataTransfer{Id = auditoriumTypeId}, 
                    capacity, 
                    startDate, 
                    endDate)
                .Select(x => new AuditoriumViewModel(x));


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        
        [HttpPost]
        public HttpResponseMessage Add(AuditoriumAddViewModel viewModel)
        {
            var qBuilding = new Building { Id = viewModel.BuildingId };
            var qAuditoriumType = new AuditoriumType { Id = viewModel.AuditoriumTypeId };
            var aAuditorium = new Auditorium
            {
                Building = qBuilding,
                AuditoriumType = qAuditoriumType,
                Capacity = viewModel.Capacity,
                Info = viewModel.Info,
                Name = viewModel.Name,
                Number = viewModel.Number,
                UpdateDate = DateTime.Now.Date,
                CreatedDate = DateTime.Now.Date,
                IsActual = true
            };

            NewDataService.Add(aAuditorium);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            NewDataService.Delete(new Auditorium { Id = id });

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public Auditorium CreateAuditorium(
            int Id,
            int AudTypeId,
            int BuildId)
        {
            var a1 = new Auditorium();

            a1.AuditoriumType = new AuditoriumType();
            a1.AuditoriumType.Id = AudTypeId;
            if (AudTypeId == 1)
                a1.AuditoriumType.Name = "Лекционная";

            if (AudTypeId == 2)
                a1.AuditoriumType.Name = "Лабораторная";

            if (AudTypeId == 3)
                a1.AuditoriumType.Name = "Дисплейная";

            if (AudTypeId == 4)
                a1.AuditoriumType.Name = "Кабинет";

            a1.Building = new Building();
            a1.Building.Id = BuildId;
            if (BuildId == 1)
            {
                a1.Building.Name = "Главный корпус";
                a1.Building.ShortName = "ГК";
            }
            if (BuildId == 2)
            {
                a1.Building.Name = "Теоритический корпус";
                a1.Building.ShortName = "ГК";
            }

            a1.Capacity = 30;
            a1.Number = Id.ToString();
            a1.Id = 2;

            return a1;
        }
    }
}
