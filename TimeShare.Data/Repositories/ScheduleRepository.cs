using TimeShare.Data.Abstract;
using TimeShare.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;

namespace TimeShare.Data.Repositories
{
    public class ScheduleRepository : EntityBaseRepository<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(TimeShareContext context)
            : base(context)
        { }
    }
}
