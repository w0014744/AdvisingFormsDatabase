namespace AdvisingFormsDatabase.Migrations
{
    using AdvisingFormsDatabase.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AdvisingFormsDatabase.DAL.AdvisingFormsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "AdvisingFormsDatabase.DAL.AdvisingFormsContext";
        }

        protected override void Seed(AdvisingFormsDatabase.DAL.AdvisingFormsContext context)
        {

            context.Students.AddOrUpdate(
              s => s.WNumber,
              new Student { FirstName = "andrew", LastName = "peters", WNumber="10123456", ConcentrationID =1, GPA = 4.0, HoursCompleted=20 },
              new Student { FirstName = "brice", LastName = "lambsonw", WNumber = "20123456", ConcentrationID = 2, GPA = 3.5, HoursCompleted = 30 },
              new Student { FirstName = "rowan ", LastName = "miller", WNumber="30123456", ConcentrationID =3, GPA = 3.0, HoursCompleted= 50}
            );


            
        }
    }
}
