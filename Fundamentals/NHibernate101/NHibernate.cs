using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Hql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Fundamentals.NHibernate101
{
    class NHibernate101
    {
        public static void Lesson()
        {
            #region Configuration
            // NHibernateProfiler.Initialize(); does not come out of the box... really nice gui 4 debug.

            StringBuilder sb = new StringBuilder();
            sb.Append("Data Source=CALDERONMUNOZ\\BRANDON_LS;");
            sb.Append("Initial Catalog=Brandon_LocalServer;");
            sb.Append("Integrated Security=True;");
            sb.Append("Connect Timeout=30;");
            sb.Append("Encrypt=False;");

            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = sb.ToString(); 
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
                // x.LogSqlInConsole = true; // Comment out in production, use for debugging sql queries.
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = cfg.BuildSessionFactory();
            #endregion

            #region Lessons
            // Overview();
            // QueryDbWithNHibernate(sessionFactory);
            // InsertDataWithNHibernate(sessionFactory);
            // UpdateDataWithNHibernate(sessionFactory, "CM", "Calderon-Morales");
            // DeleteDataWithNHibernate(sessionFactory, 4);
            // MappingMetaData(sessionFactory);
            MappingComponents(sessionFactory);
            #endregion
        }

        public static void Overview()
        {
            #region Supported Databases
            /**
             * - SQL Server
             * - Oracle
             * - DB2
             * - Firebird
             * - Informix
             * - Ingres
             * - MySQL
             * - PostgresSQL
             * - SQLite
             * - SQL Server CE
             * - SyBase
             * - Any OBDC - or OleDb-compliant data source
             */
            #endregion

            #region OO vs. Relational World
            /**
             * OO
             * - Object-based
             * - Unidirectional associations
             * - Pointer from owner
             * - Inheritance
             * - Polymorphism
             * - Many-to-many (customer with many addresses)
             * 
             * Relational
             * - Set-based
             * - Bidirectional associations
             * - FK on owned
             * - No inheritance
             * - No polymorphism
             * - Join tables
             */
            #endregion

            #region Mapping Concepts
            /**
             * Class definitions => Mapping Metadata ->  Database Schema
             */
            #endregion

            #region Domain Classes in NHibernate
            /**
             *   - No special persistence-related base class, don't have to derive
             *   from a particular base class (POCOs, plain-old clr objects).
             *   - No marker or callback interfaces
             *   - No persistence related attributes
             *   - NHibernate domain class:

                public class Customer
                {
                    public virtual int Id { get; set; }
                    public virtual string FirstName { get; set; }
                    public virtual string LastName { get; set; }
                }
             */
            #endregion

            #region DDL in NHibernate
            /**
            
            NHibernate can map the following table to the previous Domain class.

            CREATE TABLE [dbo].[Customer] (
                [Id] [int] IDENTITY(1,1) NOT NULL,
                [FirstName] [nvarchar](50) NOT NULL,
                [LastName] [nvarchar](50) NOT NULL,
                CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([Id])
            ) 
             */
            #endregion

            #region Mapping Metadata in NHibernate
            /**
            <?xml version="1.0" encoding="utf-8" ?>
            <hibernate-mapping xmlns="urn:nhibernate-mapping-2.2"
                               assembly="NhibernateDemo"
                               namespace="NHibernateDemo">
                <class name="Customer">
                    <id name="Id">
                        <generator class="native" />
                    </id>
                    <property name="FirstName" />
                    <property name="LastName" />
                </class>
            </hibernate-mapping>
            */
            #endregion

            #region NHibernate API
            /**
             * Configuration - Class for bootstrapping NHibernate
             * ISessionFactory - Factory for creating sessions
             * ISession - Roughly analogous to a database connection
             * ITransaction - Abstracts underlying transaction semantics
             * IQuery - String-based query API aka HQL
             * ICriteria - Object-based query API aka Criteria
             * LINQ - LINQ-based query api
             */
            #endregion

            #region Using NHibernate in a project
            /**
             * Install using nuget pm:
             *  - install-package nhibernate
             *  
             * Create a new NHibernate configuration object.
             *  - Typically at root of project (like Program.cs).
             */
            #endregion

            #region Configuration
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Server=localhost;Database=NHibernateDemo;Integrated Security=SSPI";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            var sessionFactory = cfg.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                // perform database logic
                tx.Commit();
            }
            #endregion
        }

        #region Helper Methods

        public static void QueryIndividualCustomerPrimaryKey(ISessionFactory sessionFactory,
            int pkToQuery,
            bool hold = false)
        {
            
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = from customer in session.Query<Customer>()
                where customer.Id == pkToQuery 
                select customer;

                try
                {
                    var found = query.Single();
                    Console.WriteLine("Found Customer: {0} {1}, PK: {2}",
                        found.FirstName, found.LastName, found.Id);
                }
                catch (System.InvalidOperationException)
                {
                    Console.WriteLine("Customer with id: {0} not found", pkToQuery);
                }
                finally
                {
                    tx.Commit();
                }
            }
            if (hold)
            {
                Console.WriteLine("Press <Enter> to exit");
                Console.ReadLine();
            }
        }

        public static void QueryIndividualCustomerLastName(
            ISessionFactory sessionFactory,
            string lastName)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = from customer in session.Query<Customer>()
                            where customer.LastName == lastName
                            select customer;

                try
                {
                    var found = query.Single();
                    if (found != null)
                    {
                        Console.WriteLine("Found Customer: {0} {1}, PK: {2}",
                            found.FirstName, found.LastName, found.Id);
                    }
                }
                catch (System.InvalidOperationException)
                {
                    Console.WriteLine("Customer not found");
                }
                finally
                {

                    tx.Commit();
                    Console.WriteLine("Press <Enter> to exit");
                    Console.ReadLine();
                }
            }
        }

        public static Customer CreateCustomer(string firstName,
            string lastName,
            int points,
            bool hasGoldStatus,
            CustomerCreditRating creditRating,
            double averageRating,
            Location location)
        {
            return new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Points = points,
                HasGoldStatus = hasGoldStatus,
                MemberSince = DateTime.Now,
                CreditRating = creditRating,
                AverageRating = averageRating,
                Address = location
            };
        }

        #endregion

        #region CRUD Operations

        public static void QueryDbWithNHibernate(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                #region using ICriteria
                //var customers = session.CreateCriteria<Customer>()
                //       .List<Customer>();
                #endregion

                #region
                // using LINQ
                var customers = from customer in session.Query<Customer>()
                                // where customer.LastName.Length > 5
                                where customer.LastName.StartsWith("G")
                                orderby customer.LastName
                                select customer;
                #endregion

                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}, {1}",
                        customer.FirstName, customer.LastName);
                }
                tx.Commit();
            }
            Console.WriteLine("Press <Enter> to exit");
            Console.ReadLine();
        }

        public static void InsertDataWithNHibernate(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = new Customer
                {
                    FirstName = "Kiara",
                    LastName = "Morales"
                };

                Console.WriteLine("Adding new customer {0} {1} to the Customer table. PK is: {2}",
                    customer.FirstName, customer.LastName, customer.Id);
                session.Save(customer);
                tx.Commit();
                QueryIndividualCustomerLastName(sessionFactory, customer.LastName);
            }
        }

        public static void UpdateDataWithNHibernate(ISessionFactory sessionFactory,
            string lastNameToUpdate,
            string updatedLastName)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = from customer in session.Query<Customer>()
                            where customer.LastName == lastNameToUpdate
                            select customer;

                var found = query.Single();
                if (found != null)
                {
                    found.LastName = updatedLastName;
                }
                else
                {
                    Console.WriteLine("Customer not found. Can't update!");
                }
                session.Update(found);
                tx.Commit();
            }
            QueryIndividualCustomerLastName(sessionFactory, updatedLastName);
        }

        public static void DeleteDataWithNHibernate(ISessionFactory sessionFactory,
            int pkToDelete)
        {
            Console.WriteLine("Before delete...");
            QueryIndividualCustomerPrimaryKey(sessionFactory, pkToDelete);
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = from customer in session.Query<Customer>()
                            where customer.Id == pkToDelete 
                            select customer;

                var found = query.Single();
                if (found != null)
                {
                    session.Delete(found);
                }
                else
                {
                    Console.WriteLine("Customer not found. Can't update!");
                }
                tx.Commit();
            }
            Console.WriteLine("After delete...");
            QueryIndividualCustomerPrimaryKey(sessionFactory, pkToDelete, true);
        }

        #endregion

        public static void MappingMetaData(ISessionFactory sessionFactory)
        {
            #region Intellisense for NHibernate
            /**
             * Drop XSD files
             *  - nhibernate-mapping.xsd
             *  - nhibernate-configuration.xsd
             *  
             * Into
             *  - C:\Program Files\Microsoft Visual Studio 10.0\xml\schemas
             *  or
             *  - C:\Program Files(x86)\Microsoft Visual Studio 10\xml\schemas
             */
            #endregion

            #region Mapping Data Types
            /**
             * int, double, decimal
             * string
             * bool
             * DateTime
             * enum
             * Complex Objects
             * Collections
             */
            #endregion
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                //var customer = CreateCustomer("Mario",
                //    "Mares",
                //    100,
                //    true,
                //    CustomerCreditRating.Good
                //    ...needs additional params to work.
                //    );
                //session.Save(customer);
                tx.Commit();
                //QueryIndividualCustomerLastName(sessionFactory, customer.LastName);
            }
        }

        public static void MappingComponents(ISessionFactory sessionFactory)
        {
            /**
             * Components are value objects
             * Components have no primary key
             * Components are persisted in the same table as the owning object
             * Allow us to separate columns within the database table within the class.
             * Since it doesn't have it's own primary key, we are able to save it to the same table.
             * 
             * example:
             * 
             * public class Customer
             * {
             *      ...properties
             *      public virtual Location Address { get; set; }
             * }
             * 
             * public class Location
             * {
             *      public virtual string Street { get; set; }
             *      public virtual string City { get; set; }
             *      public virtual string Province { get; set; }
             *      public virtual string Country { get; set; }
             * }
             * 
             */ 

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = CreateCustomer("John",
                    "Doe",
                    100,
                    true,
                    CustomerCreditRating.Excellent,
                    42.42424242,
                    new Location()
                    {
                        Street = "123 Somewhere Avenue",
                        City = "Nowhere",
                        Province = "Alberta",
                        Country = "Canada"
                    });
                session.Save(customer);
                tx.Commit();
                QueryIndividualCustomerLastName(sessionFactory, customer.LastName);
            }
        }
    }
}
