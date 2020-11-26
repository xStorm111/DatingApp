using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PhotoUrl { get; set; }

        //automapper is smart enough to realize that this comes from getAge 
        //he does this by assuming Get{Property} and the type
        public int Age { get; set; }

        public string KnownAs { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastActive { get; set; }

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public ICollection<PhotoDto> Photos { get; set; } //1 user too N photos
    }
}