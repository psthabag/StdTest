using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace StdTest.Models
{
    public class Students
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string lName { get; set; }
        public string Address { get; set; }
        public long Mobile { get; set; }
    }
}
