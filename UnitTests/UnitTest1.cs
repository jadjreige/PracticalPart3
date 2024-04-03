using Microsoft.EntityFrameworkCore;
using PracticalPart3.Controllers;
using PracticalPart3.Data;
using PracticalPart3.Models;

namespace UnitTests
{

    [TestClass]
    public class DataCentersControllerTest
    {

        /// <summary>
        /// The test will run the application when it is firsdt loaded. The result should be a fresh dataset with 100 records.
        /// </summary>
        [TestMethod]
        public void Load_records_when_application_is_first_loaded()
        {
            /// Create context
            DataCenterContext context = DataCenterContext.CreateContext();

            /// Set up the controller
            DataCentersController controller = new(context);

            var list = context.DataCenter.ToList();

            /// Initialize a record 
            var dataToCreate = new DataCenter
            {
                FiscalYear = "Test",
                FiscalPeriod = "Test",
                Month = "Test",
                InformationDate = "Test",
                Branch = "Test",
                Service = "Test",
                SscClient = "Test",
                MetricName = "Test",
                Value = 1,
                MetricType = "Test"
            };

            /// Add the record into the database
            context.Add(dataToCreate);
            context.SaveChanges();

            /// Load data, this should erase everything that was done and load 100 rows
            controller.LoadData().Wait();

            /// Get updated list
            list = context.DataCenter.ToList();

            /// The database should have 100 records
            Assert.IsTrue(list.Count == 100);

        }
    }
} // Jad Jreige


