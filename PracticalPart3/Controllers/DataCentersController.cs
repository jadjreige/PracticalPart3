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

        // GET: DataCenters
        public async Task<IActionResult> Index(String searchString) {

            var dataCenters = from dc in _context.DataCenter select dc;

            ViewData["IsSearchPerformed"] = !String.IsNullOrEmpty(searchString);

            if (!String.IsNullOrEmpty(searchString))
            {
                //double.TryParse(searchString, out double searchValue);

                dataCenters = dataCenters.Where(dc =>
                dc.Branch.Contains(searchString) ||
                dc.FiscalPeriod.Contains(searchString) ||
                dc.InformationDate.Contains(searchString) ||
                dc.MetricName.Contains(searchString) ||
                dc.MetricType.Contains(searchString) ||
                dc.Month.Contains(searchString) ||
                dc.Service.Contains(searchString) ||
                dc.FiscalYear.Contains(searchString) ||
                dc.SscClient.Contains(searchString) ||
                dc.Value.ToString().Contains(searchString));

            }

            return View(await dataCenters.ToListAsync());
        }

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
        /// Method that saves the DTO list to a csv file in the prefered location 
        /// </summary>
        public IActionResult SaveData()
        {
            using (var writer = new StreamWriter(@"C:\\Users\\Jadan\\source\\repos\\jadjreige\\PracticalPart3\\PracticalPart3\\SavedData.csv"))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                csv.WriteRecords(_context.DataCenter.ToList());
            }
            return RedirectToAction("Index");
        }

        // GET: DataCenters/Details/5
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

        // GET: DataCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: DataCenters/Edit/5
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

        // POST: DataCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: DataCenters/Delete/5
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

        // POST: DataCenters/Delete/5
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
