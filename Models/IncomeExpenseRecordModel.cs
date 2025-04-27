using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_SkillTree.Models
{
    public class IncomeExpenseRecordModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 讓資料庫自己 AutoIncrement
        public int Id { get; set; }

        /// <summary>
        /// 類別
        /// 1：收入
        /// 2：支出
        /// </summary>
        public CategoryType Category { get; set; }

        /// <summary>
        /// 金額
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string? Note { get; set; }
    }
}
