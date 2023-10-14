using ConferenceRegistration.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml.Schema;

namespace ConferenceRegistration.Services
{
    public class ParticipantsService
    {
        public IEnumerable<Participant> GetParticipantsForPage(int pageSize, int page, int? sortBy = null, bool ascending = true)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var participants = dbContext.Users.Include(x => x.Region);
                switch (sortBy) {
                    case 0:
                        participants = participants.OrderBy(x => x.FullName, ascending);
                        break;
                    case 1:
                        participants = participants.OrderBy(x => x.Region.Name, ascending);
                        break;
                    case 2:
                        participants = participants.OrderBy(x => x.Age, ascending);
                        break;
                    default:
                        participants = participants.OrderBy(x => x.EnrollmentDate, false);
                        break;
                }

                var currentPage = participants.Skip(pageSize * page).Take(pageSize).ToList();

                return currentPage;
            }
        }

        public int CalculatePagesCount(int pageSize)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var pages = (double)dbContext.Users.Count() / pageSize;
                return Convert.ToInt32(Math.Ceiling(pages));
            }
        }
    }

    public static class ExtensionMethodClass
    {
        public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, Expression<Func<TSource, TKey>> keySelector, bool ascending)
        {
            if (ascending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }
    }
}