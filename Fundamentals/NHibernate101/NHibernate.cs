using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Hql;
using System;
using System.Collections.Generic;
using System.Data;
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
            // MappingComponents(sessionFactory);
            // PrimaryKeys(sessionFactory);
            // NHibernateConfiguration();
            NHibernateRelationships(sessionFactory);

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
            Guid pkToQuery,
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
            Guid pkToDelete)
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
                var address = new Location
                {
                    Street = "123 Somewhere Avenue",
                    City = "Nowhere",
                    Province = "Alberta",
                    Country = "Canada"
                };
                var customer = CreateCustomer("John",
                    "Doe",
                    100,
                    true,
                    CustomerCreditRating.Excellent,
                    42.42424242,
                    address);
                session.Save(customer);
                tx.Commit();
                QueryIndividualCustomerLastName(sessionFactory, customer.LastName); // will not work if same last name is queried (TRUNCATE TABLE [Server_Name].[dbo].[Customer]; to delete all entries with the same last name and try again.
            }
        }

        public static void PrimaryKeys(ISessionFactory sessionFactory)
        {
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                /**
                 * Primary Key Generation
                 * 
                 * - Assigned:
                 *      - Application's responsibility to assign a PK.
                 * - Native:
                 *      - Use Identity, Sequence, or HiLo depending on the database engine
                 * - Identity:
                 *      - Use identity column in supported databases
                 * - Sequence:
                 *      - Use sequence in supported databases (seen with oracle)
                 * - HiLo
                 *      - Use HiLo algorithm with a table (hilo) or a sequence (seqhilo)
                 * - Guid or Guid.Comb
                 *      - Use a Guid primary key generated
                 * - Others...can also create own, but strongly consider using built-in generators.
                 * 
                 * Recommendations for PK Generation Strategies
                 * - Do not use "assigned"
                 *      - NHibernate cannot tell if the object is new or updated
                 *      
                 * - Avoid Identity or sequence
                 *      - NHibernate must make a database roundtrip per new object
                 *      
                 * - Avoid native
                 *      - For most databases, this maps to identity or sequence
                 * 
                 * - If you don't like Guid PKs, use "hilo" or "seqhilo"
                 *      - NHibernate can make a single call to the database to reverse a range of PKs
                 *      - PKs are only unique to a single database
                 *      - integer pks
                 *      
                 * - If you don't mind Guid PKs, use guid.comb
                 *      - NHibernate can assign PKs without calling the database
                 *      - PKs are unique across a database cluster
                 *      - Guid.comb algorithm avoids index fragmentation issues
                 *      - guid pks (guid.comb for incremental guid generation approach).
                 */
                tx.Commit();
            }
        }

        public static void NHibernateConfiguration()
        {
            /**
             * Ways to Configure NHibernate
             * - XML
             *      - hibernate.cfg.xml
             *      - app.config
             * 
             * - Code-based
             *      - Loquacious (built-in to NHibernate)
             *      - Fluent NHibernate
             * 
             * Overriding NHibernate Configuration
             * - Configuration is additive
             * - Last configuration wins
             * 
             * var cfg = new Configuration();
             * cfg.DatabaseIntegration(x =>
             * {
             *      ...configs using built-in Loquacious
             * });
             * 
             * cfg.AddAssembly(Assembly.GetExecutingAssembly();
             * cfg.Configure("hibernate.cfg.xml"); // Loads hibernate.cfg.xml
             */

            // Hard-coded connection-string - valid option
            StringBuilder sb = new StringBuilder();
            sb.Append("Data Source=CALDERONMUNOZ\\BRANDON_LS;");
            sb.Append("Initial Catalog=Brandon_LocalServer;");
            sb.Append("Integrated Security=True;");
            sb.Append("Connect Timeout=30;");
            sb.Append("Encrypt=False;");

            // Loquacious configuration
            var cfg = new Configuration();
            cfg.DataBaseIntegration(
                x =>
                {
                    // x.ConnectionString = "default"; // may or may not be same as using hard-coded value
                    x.ConnectionString = sb.ToString();
                    x.Driver<SqlClientDriver>(); // OracleClientDriver
                    x.Dialect<MsSql2008Dialect>(); // Oracle10gDialect
                    x.IsolationLevel = IsolationLevel.RepeatableRead;
                    x.Timeout = 10; // (seconds)
                    x.BatchSize = 10; // SQL Server and Oracle supported - batch is default to 20 in newer versions of NHibernate.
                    x.LogSqlInConsole = true; // use for debugging, set to false in production
                });
            // cfg.SessionFactory().GenerateStatistics(); // helpful with NHibernate Profiler
            // cfg.SessionFactory().Caching - can be Query or Entity specific caching
            cfg.AddAssembly(Assembly.GetExecutingAssembly());
            var sessionFactory = cfg.BuildSessionFactory();

            /**
             * XML configuration:
             * will automatically read the hibernate.cfg.xml file located in project
             * cfg.Configure("hibernate.cfg.xml");
             * 
             * example hibernate.cfg.xml:
             * 
             * properties of hibernate.cfg.xml file:
             * - Build Action : Content
             * - Copy to Output Direct : Copy if newer
             * - Custom Tool : n/a
             * - Custom Tool Namespace : n/a
             * - File Name : hibernate.cfg.xml
             * - Full Path : C:\Users\<User>\**path to hibernate.cfg.xml
             * 
             * <?xml version="1.0" encoding="utf-8" ?>
             * <hibernate-configuration xmlns="urn:nhibernate-configuration-2.2">
             *      <session-factory>
             *          <property name="connection.connection_string_name">default</property> 
             *          <property name="connection.driver_class">NHibernate.Driver.SqlClientDriver</property> 
             *          <property name="dialect">NHibernate.Dialect.MsSql2008Dialect</property>
             *          <property name="show_sql">true</property>
             *          <mapping assembly="Fundamentals.NHibernate101"/>
             *      </session-factory>
             * </hibernate-configuration>
             */

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                for (var i = 0; i < 25; i++)
                {
                    var newCustomer = CreateCustomer(
                        "Billy",
                        "Bob",
                        100,
                        true,
                        CustomerCreditRating.Terrible,
                        78.23231,
                        new Location
                        {
                            Street = "123 Somewhere Avenue",
                            City = "Nowhere",
                            Province = "Alberta",
                            Country = "Canada"
                        });
                    session.Save(newCustomer);
                }
                tx.Commit();
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }
        }

        public static void NHibernateRelationships(ISessionFactory sessionFactory)
        {
            /**
             * Understanding Relationships
             * 
             * Customer.Orders
             *      - <set><one-to-many class="Order"/></set>
             *      - Two tables, Customer and Order, with a CustomerId on the Order table
             * 
             * Order.Customer
             *      - <many-to-one name="Customer"/>
             *      - Two tables, Customer and Order, with a FK on Order pointing back to its
             *      parent Customer.
             *      
             * Customer.Addresses
             *      - <many-to-many></many-to-many>
             *      - Two tables with a joining table
             *      - Joining table has two FKs, one to each table
             */

            // one-to-many
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var newCustomer = CreateCustomer(
                    "Billy",
                    "Bob",
                    100,
                    true,
                    CustomerCreditRating.Terrible,
                    78.23231,
                    new Location
                    {
                        Street = "123 Somewhere Avenue",
                        City = "Nowhere",
                        Province = "Alberta",
                        Country = "Canada"
                    }
                    //,
                    //{
                    //    new Order
                    //    {
                    //        Ordered = DateTime.Now
                    //    },
                    //    new Order
                    //    {
                    //        Ordered = DateTime.Now.AddDays(-1),
                    //        Shipped = DateTime.Now
                    //        ShipTo = CreateLocation()
                    //    }
                    //}
                    );
                session.Save(newCustomer);
                tx.Commit();
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }
        }
    }
}
