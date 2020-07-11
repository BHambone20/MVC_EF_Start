using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EF_Start.DataAccess;
using MVC_EF_Start.Models;

namespace MVC_EF_Start.Controllers
{
  public class DatabaseExampleController : Controller
  {
    public ApplicationDbContext dbContext;

    public DatabaseExampleController(ApplicationDbContext context)
    {
      dbContext = context;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<ViewResult> DatabaseOperations()
    {
      
      
      // CREATE operation
      Student MyStudent = new Student();
      MyStudent.Name = "Brian Hammond 3";

      Course MyCourse = new Course();
      MyCourse.Name = "ISM 6225 Distributed Information System 3";
      
      Enrolment MyEnrollment = new Enrolment();
      MyEnrollment.grade = "B+";
            MyEnrollment.course = MyCourse;
            MyEnrollment.student = MyStudent;


      Company MyCompany = new Company();
      MyCompany.symbol = "MCOB";
      MyCompany.name = "ISM";
      MyCompany.date = "ISM";
      MyCompany.isEnabled = true;
      MyCompany.type = "ISM";
      MyCompany.iexId = "ISM";

      Quote MyCompanyQuote1 = new Quote();
      //MyCompanyQuote1.EquityId = 123;
      MyCompanyQuote1.date = "11-25-2020";
      MyCompanyQuote1.open = 46.13F;
      MyCompanyQuote1.high = 47.18F;
      MyCompanyQuote1.low = 44.67F;
      MyCompanyQuote1.close = 47.01F;
      MyCompanyQuote1.volume = 37654000;
      MyCompanyQuote1.unadjustedVolume = 37654000;
      MyCompanyQuote1.change = 13.43F;
      MyCompanyQuote1.changePercent = 0.03F;
      MyCompanyQuote1.vwap = 9.76F;
      MyCompanyQuote1.label = "Nov 25";
      MyCompanyQuote1.changeOverTime = 0.56F;
      MyCompanyQuote1.symbol = "MCOB";

      Quote MyCompanyQuote2 = new Quote();
      //MyCompanyQuote1.EquityId = 123;
      MyCompanyQuote2.date = "11-23-2018";
      MyCompanyQuote2.open = 46.13F;
      MyCompanyQuote2.high = 47.18F;
      MyCompanyQuote2.low = 44.67F;
      MyCompanyQuote2.close = 47.01F;
      MyCompanyQuote2.volume = 37654000;
      MyCompanyQuote2.unadjustedVolume = 37654000;
      MyCompanyQuote2.change = 1.43F;
      MyCompanyQuote2.changePercent = 0.03F;
      MyCompanyQuote2.vwap = 9.76F;
      MyCompanyQuote2.label = "Nov 23";
      MyCompanyQuote2.changeOverTime = 0.56F;
      MyCompanyQuote2.symbol = "MCOB";

      //dbContext.Students.Add(MyStudent);
      //dbContext.Courses.Add(MyCourse);
      dbContext.Enrolments.Add(MyEnrollment);
      //dbContext.Companies.Add(MyCompany);
      //dbContext.Quotes.Add(MyCompanyQuote1);
      //dbContext.Quotes.Add(MyCompanyQuote2);

      dbContext.SaveChanges();
      
      // READ operation
      Company CompanyRead1 = dbContext.Companies
                              .Where(c => c.symbol == "MCOB")
                              .First();

      Company CompanyRead2 = dbContext.Companies
                              .Include(c => c.Quotes)
                              .Where(c => c.symbol == "MCOB")
                              .First();

      // UPDATE operation
      CompanyRead1.iexId = "MCOB223222";
      dbContext.Companies.Update(CompanyRead1);
      //dbContext.SaveChanges();
      await dbContext.SaveChangesAsync();
      
      // DELETE operation
      //dbContext.Companies.Remove(CompanyRead1);
      //await dbContext.SaveChangesAsync();

      return View();
    }

    public ViewResult LINQOperations()
    {
      Company CompanyRead1 = dbContext.Companies
                                      .Where(c => c.symbol == "MCOB")
                                      .First();

      Company CompanyRead2 = dbContext.Companies
                                      .Include(c => c.Quotes)
                                      .Where(c => c.symbol == "MCOB")
                                      .First();

      Quote Quote1 = dbContext.Companies
                              .Include(c => c.Quotes)
                              .Where(c => c.symbol == "MCOB")
                              .FirstOrDefault()
                              .Quotes
                              .FirstOrDefault();

      return View();
    }

        public ViewResult StudentUpdate()
        {
            Student Student1 = dbContext.Students
                                            .Where(c => c.Id == 2)
                                            .First();

            Student1.Name = "Dan Howard";
            dbContext.Students.Update(Student1);
            dbContext.SaveChanges();
            
            return View();
        }

    }
}