using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessCenter.Data;

namespace BusinessCenter.Api.Test
{
    public class BcbcTestContext : DbContext
    {
        public BcbcTestContext()
            : base("Name=IdentityConnectionContext")
        {

        }

        public DbSet<SecurityQuestion> SecurityQuestion { get; set; }

        public void Seed(BcbcTestContext context)
        {
            var listCountry = new List<SecurityQuestion>() {
             new SecurityQuestion() { id = 1, Question = "what is your favorite color" },
             new SecurityQuestion() { id = 2, Question = "what is your pet name" },
             new SecurityQuestion() { id = 3, Question = "what is your primary school name" },
              new SecurityQuestion() { id = 4, Question = "what is your phone color" }
            };
            context.SecurityQuestion.AddRange(listCountry);
            context.SaveChanges();
        }

        public class DropCreateIfChangeInitializer : DropCreateDatabaseIfModelChanges<BcbcTestContext>
        {
            protected override void Seed(BcbcTestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class CreateInitializer : CreateDatabaseIfNotExists<BcbcTestContext>
        {
            protected override void Seed(BcbcTestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<BcbcTestContext>
        {
            protected override void Seed(BcbcTestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

    }
}
