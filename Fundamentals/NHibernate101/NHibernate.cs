using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Hql;
using NHibernate.Linq;
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
            // NHibernateRelationships(sessionFactory);
            // NHibernateCascades(sessionFactory);
            // NHibernateLazyLoading(sessionFactory);
            // NHibernateUnderstandInverseEqualsTrue(sessionFactory);
            // NHibernateGetVsLoad(sessionFactory);
            // NHibernateLINQ(sessionFactory);
            // NHibernateHQL(sessionFactory);
            NHibernateCriteriaQueries(sessionFactory);
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
            bool hold = false,
            bool innerJoinFetch = false)
        {
            
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                Console.WriteLine("Loaded");
                var query = from customer in session.Query<Customer>()
                where customer.Id == pkToQuery 
                select customer;

                try
                {
                    if (innerJoinFetch)
                    {
                        // var found = query.First(); two queries
                        // var found = query.Fetch(x => x.Orders).First(); // one query
                        // Console.WriteLine(found);

                        // n + 1 queries will happen when using query.ToList();
                        // var reloaded = query.ToList();
                        // Console.WriteLine("Reloaded");
                        // foreach (var customer in reloaded)
                        // {
                        // Console.WriteLine(customer);
                        // foreach (var order in customer.Orders)
                        // {
                        // Console.WriteLine(order);
                        // }
                        // }

                        // instead, do eager join per query basis to reduce number of round trips to db:
                        var reloaded = query.Fetch(x => x.Orders).ToList();
                        Console.WriteLine("Reloaded");
                        foreach (var customer in reloaded)
                        {
                            Console.WriteLine(customer);
                        }
                    }
                    else
                    {
                        var found = query.Single();
                        Console.WriteLine("Reloaded");
                        Console.WriteLine("Found Customer: {0} {1}, PK: {2}",
                            found.FirstName, found.LastName, found.Id);
                    }
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
            Location location,
            ISet<Order> orders)
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
                Address = location,
                Orders = orders
            };
        }

        public static Location CreateLocation(string street,
            string city,
            string province,
            string country)
        {
                return new Location
                {
                    Street = street,
                    City = city,
                    Province = province,
                    Country = country
                };
        }

        #endregion
        


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
                //var address = new Location
                //{
                //    Street = "123 Somewhere Avenue",
                //    City = "Nowhere",
                //    Province = "Alberta",
                //    Country = "Canada"
                //};
                //var customer = CreateCustomer("John",
                //    "Doe",
                //    100,
                //    true,
                //    CustomerCreditRating.Excellent,
                //    42.42424242,
                //    address);
                //session.Save(customer);
                tx.Commit();
                //QueryIndividualCustomerLastName(sessionFactory, customer.LastName); // will not work if same last name is queried (TRUNCATE TABLE [Server_Name].[dbo].[Customer]; to delete all entries with the same last name and try again.
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
                //for (var i = 0; i < 25; i++)
                //{
                //    var newCustomer = CreateCustomer(
                //        "Billy",
                //        "Bob",
                //        100,
                //        true,
                //        CustomerCreditRating.Terrible,
                //        78.23231,
                //        new Location
                //        {
                //            Street = "123 Somewhere Avenue",
                //            City = "Nowhere",
                //            Province = "Alberta",
                //            Country = "Canada"
                //        });
                //    session.Save(newCustomer);
                //}
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
            // OneToOne(sessionFactory);
            // OneToMany(sessionFactory);
            // Lists(sessionFactory);
            // Sets(sessionFactory);
            // Bags(sessionFactory);
            // Others(sessionFactory);
        }

        private static void OneToOne(ISessionFactory sessionFactory)
        {
            /**
             * Customer.Person
             * <one-to-one name="Person" />
             * Two tables, Customer and Person, share same PK
             * i.e. one person for every customer.
             * 
             * In general, avoid <one-to-one/>
             * 
             * Use inheritance
             * - Customer IS-A Person
             * 
             * Use <many-to-one>
             * - Customer HAS-A DefaultShippingAddress
             * - Customers table has a standard FK to Addresses
             * - <many-to-one name="DefaultShippingAddress" />
             */
            throw new NotImplementedException();
        }
        
        public static void OneToMany(ISessionFactory sessionFactory)
        {
            // one-to-many - no cascade="save-update" or casecade="all-delete-orphan" in hbm.xml
            Guid id;
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                ISet<Order> orders = new HashSet<Order>
                {
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                    },
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                        ShipTo = CreateLocation("234 Another Avenue",
                    "Somewhere",
                    "Omaha",
                    "United Estatedes")
                    }
                };

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
                    },
                    orders);
                session.Save(newCustomer);
                // without cascade="save-update" we would have to do something like this.
                // not ideal, given nhibernate has some baked in solutions for this exact
                // scenario.
                foreach (var order in newCustomer.Orders)
                {
                    session.Save(order);
                }
                id = newCustomer.Id;
                tx.Commit();
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
                QueryIndividualCustomerPrimaryKey(sessionFactory, id);
            }
        }
        
        private static void Lists(ISessionFactory sessionFactory)
        {
            /**
             * List
             * - Ordered collection of non-unique elements
             * - Mapped using System.Collections.Generic.IList<T>
             */
            throw new NotImplementedException();
        }

        private static void Sets(ISessionFactory sessionFactory)
        {
            /**
             * Set
             * - Unordered collection of unique elements
             * - Mapped using lesi.Collections.Generic.ISet<T>
             * - Or HashSet<T>(.NET 4.0+ only)
             */
            throw new NotImplementedException();
        }

        private static void Bags(ISessionFactory sessionFactory)
        {
            /**
             * Bag
             * Unordered collection of non-unique elements
             * Mapped using System.Collections.Generic.IList<T>
             */
            throw new NotImplementedException();
        }

        private static void Others(ISessionFactory sessionFactory)
        {
            /**
             * - Map(HashTable or Dictionary<K, T>)
             * - Array
             * - Primitive Array
             * - IdBag
             */
            throw new NotImplementedException();
        }

        private static void NHibernateCascades(ISessionFactory sessionFactory)
        {
            /**
             * Cascade:
             * - Tells NHibernate how to handle child entites
             * - Options:
             *      - none: no cascades (default)
             *      - all: cascade saves, updates, and deletes
             *      - save-update: cascade saves and updates
             *      - delete: cascade deletes
             *      - all-delete-orphan: same as all and delete orphaned rows
             * - Can specify default-cascade in hbm.xml file
             */
            // one-to-many - cascade="save-update" or casecade="all-delete-orphan" in hbm.xml
            Guid id;
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                ISet<Order> orders = new HashSet<Order>
                {
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                    },
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                        ShipTo = CreateLocation("234 Another Avenue",
                    "Somewhere",
                    "Omaha",
                    "United Estatedes")
                    }
                };

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
                    },
                    orders);
                session.Save(newCustomer); // no need to loop and session.Save each child entity.
                tx.Commit();
                id = newCustomer.Id;
                QueryIndividualCustomerPrimaryKey(sessionFactory, id);
            }
        }

        public static void NHibernateLazyLoading(ISessionFactory sessionFactory)
        {
            /**
             * LazyLoading
             * 
             * - By default, if a Customer is pulled in, it won't load in all of the orders.
             * - Associations lazy loaded by default.
             * - Requires open ISession
             * - Fetching strategies
             *      - Select, outer-join
             * - Avoiding the N + 1 SELECT problem
             *      - Load a customer (query), customer has orders (query):
             *      - for each order in that collection I need the order line items,
             *      - n + 1 queries to database
             *      - this is where outer-joins could come into help.
             */
            Guid id;
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                ISet<Order> orders = new HashSet<Order>
                {
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                    },
                    new Order()
                    {
                        Ordered = DateTime.Now.AddDays(1),
                        Shipped = DateTime.Now,
                        ShipTo = CreateLocation("234 Another Avenue",
                    "Somewhere",
                    "Omaha",
                    "United Estatedes")
                    }
                };

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
                    },
                    orders);
                session.Save(newCustomer); // no need to loop and session.Save each child entity.
                tx.Commit();
                id = newCustomer.Id;
                QueryIndividualCustomerPrimaryKey(sessionFactory, id, false, true); // see method.
            }

        }

        private static void NHibernateUnderstandInverseEqualsTrue(ISessionFactory sessionFactory)
        {
            /**
             * Understanding Inverse="true"
             * 
             * - Relational model
             *      - Bidirectional associations using one FK
             * - OO model
             *      - Unidirectional associations using references
             *      
             * Nhibernate doesn't have enough information to know that Customer.Orders
             * and Order.Customer represent the same relationship in the db.
             * 
             * So inverse="true" is provided as a hint to Nhibernate.
             * 
             * - Bidirectional associations in the OO
             *      - Two unidirectional associations with the same data.
             * - Inverse="true" tells NHibernate which one to ignore.
             * - Prevents duplicate updates of FK.
             * - Prevents FK violations
             * - Which table owns the FK?
             *      - <many-to-one /> and <one-to-many /> collection
             *          - inverse is <one-to-many /> collection
             *          
             *      - <many-to-many />
             *          - choose either
             *  N.B. Cascades are an orthogonal concept. Do not convolute concepts
             *  of cascade and inverse.
             */
        }

        private static void NHibernateGetVsLoad(ISessionFactory sessionFactory)
        {
            /**
             * Get vs. Load
             * 
             * ISession.Get<T>(Id)
             * - Returns object or null
             * 
             * ISession.Load<T>(Id)
             * - Returns object or throws ObjectNotFoundException
             * 
             * Load can optimize database roundtrips
             * - Load returns a proxy object
             * - Load need not access the db immediately
             * - Get must return null if object does not exist (bc of clr)
             * - Get must access the db immediately
             * 
             * If session.Load<Customer>(goodId) is called again (after first time),
             * a proxy will not be returned since the object has already been 
             * loaded.
             */
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                // var goodId = Guid.Parse("160C861B-E852-4E8F-995F-B0390005F5D3");
                // var badId = Guid.Parse("160C861B-FFFF-FFFF-995F-B0390005F5D3");

                // var customer = session.Get<Customer>(goodId); // does not return proxy object
                // var badCustomer = session.Get<Customer>(badId); // same as good, but throws nothing 
                // var customer = session.Load<Customer>(goodId); // returns a proxy object (step through)
                // var badCustomer = session.Load<Customer>(badId); // throws an ObjectNotFoundException

                // Console.WriteLine(customer); // proxy will be loaded once it's properties are accessed.
                // try
                // {
                //    Console.WriteLine(badCustomer); // ObjectNotFound
                // }
                // catch (ObjectNotFoundException onfe)
                // {
                //    Console.WriteLine("Customer not found!");
                // }
                // finally
                // {
                //    tx.Commit();
                // }

                //ISet<Order> orders = new HashSet<Order>
                //{
                //    new Order(),
                //    new Order()
                //};

                //// Add a new customer to the db.
                //var newCustomer = CreateCustomer(
                //    "Mary",
                //    "Jane",
                //    25,
                //    true,
                //    CustomerCreditRating.Excellent,
                //    98.21231213,
                //    new Location
                //    {
                //        Street = "123 Somewhere Avenue",
                //        City = "Nowhere",
                //        Province = "Alberta",
                //        Country = "Canada"
                //    },
                //    orders);
                //session.Save(newCustomer);

                // Use session.Get and session.Load based off id in db
                var goodId = Guid.Parse("34B8B925-DEF5-42EA-9EDE-B0390148D335");
                var badId = Guid.Parse("160C861B-FFFF-FFFF-995F-B0390005F5D3");

                var order = new Order();
                //order.Customer = session.Get<Customer>(goodId); // already have the pk
                order.Customer = session.Load<Customer>(goodId); // don't need to load record to insert
                session.Save(order);

                // Often .Load is used to optimize Insert statements.
                tx.Commit();
            }
        }
        
        private static void NHibernateLINQ(ISessionFactory sessionFactory)
        {
            /**
             * NHibernate LINQ
             * 
             * - Accessed through ISession.Query<T>()
             * - Use method chain syntax:
             * 
             * var query = session.Query<Customer>()
             *                    .Where(c => c.FirstName == "John");
             *                    
             * Or query comprehensions
             * 
             * var query = from c in session.Query<Customer>()
             *             where c.FirstName == "John"
             *             select c;
             *             
             * N.B. Be carefule of unintentional multiple enumeration
             */
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                //Object chaining syntax:
                //var query = session.Query<Customer>()
                //            .Where(c => c.FirstName.StartsWith("M"));

                //Query comprehensions syntax (same query as above):
                //var query = from customer in session.Query<Customer>()
                //            where customer.FirstName.StartsWith("M")
                //            select customer;

                /**
                 * Both will generate the exact same sql to generate the customers.
                 */

                //Complicated queries
                //var query = from customer in session.Query<Customer>()
                //            where customer.Orders.Count > 2
                //            orderby customer.FirstName, customer.LastName
                //            select customer;

                //foreach (var customer in query.ToList())
                //{
                //    Console.WriteLine(customer);
                //}

                var query = from c in session.Query<Customer>()
                            where c.Orders.Count > 1
                            orderby c.FirstName, c.LastName
                            select new // new Type
                            {
                                c.FirstName,
                                c.LastName,
                                OrderCount = c.Orders.Count
                            };
                
                foreach (var stat in query.ToList()) // query.ToList() is a List<new Type>
                {
                    Console.WriteLine("{0} {1} placed {2} orders",
                        stat.FirstName, stat.LastName, stat.OrderCount);
                }

                // var customers = query.AsEnumerable(); // will query more than once
                // var customers = query.AsQueryable(); // also query more than once

                tx.Commit();
            }
        }
        
        private static void NHibernateHQL(ISessionFactory sessionFactory)
        {
            /**
             * Hibernate Query Language (HQL)
             * 
             * - Oldest query mechanism (along with Criteria)
             * - Accessed through ISession.CreateQuery()
             * - HQL is SQL-Like
             * 
             * var query = session.CreateQuery(
             *      @"
             *      SELECT C
             *      FROM CUSTOMER C
             *      WHERE C.FirstName = 'Mary'");
             */
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = session.CreateQuery("select c from Customer c " +
                                                "where c.Orders.size > 1 " +
                                                "order by c.FirstName asc, c.LastName asc");
                foreach (var customer in query.List<Customer>()) // cast here, even if not required.
                {
                    Console.WriteLine(customer + "\n");
                }
                tx.Commit();
            }
        }
        
        private static void NHibernateCriteriaQueries(ISessionFactory sessionFactory)
        {
            /**
             * Criteria Queries
             * 
             * - Classic criteria query syntax
             * 
             * var query = session.CreateCriteria<Customer>()
             *                    .Add(Restrictions.Eq("FirstName", "John"));
             *                    
             * - LINQ-style method chain syntax
             * 
             * var query = session.QueryOver<Customer>()
             *                    .Where(x => x.FirstName == "John");
             *                    
             * - Can't mix and match syntax within the same query, but can within the same app.
             */
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {

                // classic syntax
                //var query = session.CreateCriteria<Customer>()
                //                    // (Restrictions.Eq...
                //                    // (Restrictions.NotEq...
                //                   .Add(Restrictions.Like("FirstName", "M%"));

                // LINQ-style syntax
                var query = session.QueryOver<Customer>()
                                   .Where(c => c.FirstName == "Mary");
                // 'cept, can't use c.FirstName.StartsWith method bc of runtime exception
 
                foreach (var customer in query.List<Customer>())
                {
                    Console.WriteLine(customer + "\n");
                }

                tx.Commit();
            }
        }
    }
}
