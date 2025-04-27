using System;
using System.Diagnostics;
using Homework_SkillTree.Data;
using Homework_SkillTree.Models;
using Homework_SkillTree.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Homework_SkillTree.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly HomeworkServiceContext _context;
        private static List<IncomeExpenseRecordModel> _transactions = new List<IncomeExpenseRecordModel>();
        private static int _currentId = 1; // 流水號起始值

        public HomeController(
            ILogger<HomeController> logger
            //HomeworkServiceContext context
        )
        {
            _logger = logger;
            //_context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_transactions.Count == 0)
            {
                // 產生測試資料
                List<IncomeExpenseRecordModel> incomeExpenseRecordData = GenerateIncomeExpenseRecordData();
                _transactions.AddRange(incomeExpenseRecordData);
                _currentId = _transactions.Max(t => t.Id) + 1;
            }
            // 將資料傳遞到 View
            return View(_transactions);
            //var incomeExpenseRecord = _context.IncomeExpenseRecord.ToList();
            //return View(incomeExpenseRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IncomeExpenseRecordViewModel model)
        {
            if (ModelState.IsValid)
            {
                _currentId = _transactions.Max(t => t.Id) + 1;
                var recordModel = new IncomeExpenseRecordModel
                {
                    Id = _currentId,
                    Category = model.Category,
                    Amount = model.Amount,
                    Date = model.Date,
                    Note = model.Note
                };
                _transactions.Add(recordModel); // 新增資料到列表
                TempData["Success"] = "新增成功！";
                //_context.IncomeExpenseRecord.Add(recordModel);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // 重新導向到 Index 頁面
            }
            else
            {
                // 收集所有錯誤訊息
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                // 把錯誤訊息串起來
                var errorMessage = string.Join("; ", errors);
                TempData["Error"] = errorMessage;
                // 如果驗證失敗，返回 Index 頁面，並顯示目前的資料
                //var _transactions = _context.IncomeExpenseRecord.ToList();
                return View("Index", _transactions);            
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private static List<IncomeExpenseRecordModel> GenerateIncomeExpenseRecordData()
        {
            var data = new List<IncomeExpenseRecordModel>();
            for (int i = 1; i < 4; i++)
            {
                data.Add(new IncomeExpenseRecordModel
                {
                    Id = i, // 流水號
                    Category = (CategoryType)new Random().Next(1, 3),
                    Amount = new Random().Next(1, 10000), // 隨機金額，範圍 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // 日期為今天往前推 i 天
                    Note = $"這是第 {i} 筆測試資料"
                });
            }
            return data;
        }
    }
}
