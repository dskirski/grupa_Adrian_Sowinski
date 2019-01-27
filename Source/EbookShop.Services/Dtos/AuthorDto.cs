using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EbookShop.Services.Dtos
{
    [DataContract]
   public class AuthorDto
    {
        [DataMember]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataMember]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
