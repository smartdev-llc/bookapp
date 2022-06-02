using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests
{
    public class BookReadingRequest
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int CurrentPage { get; set; }

    }
}
