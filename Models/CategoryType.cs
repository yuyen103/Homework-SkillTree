using System.ComponentModel.DataAnnotations;

namespace Homework_SkillTree.Models
{
    public enum CategoryType
    {
        [Display(Name = "請選擇")]
        None = 0,

        [Display(Name = "收入")]
        Income = 1,

        [Display(Name = "支出")]
        Expense = 2
    }
}
