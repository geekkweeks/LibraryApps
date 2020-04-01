using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.ServiceModels
{
    public class BookTransactionServiceModel
    {
        public int? TransactionId { get; set; }
        [Required]
        public int BookId { get; set; }
        
        public Nullable<System.DateTime> StartDate { get; set; }
        
        public Nullable<System.DateTime> EndDate { get; set; }
        [Required]
        public double Total { get; set; }
        public string CreatedBy { get; set; }
        public string FullName { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Days { get; set; }
        public string Author { get; set; }
        public string BookTitle { get; set; }
        public string CategoryName { get; set; }
        public string StatusName { get; set; }
    }
}
