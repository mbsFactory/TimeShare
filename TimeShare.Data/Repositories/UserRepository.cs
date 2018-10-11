using TimeShare.Data.Abstract;
using TimeShare.Model.Entities;

using System;
using System.Collections.Generic;
using System.Text;


namespace TimeShare.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(TimeShareContext context)
            : base(context)
        { }
    }
}
