using Microsoft.EntityFrameworkCore;
using SchoolFinder.Common.Abstraction.Extensions;
using SchoolFinder.Common.Identity.Authentication.Registration;
using SchoolFinder.DAL.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFinder.DAL.Stores
{
    public class RegistrationFormStore
    {
        private readonly  ApplicationDbContext _context;

        public RegistrationFormStore(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> Create(RegistrationForm form)
        {
            _context.RegistrationForms.Add(form);

            return _context.SaveChangesAsync();
        }

        public Task<List<RegistrationForm>> Find(RegistrationFormFilter filter) {
            return _context.RegistrationForms
                .FilterBy(filter)
                .SortBy(filter)
                .TakePage(filter)
                .ToListAsync();
        }

        public Task<int> GetTotalCount(RegistrationFormFilter filter)
        {
            return _context.RegistrationForms
                .FilterBy(filter)
                .CountAsync();
        }

        public Task<int> Update(RegistrationForm form)
        {
            _context.RegistrationForms.Update(form);

            return _context.SaveChangesAsync();
        }
    }
}
