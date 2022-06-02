using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserBook
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public ReadingState ReadingStatus { get; set; }
        public DateTime LastRead { get; set; }
    }
    public enum ReadingState
    {
        Reading = 0,
        Completed = 1,
    }
}
