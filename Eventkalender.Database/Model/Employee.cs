﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eventkalender.Database
{
    public class Employee
    {
        public Employee()
        {
            DateTime date = new DateTime(2018, 02, 23);

            // Default values
            No = "";
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Initials = "";
            JobTitle = "";
            SearchName = "";
            Address = "";
            Address2 = "";
            City = "";
            PostCode = "";
            County = "";
            PhoneNo = "";
            MobilePhoneNo = "";
            Email = "";
            AltAddressCode = "";
            AltAddressStartDate = date;
            AltAddressEndDate = date.AddDays(1);
            Picture = null;
            BirthDate = date.AddYears(-20);
            SocialSecurityNo = "";
            UnionCode = "";
            UnionMembershipNo = "";
            Sex = 2;
            CountryRegionCode = "";
            ManagerNo = "";
            EmploymentContractCode = "";
            StatisticsGroupCode = "";
            EmploymentDate = date.AddYears(-1);
            Status = 2;
            InactiveDate = date.AddDays(-1);
            CauseOfInactivityCode = "";
            TerminationDate = date;
            GroundsForTermCode = "";
            GlobalDimension1Code = "";
            GlobalDimension2Code = "";
            ResourceNo = "";
            LastDateModified = date.AddDays(-20);
            Extension = "";
            Pager = "";
            FaxNo = "";
            CompanyEmail = "";
            Title = "";
            SalesPersPurchCode = "";
            NoSeries = "";
        }

        public Employee(string no, string firstName, string lastName) : this()
        {
            No = no;
            FirstName = firstName;
            LastName = lastName;
        }

        [Column("timestamp")]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        [Key]
        [Column("No_")]
        public string No { get; set; }

        [Column("First Name")]
        public string FirstName { get; set; }

        [Column("Middle Name")]
        public string MiddleName { get; set; }

        [Column("Last Name")]
        public string LastName { get; set; }

        public string Initials { get; set; }

        [Column("Job Title")]
        public string JobTitle { get; set; }

        [Column("Search Name")]
        public string SearchName { get; set; }

        public string Address { get; set; }

        [Column("Address 2")]
        public string Address2 { get; set; }

        public string City { get; set; }

        [Column("Post Code")]
        public string PostCode { get; set; }

        public string County { get; set; }

        [Column("Phone No_")]
        public string PhoneNo { get; set; }

        [Column("Mobile Phone No_")]
        public string MobilePhoneNo { get; set; }

        [Column("E-Mail")]
        public string Email { get; set; }

        [Column("Alt_ Address Code")]
        public string AltAddressCode { get; set; }

        [Column("Alt_ Address Start Date")]
        public DateTime AltAddressStartDate { get; set; }

        [Column("Alt_ Address End Date")]
        public DateTime AltAddressEndDate { get; set; }

        public byte[] Picture { get; set; }

        [Column("Birth Date")]
        public DateTime BirthDate { get; set; }

        [Column("Social Security No_")]
        public string SocialSecurityNo { get; set; }

        [Column("Union Code")]
        public string UnionCode { get; set; }

        [Column("Union Membership No_")]
        public string UnionMembershipNo { get; set; }

        [Column("Sex")]
        public int Sex { get; set; }

        [Column("Country_Region Code")]
        public string CountryRegionCode { get; set; }

        [Column("Manager No_")]
        public string ManagerNo { get; set; }

        [Column("Emplymt_ Contract Code")]
        public string EmploymentContractCode { get; set; }

        [Column("Statistics Group Code")]
        public string StatisticsGroupCode { get; set; }

        [Column("Employment Date")]
        public DateTime EmploymentDate { get; set; }

        public int Status { get; set; }

        [Column("Inactive Date")]
        public DateTime InactiveDate { get; set; }

        [Column("Cause of Inactivity Code")]
        public string CauseOfInactivityCode { get; set; }

        [Column("Termination Date")]
        public DateTime TerminationDate { get; set; }

        [Column("Grounds for Term_ Code")]
        public string GroundsForTermCode { get; set; }

        [Column("Global Dimension 1 Code")]
        public string GlobalDimension1Code { get; set; }

        [Column("Global Dimension 2 Code")]
        public string GlobalDimension2Code { get; set; }

        [Column("Resource No_")]
        public string ResourceNo { get; set; }

        [Column("Last Date Modified")]
        public DateTime LastDateModified { get; set; }

        public string Extension { get; set; }

        public string Pager { get; set; }

        [Column("Fax No_")]
        public string FaxNo { get; set; }

        [Column("Company E-Mail")]
        public string CompanyEmail { get; set; }

        public string Title { get; set; }

        [Column("Salespers__Purch_ Code")]
        public string SalesPersPurchCode { get; set; }

        [Column("No_ Series")]
        public string NoSeries { get; set; }
    }
}