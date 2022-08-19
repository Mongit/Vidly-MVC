using System.Collections.Generic;

namespace Vidly2.DTOs
{
    public class RentalDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public List<int> Movies { get; set; }
    }
}