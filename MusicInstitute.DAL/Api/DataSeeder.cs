using Microsoft.EntityFrameworkCore;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Api
{
    public class DataSeeder
    {
        public static void SeedAvailableLessons(DB_Manager context)
        {
            context.Database.EnsureCreated();

            var lessons = new List<AvailableLesson>();

            DateTime now = DateTime.Now;
            var firstDayOfNextMonth = new DateOnly(now.Year, now.Month, 1).AddMonths(1);
            var lastDayOfNextMonth = firstDayOfNextMonth.AddMonths(1).AddDays(-1);

            for (DateOnly date = firstDayOfNextMonth; date <= lastDayOfNextMonth; date = date.AddDays(1))
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        lessons.Add(CreateLesson(date, new TimeOnly(9, 0), "piano", 1, 45));
                        break;

                    case DayOfWeek.Monday:
                        lessons.Add(CreateLesson(date, new TimeOnly(10, 0), "violin", 2, 45));
                        break;

                    case DayOfWeek.Tuesday:
                        lessons.Add(CreateLesson(date, new TimeOnly(11, 0), "guitar", 3, 60));
                        break;

                    case DayOfWeek.Wednesday:
                        lessons.Add(CreateLesson(date, new TimeOnly(12, 30), "drums", 4, 45));
                        break;

                    case DayOfWeek.Thursday:
                        lessons.Add(CreateLesson(date, new TimeOnly(14, 0), "flute", 5, 30));
                        break;
                }
            }

            context.AvailableLessons.AddRange(lessons);
            context.SaveChanges();
        }

        private static AvailableLesson CreateLesson(DateOnly date, TimeOnly time, string kind, int teacherId, int duration)
        {
            return new AvailableLesson
            {
                LessonDate = date,
                LessonTime = time,
                Kind = kind,
                TeacherIdLessons = teacherId,
                DurationMinutes = duration
            };
        }

    }
}
