using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.Common.Entity
{
    /// <summary>
    /// Nguồn ngân sách
    /// </summary>
    public class Budget
    {
        /// <summary>
        /// ID nguồn ngân sách
        /// </summary>
        public Guid BudgetID { get; set; }

        /// <summary>
        /// Mã ngân sách
        /// </summary>
        public string BudgetCode { get; set; }

        /// <summary>
        /// Tên ngân sách
        /// </summary>
        public string BudgetName { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
