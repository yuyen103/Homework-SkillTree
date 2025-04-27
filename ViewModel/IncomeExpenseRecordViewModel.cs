using Homework_SkillTree.Models;
using System.ComponentModel.DataAnnotations;
namespace Homework_SkillTree.ViewModel
{
    /// <summary>
    /// 收入支出資料
    /// </summary>
    public class IncomeExpenseRecordViewModel
    {
        /// <summary>
        /// 類別
        /// 1：收入
        /// 2：支出
        /// </summary>
        [Required]
        [Range(1, 2, ErrorMessage = "請選擇類別")]
        public CategoryType Category { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "金額必須大於 0")]
        public int Amount { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [Required]

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string? Note { get; set; }
    }
}
