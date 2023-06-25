using Microsoft.OpenApi.Models;
using System;

public static class TaskService
{
    // For Interval in Seconds
    // IntervalInSeconds(start_hour, start_minute, seconds)
    public static void IntervalInSeconds(int hour, int sec, double interval, Action task)
    {
        interval = interval/3600;
        SchedulerService.Instance.ScheduleTask(hour, sec, interval, task);
    }

    // For Interval in Minutes
    // IntervalInSeconds(start_hour, start_minute, minutes)
    public static void IntervalInMinutes(int hour, int min, double interval, Action task)
    {
        interval = interval/60;
        SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }

    // For Interval in Hours
    // IntervalInSeconds(start_hour, start_minute, hours)
    public static void IntervalInHours(int hour, int min, double interval, Action task)
    {
        SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }

    // For Interval in Days
    // IntervalInSeconds(start_hour, start_minute, days)
    public static void IntervalInDays(int hour, int min, double interval, Action task)
    {
        interval = interval * 24;
        SchedulerService.Instance.ScheduleTask(hour, min, interval, task);
    }

    //Example to use
    //
    //MyScheduler.IntervalInSeconds(11, 39, 5,
    //        () => {
    //            Console.WriteLine(">> Execute Task "));
    //        });
}