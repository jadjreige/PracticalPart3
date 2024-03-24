using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PracticalPart3.Data;
using PracticalPart3.Models;
using PracticalPart3.Migrations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace PracticalPart3.Controllers{
    public class DataCentersController : Controller
    {
        DTO list = DTO.DataCenterList;

        private readonly DataCenterContext _context;

        public DataCentersController(DataCenterContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Loads the database to a list.
        /// </summary>
        /// <returns>Index view</returns>
        public async Task<IActionResult> Index() {
            return View(await _context.DataCenter.ToListAsync());
        }

        /// <summary>
        /// Deletes current data in the data base and reloads 100 new records. 
        /// Creates new database if it does not exist
        /// </summary>
        /// <returns>Index view</returns>
        public async Task<IActionResult> LoadData()
        {

            if (!_context.Database.CanConnect())
            {
                _context.Database.Migrate();
            }

            _context.Database.ExecuteSqlRaw($"DELETE FROM DataCenter;");

            DTO data = list.LoadData();
            foreach (var item in data)
            {
                DataCenter dc = new DataCenter()
                {
                    Branch = item.Branch,
                    FiscalPeriod = item.FiscalPeriod,
                    InformationDate = item.InformationDate,
                    MetricName = item.MetricName,
                    MetricType = item.MetricType,
                    Month = item.Month,
                    Service = item.Service,
                    FiscalYear = item.FiscalYear,
                    SscClient = item.SscClient,
                    Value = item.Value,

                };
                _context.Add(dc);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Method that saves the database records to a csv file in the prefered location 
        /// </summary>
        public IActionResult SaveData()
        {
            using (var writer = new StreamWriter(@"C:\\Users\\Jadan\\source\\repos\\PracticalPart3\\PracticalPart3\\SavedData.csv"))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(_context.DataCenter.ToList());
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// GET: gets the details of a records
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details view</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataCenter == null)
            {
                return NotFound();
            }

            return View(dataCenter);
        }

        /// <summary>
        /// GET: Goes to the view for create
        /// </summary>
        /// <returns>Create view</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: creates new record in the database
        /// </summary>
        /// <param name="dataCenter"></param>
        /// <returns>Index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FiscalYear,FiscalPeriod,Month,InformationDate,Branch,Service,SscClient,MetricName,Value,MetricType")] DataCenter dataCenter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataCenter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataCenter);
        }

        /// <summary>
        /// GET: gets details of a record by Id to edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Edit view</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter.FindAsync(id);
            if (dataCenter == null)
            {
                return NotFound();
            }
            return View(dataCenter);
        }

        /// <summary>
        /// POST: updates editted record to the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dataCenter"></param>
        /// <returns>Index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FiscalYear,FiscalPeriod,Month,InformationDate,Branch,Service,SscClient,MetricName,Value,MetricType")] DataCenter dataCenter)
        {
            if (id != dataCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataCenter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataCenterExists(dataCenter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dataCenter);
        }

        /// <summary>
        /// GET: gets record by Id to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Delete view</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataCenter = await _context.DataCenter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dataCenter == null)
            {
                return NotFound();
            }

            return View(dataCenter);
        }

        /// <summary>
        /// POST: deletes record from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Index view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataCenter = await _context.DataCenter.FindAsync(id);
            if (dataCenter != null)
            {
                _context.DataCenter.Remove(dataCenter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataCenterExists(int id)
        {
            return _context.DataCenter.Any(e => e.Id == id);
        }
    }
}
