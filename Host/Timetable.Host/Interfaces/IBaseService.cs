﻿using System;
using System.ServiceModel;
using Timetable.Base.Entities;
using Timetable.Data.Models;

namespace Timetable.Host.Interfaces
{
    [ServiceContract]
    public interface IBaseService
    {
        [OperationContract]
        OperationResult Add(BaseEntity entity);

        [OperationContract]
        OperationResult Update(BaseEntity entity);

        [OperationContract]
        OperationResult Delete(BaseEntity entity);
    }
}
