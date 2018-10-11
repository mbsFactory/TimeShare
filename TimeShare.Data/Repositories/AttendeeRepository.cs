using TimeShare.Data.Abstract;
using TimeShare.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeShare.Data.Repositories
{
    public class AttendeeRepository : EntityBaseRepository<Attendee>, IAttendeeRepository
    {
        public AttendeeRepository(TimeShareContext context)
            : base(context)
        { }
    }
}
