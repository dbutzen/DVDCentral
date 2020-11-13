using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTB.DVDCentral.PL;
using DTB.DVDCentral.BL.Models;

namespace DTB.DVDCentral.BL
{
    public static class CustomerManager
    {
        public static int Insert(Customer customer, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (butzendbEntities dc = new butzendbEntities())
                {                                                    
                    DbContextTransaction transaction = null;        
                    if (rollback) transaction = dc.Database.BeginTransaction(); 

                    tblCustomer row = new tblCustomer();

                    row.Id = dc.tblCustomers.Any() ? dc.tblCustomers.Max(dt => dt.Id) + 1 : 1;
                    row.FirstName = customer.FirstName;
                    row.LastName = customer.LastName;
                    row.Phone = customer.Phone;
                    row.State = customer.State;
                    row.UserId = customer.UserId;
                    row.ZIP = customer.ZIP;
                    row.Address = customer.Address;
                    row.City = customer.City;

                    customer.Id = row.Id;
                    dc.tblCustomers.Add(row);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();
                    return results;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static int Update(Customer customer, bool rollback = false)
        {
            try
            {
                int results;
                using (butzendbEntities dc = new butzendbEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblCustomer row = dc.tblCustomers.FirstOrDefault(dt => dt.Id == customer.Id);

                    if (row != null)
                    {
                        row.FirstName = customer.FirstName;
                        row.LastName = customer.LastName;
                        row.Phone = customer.Phone;
                        row.State = customer.State;
                        row.UserId = customer.UserId;
                        row.ZIP = customer.ZIP;
                        row.Address = customer.Address;
                        row.City = customer.City;
                        results = dc.SaveChanges();

                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }

                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results;
                using (butzendbEntities dc = new butzendbEntities())
                {
                    DbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();
                    tblCustomer row = dc.tblCustomers.FirstOrDefault(dt => dt.Id == id);

                    if (row != null)
                    {
                        dc.tblCustomers.Remove(row);
                        results = dc.SaveChanges();
                        if (rollback) transaction.Rollback();
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static List<Customer> Load()
        {
            try
            {
                List<Customer> rows = new List<Customer>();
                using (butzendbEntities dc = new butzendbEntities())
                {
                    dc.tblCustomers
                        .ToList()
                        .ForEach(dt => rows.Add(new Customer
                        {
                            Id = dt.Id,
                            FirstName = dt.FirstName,
                            LastName = dt.LastName,
                            Address = dt.Address,
                            City = dt.City,
                            Phone = dt.Phone,
                            State = dt.State,
                            UserId = dt.UserId,
                            ZIP = dt.ZIP
                        })) ;
                    return rows;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static Customer LoadById(int id)
        {
            try
            {
                using (butzendbEntities dc = new butzendbEntities())
                {
                    tblCustomer row = dc.tblCustomers.FirstOrDefault(dt => dt.Id == id);
                    if (row != null)
                    {
                        Customer customer = new Customer { Id = row.Id, FirstName = row.FirstName, LastName = row.LastName, Address = row.Address, City = row.City, ZIP = row.ZIP, UserId = row.UserId, State = row.State, Phone = row.Phone };
                        return customer;
                    }
                    else
                    {
                        throw new Exception("Row was not found");
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
