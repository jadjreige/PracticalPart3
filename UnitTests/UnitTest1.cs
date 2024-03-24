using Microsoft.EntityFrameworkCore;
using PracticalPart3.Controllers;
using PracticalPart3.Data;
using PracticalPart3.Models;

namespace UnitTests
{

    [TestClass]
    public class DataCentersControllerTest
    {


        [TestMethod]
        public void Load_records()
        {
            DataCenterContext context = DataCenterContext.CreateContext();

            DataCentersController controller = new(context);

            var list = context.DataCenter.ToList();


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

            context.Add(dataToCreate);

            context.SaveChanges();

            list = context.DataCenter.ToList();

            controller.LoadData().Wait();

            Assert.IsTrue(list.Count == 101);


            list = context.DataCenter.ToList();

            //The database should have 100 records when it gets migrated on launch
            Assert.IsTrue(list.Count == 100);

        }
    }
}


