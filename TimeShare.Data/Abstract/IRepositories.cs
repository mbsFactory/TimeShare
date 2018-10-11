using TimeShare.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeShare.Data.Abstract
{
    public interface IScheduleRepository : IEntityBaseRepository<Schedule> { }

    public interface IUserRepository : IEntityBaseRepository<User> { }

    public interface IAttendeeRepository : IEntityBaseRepository<Attendee> { }

}
