//using Arch.Cqrs.Client.Command.Customer;
//using Arch.Domain.ValueObjects;
//using Arch.Infra.Shared.Cqrs.Commands;
//using Bogus;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using Dapper;
//using Microsoft.Data.Sqlite;

//namespace Arch.Cqrs.Handlers.Customer
//{
//    public class CustomerCommandHandlerDapper: 
//        ICommandHandler<InsertVolumeCustomersDapper>,
//        ICommandHandler<InsertOpenCloseDapper500>
//    {
//        private const string sqlCustomers =
//                "INSERT INTO Customers (Id, AddressId, BirthDate, CreatedDate, EmailAddress, FirstName, LastName, Score) " +
//                "Values (@Id, @AddressId, @BirthDate, @CreatedDate, @EmailAddress, @FirstName, @LastName, @Score);";
//        private const string sqlAddress = "INSERT INTO Addresses (Id, City, CreatedDate, Number, Street , ZipCode) " +
//                "Values (@Id, @City, @CreatedDate, @Number, @Street , @ZipCode)";
//        public void AddSingleCustomer(Domain.Models.Customer customerAdd)
//        {
//            using (IDbConnection dbConnection = Connection)
//            {
//                dbConnection.Open();

//                using (var transaction = dbConnection.BeginTransaction())
//                {
//                    dbConnection.Execute(sqlAddress, customerAdd.Address, transaction: transaction);
//                    dbConnection.Execute(sqlCustomers, customerAdd, transaction: transaction);

//                    transaction.Commit();
//                }
//                dbConnection.Close();
//            }
//        }
//        public void AjouterCustomersAleatoires(int interactions)
//        {

//            var result = "";
//            var list = GetCustomers(interactions);

//            using (IDbConnection dbConnection = Connection)
//            {
//                dbConnection.Open();

//                using (var transaction = dbConnection.BeginTransaction())
//                {
//                     list.ForEach(_ =>
//                         {
                             
//                             dbConnection.Execute(sqlAddress, _.Address, transaction: transaction);
//                             dbConnection.Execute(sqlCustomers, _, transaction: transaction);
//                         }
//                    );

//                    transaction.Commit();
//                }

//            }
         
//        }

//        public void Handle(InsertVolumeCustomersDapper command)
//        {
//            AjouterCustomersAleatoires(command.InsertsCount);
//        }

//        public void Handle(InsertOpenCloseDapper500 command)
//        {
//            GetCustomers(command.InsertsCount).ForEach(_ => AddSingleCustomer(_));
//        }

//        public IDbConnection Connection
//        {
//            get
//            {
//                //var connStrings = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ArchDatabase4;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
//                var connStrings  = "Data Source=ArchDatabase.sqlite";
//                //return new SqlConnection(connStrings);
//                return new SqliteConnection(connStrings);
//            }
//        }

//        public List<Domain.Models.Customer> GetCustomers(int interactions)
//        {
//            string sqlCustomers =
//              "INSERT INTO Customers (Id, AddressId, BirthDate, CreatedDate, EmailAddress, FirstName, LastName, Score) " +
//              "Values (@Id, @AddressId, @BirthDate, @CreatedDate, @EmailAddress, @FirstName, @LastName, @Score);";

//            var sqlAddress = "INSERT INTO Addresses (Id, City, CreatedDate, Number, Street , ZipCode) " +
//                "Values (@Id, @City, @CreatedDate, @Number, @Street , @ZipCode)";

//            var faker = new Faker();
//            var list = new List<Domain.Models.Customer>();
//            for (var i = 0; i < interactions; i++)
//            {
//                var minDate = DateTime.Now.AddYears(-30);
//                var maxDate = DateTime.Now.AddYears(-60);

//                var idAddress = Guid.NewGuid();
//                var address = new Address(faker.Address.StreetName(), faker.Address.BuildingNumber(), faker.Address.City(), faker.Address.ZipCode(), idAddress);
//                var customer = new Domain.Models.Customer(
//                    faker.Name.FirstName(),
//                    faker.Name.LastName(),
//                    faker.Person.Email,
//                    faker.Date.Between(minDate, maxDate)
//                    );
//                customer.Address = address;
//                customer.AddressId = idAddress;

//                list.Add(customer);
//            }

//            return list;
//        }
//    }
//}
