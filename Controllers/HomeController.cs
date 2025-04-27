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
        private static int _currentId = 1; // �y�����_�l��

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
                // ���ʹ��ո��
                List<IncomeExpenseRecordModel> incomeExpenseRecordData = GenerateIncomeExpenseRecordData();
                _transactions.AddRange(incomeExpenseRecordData);
                _currentId = _transactions.Max(t => t.Id) + 1;
            }
            // �N��ƶǻ��� View
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
                _transactions.Add(recordModel); // �s�W��ƨ�C��
                TempData["Success"] = "�s�W���\�I";
                //_context.IncomeExpenseRecord.Add(recordModel);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index"); // ���s�ɦV�� Index ����
            }
            else
            {
                // �����Ҧ����~�T��
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                // ����~�T����_��
                var errorMessage = string.Join("; ", errors);
                TempData["Error"] = errorMessage;
                // �p�G���ҥ��ѡA��^ Index �����A����ܥثe�����
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
                    Id = i, // �y����
                    Category = (CategoryType)new Random().Next(1, 3),
                    Amount = new Random().Next(1, 10000), // �H�����B�A�d�� 1 ~ 10000
                    Date = DateTime.Now.AddDays(-i), // ��������ѩ��e�� i ��
                    Note = $"�o�O�� {i} �����ո��"
                });
            }
            return data;
        }
    }
}
