using Eventkalender.Database.DAL;
using Eventkalender.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eventkalender.Database.Controller;

[WebService(Namespace = "http://www.ics.lu.se.cali/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CronusService : WebService
{
    private CronusController cronusController;

    public CronusService()
    {
        cronusController = new CronusController();
    }

    /* [WebMethod]
    public void Update()
    {

        return cronusController.Update();
    }
    [WebMethod]
    public void Insert()
    {

        return cronusController.Insert();
    }
    [WebMethod]
    public void Delete()
    {

        return cronusController.Delete();
    }
    [WebMethod]
    public void Select()
    {

        return cronusController.Select();
    }
    */

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
    public Eventkalender.Database.Model.Tuple GetIllestPerson()
    {
        return cronusController.GetIllestPerson();
    }
    [WebMethod]
    public List<Eventkalender.Database.Model.Tuple> GetIllPersonsByYear(int startYear, int endYear)
    {
        return cronusController.GetIllPersonsByYear(startYear, endYear);
    }
    [WebMethod]
    public List<Eventkalender.Database.Model.Tuple> GetEmployeeAndRelatves()
    {
        return cronusController.GetEmployeeAndRelatives();
    }


    [WebMethod]
     public List<Eventkalender.Database.Model.Tuple> GetEmployeeData()
     {
        return cronusController.GetEmployeeData();
     }

    [WebMethod]
    public List<Eventkalender.Database.Model.Tuple> GetEmployeeAbsenceData()
    {
        return cronusController.GetEmployeeAbsenceData();
    }

    [WebMethod]
     public List<Eventkalender.Database.Model.Tuple> GetEmployeeRelativeData()
     {
        return cronusController.GetEmployeeRelativeData();
     }

     [WebMethod]
     public List<Eventkalender.Database.Model.Tuple> GetEmployeeQualificationData()
     {
        return cronusController.GetEmployeeQualificationData();
     }

    [WebMethod]
    public List<Eventkalender.Database.Model.Tuple> GetEmployeePortalSetupData()
    {
        return cronusController.GetEmployeePortalSetupData();
    }


    [WebMethod]
     public List<Eventkalender.Database.Model.Tuple> GetEmployeeStatisticsGroupData()
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