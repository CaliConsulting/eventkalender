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
    public List<DataTuple> GetIllPersonsByYear(int startYear, int endYear)
    {
        return cronusController.GetIllPersonsByYear(startYear, endYear);
    }
    [WebMethod]
    public List<DataTuple> GetEmployeeAndRelatves()
    {
        return cronusController.GetEmployeeAndRelatives();
    }


    [WebMethod]
     public List<DataTuple> GetEmployeeData()
     {
        return cronusController.GetEmployeeData();
     }

    [WebMethod]
    public List<DataTuple> GetEmployeeAbsenceData()
    {
        return cronusController.GetEmployeeAbsenceData();
    }

    [WebMethod]
     public List<DataTuple> GetEmployeeRelativeData()
     {
        return cronusController.GetEmployeeRelativeData();
     }

     [WebMethod]
     public List<DataTuple> GetEmployeeQualificationData()
     {
        return cronusController.GetEmployeeQualificationData();
     }

    [WebMethod]
    public List<DataTuple> GetEmployeePortalSetupData()
    {
        return cronusController.GetEmployeePortalSetupData();
    }


    [WebMethod]
     public List<DataTuple> GetEmployeeStatisticsGroupData()
     {
        return cronusController.GetEmployeeStatisticsGroupData();
     }

    [WebMethod]
    public List<String> GetEmployeeMetaData()
    {
        return cronusController.GetEmployeeMetaData();
    }

    [WebMethod]
    public List<string> GetEmployeeAbsenceMetaData()
    {
        return cronusController.GetEmployeeAbsenceMetaData();
    }

    [WebMethod]
    public List<string> GetEmployeeRelativeMetaData()
    {
        return cronusController.GetEmployeeRelativeMetaData();
    }

    [WebMethod]
    public List<string> GetEmployeeQualificationMetaData()
    {
        return cronusController.GetEmployeeQualificationMetaData();
    }

    [WebMethod]
    public List<string> GetEmployeePortalSetupMetaData()
    {
        return cronusController.GetEmployeePortalSetupMetaData();
    }


    [WebMethod]
    public List<string> GetEmployeeStatisticsGroupMetaData()
    {
        return cronusController.GetEmployeeStatisticsGroupMetaData();
    }
}