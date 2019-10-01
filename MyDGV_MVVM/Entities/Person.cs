using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDGV_MVVM.Entities
{
    [Table ("tblPeople")]
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(100)]
        public string Surname { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        [Required, StringLength(150)]
        public string Email { get; set; }
        [Required, StringLength(250)]
        public string Photo { get; set; }
        [Required, StringLength(10)]
        public string Gender { get; set; }
    }
}
