using System;
using System.ComponentModel.DataAnnotations;
using Vidly2.Models;

namespace Vidly2.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte MembershipTypeId { get; set; }

        public MembershipTypeDTO MembershipType { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }

        public int NumberOfRentedMovies { get; set; }
    }
}