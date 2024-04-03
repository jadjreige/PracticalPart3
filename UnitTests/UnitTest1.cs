using Microsoft.AspNetCore.Mvc;
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
        /// The test will reload the database and add a test record. It will then search for that record. The result should contain only 1 record.
        /// </summary>
        [TestMethod]
        public async Task Load_records_when_application_is_first_loaded()
        {
            /// Create context
            DataCenterContext context = DataCenterContext.CreateContext();

            /// Set up the controller
            DataCentersController controller = new(context);

            controller.LoadData().Wait();

            /// Initialize a record 
            var dataToCreate = new DataCenter
            {
                FiscalYear = "Test",
                FiscalPeriod = "Test",
                Month = "Another Test",
                InformationDate = "Test",
                Branch = "Something",
                Service = "Test",
                SscClient = "Other",
                MetricName = "Test",
                Value = 1,
                MetricType = "Test"
            };

            /// Add the record into the database
            context.Add(dataToCreate);
            context.SaveChanges();

            /// Initialize the search text string
            String searchText = "Test";

            /// load index view with the search string "Text"
            var result = await controller.Index(searchText);


            /// Assert: Ensure only one record is returned
            var viewResult = result as ViewResult;

            Assert.IsNotNull(viewResult.Model);
            
            /// Assert: Model should contain only one row
            var model = viewResult.Model as List<DataCenter>;

            Assert.AreEqual(1, model.Count);

            /// Assert: The record should contain the searchString in one of the columns.
            var searchedRecord = model[0];
            var concatenatedString = $"{searchedRecord.FiscalYear} {searchedRecord.FiscalPeriod} {searchedRecord.Month} {searchedRecord.InformationDate} {searchedRecord.Branch} {searchedRecord.Service} {searchedRecord.SscClient} {searchedRecord.MetricName} {searchedRecord.MetricType} {searchedRecord.Value}";

            Assert.IsTrue(concatenatedString.Contains(searchText));
        }
    }
} // Jad Jreige


