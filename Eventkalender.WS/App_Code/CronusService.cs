using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eventkalender.Database;

[WebService(Namespace = "http://www.ics.lu.se.cali/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CronusService : WebService
{
    private CronusController cronusController;

    public CronusService()
    {
        string xmlPath = HttpContext.Current.Server.MapPath("~/App_Data/cronus-db.xml");
        cronusController = new CronusController(xmlPath);
    }

    ///* [WebMethod]
    //public void Update()
    //{

    //    return cronusController.Update();
    //}
    //[WebMethod]
    //public void Insert()
    //{

    //    return cronusController.Insert();
    //}
    //[WebMethod]
    //public void Delete()
    //{

    //    return cronusController.Delete();
    //}
    //[WebMethod]
    //public void Select()
    //{

    //    return cronusController.Select();
    //}
    //*/

    [WebMethod]
    public List<String> GetKeys()
    {
        return cronusController.GetKeys();
    }

    [WebMethod]
    public List<String> GetIndexes()
    {
        return cronusController.GetIndexes();
    }

    [WebMethod]
    public List<String> GetTableConstraints()
    {
        return cronusController.GetTableConstraints();
    }

    [WebMethod]
    public List<String> GetTables()
    {
        return cronusController.GetTables();
    }

    [WebMethod]
    public List<String> GetColumnsForEmployeeTable()
    {
        return cronusController.GetColumnsForEmployeeTable();
    }

    [WebMethod]
    public DataTuple GetIllestPerson()
    {
        return cronusController.GetIllestPerson();
    }

    [WebMethod]
    public List<DataTuple> GetSickPersonByYear(int startYear, int endYear)
    {
        return cronusController.GetSickPersonByYear(startYear, endYear);
    }

    /* [WebMethod]



     public void Employee()
     {

         return cronusController.GetEmployee();
     }
     [WebMethod]
     public void GetEmployeeRelatives()
     {

         return cronusController.GetEmployeeRelatives();
     }
     [WebMethod]
     public void GetIllemployee(int year)
     {

         return cronusController.GetIllEmployee();
     }
     [WebMethod]
     public void GetIllestEmployee()
     {

         return cronusController.GetIllestEmployee(); 
     } */
}