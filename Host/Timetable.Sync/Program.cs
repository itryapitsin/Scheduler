﻿using System.Messaging;
using Timetable.Sync.Toolkit;
using Timetable.Sync.Toolkit.Tasks;

namespace Timetable.Sync
{
    class Program
    {
        static void Main(string[] args)
        {
            //var conn = new OracleConnection("DATA SOURCE=iias.karelia.ru:1521/iias;USER ID=DPYATIN;Password=xgmst321");
            //try
            //{
            //    conn.Open();
            //    OracleCommand cmd = conn.CreateCommand();
            //    cmd.CommandText = "select cast(\"extend1\".\"ID\" as number(10,0)) as \"C1\", \"Extend1\".\"NAMEFULL\" as \"NAMEFULL\" from \"sdms\".\"B_BULDINGS\" \"Extend1\")";
            //    //cmd.CommandText = "select ID, NAMEFULL from sdms.B_BULDINGS";
            //    var reader = cmd.ExecuteReader();
            //    var data = reader.GetData(0);
            //}
            //catch (Exception)
            //{
            //}
            //finally
            //{
            //    conn.Close();
            //}

            //using (var context = new IIASContext(new OracleConnection(ConfigurationManager.ConnectionStrings["OracleHR"].ConnectionString)))
            //{
            //    var buildings = context.Buildings.AsEnumerable();
            //    //var result = context.RawSqlCommand("select * from V_ASP_ATTEST");
            //}

            var task = new SyncDataTask();

            var publisher = new TaskPublisher();
            //publisher.Publish(new SyncDataTask());
            //publisher.Publish(new SyncDataTask());
            publisher.Publish(task);

            var taskPool = new TaskPool();
            var t = taskPool.GetTaskById<SyncDataTask>(task.Id);
            var tasks = taskPool.GetTasks(typeof(SyncDataTask));

            var reciver = new TaskReciver();
            reciver.Recive<SyncDataTask>();
        }
    }
}
