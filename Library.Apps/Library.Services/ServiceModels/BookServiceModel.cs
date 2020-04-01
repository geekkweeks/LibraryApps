using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.ServiceModels
{
    public class BookServiceModel
    {
        public int BookId { get; set; }

        [Required]
        public string BookTitle { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public double? Price { get; set; }
        public bool IsAvailable { get; set; }
        public string CategoryName { get; set; }
    }
}
