using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Eventkalender.Database;
using Eventkalender.WS;

[WebService(Namespace = "http://www.ics.lu.se.cali.cronus/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class CronusService : WebService
{
    private CronusController cronusController;

    public CronusService()
    {
        string databaseFilePath = PathUtility.GetPhysicalPath("~/App_Data") + "/cronus-db.xml";
        cronusController = new CronusController(databaseFilePath);
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
    public List<string> GetKeys()
    {
        return cronusController.GetKeys();
    }

    [WebMethod]
    public List<string> GetIndexes()
    {
        return cronusController.GetIndexes();
    }

    [WebMethod]
    public List<string> GetTableConstraints()
    {
        return cronusController.GetTableConstraints();
    }

    [WebMethod]
    public List<string> GetTables()
    {
        return cronusController.GetTables();
    }

    [WebMethod]
    public List<string> GetColumnsForEmployeeTable()
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
    public List<DataTuple> GetEmployeeAndRelatives()
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
    public List<DataTuple> GetEmployeeMetadata()
    {
        return cronusController.GetEmployeeMetadata();
    }

    [WebMethod]
    public List<DataTuple> GetEmployeeAbsenceMetadata()
    {
        return cronusController.GetEmployeeAbsenceMetadata();
    }

    [WebMethod]
    public List<DataTuple> GetEmployeeRelativeMetadata()
    {
        return cronusController.GetEmployeeRelativeMetadata();
    }

    [WebMethod]
    public List<DataTuple> GetEmployeeQualificationMetadata()
    {
        return cronusController.GetEmployeeQualificationMetadata();
    }

    [WebMethod]
    public List<DataTuple> GetEmployeePortalSetupMetadata()
    {
        return cronusController.GetEmployeePortalSetupMetadata();
    }

    [WebMethod]
    public List<DataTuple> GetEmployeeStatisticsGroupMetadata()
    {
        return cronusController.GetEmployeeStatisticsGroupMetadata();
    }
}
