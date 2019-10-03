using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Bogus.DataSets.Name;

namespace MyDGV_MVVM.Helpers
{
    public abstract class Helper
    {
        public static List<Entities.Person> CreateRandPeople(int countOfPeople)
        {
            if (countOfPeople <= 0)
                return null;

            List<Entities.Person> randBabies = new List<Entities.Person>();
            Random random = new Random();
            var generator = new Faker<Entities.Person>("uk")
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(p => p.Name, (f, p) => f.Name.FirstName(ReturnGenderType(p.Gender)))
                .RuleFor(p => p.Surname, (f, p) => f.Name.LastName(ReturnGenderType(p.Gender)))
                .RuleFor(p => p.Birthdate, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.Name, p.Surname))
                .RuleFor(p => p.Photo, (f, p) => ReturnUrlPhoto(random, p.Gender));

            randBabies = generator.Generate(countOfPeople);
            return randBabies;
        }

        public static Entities.Person CreateRandPerson()
        {
            Random random = new Random();
            var generator = new Faker<Entities.Person>("uk")
                .RuleFor(p => p.Gender, f => f.PickRandom<Gender>().ToString())
                .RuleFor(p => p.Name, (f, p) => f.Name.FirstName(ReturnGenderType(p.Gender)))
                .RuleFor(p => p.Surname, (f, p) => f.Name.LastName(ReturnGenderType(p.Gender)))
                .RuleFor(p => p.Birthdate, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
                .RuleFor(p => p.Email, (f, p) => f.Internet.Email(p.Name, p.Surname))
                .RuleFor(p => p.Photo, (f, p) => ReturnUrlPhoto(random, p.Gender));
            return generator.Generate();
        }

        private static Bogus.DataSets.Name.Gender ReturnGenderType(string genderString)
        {
            if (genderString == "Male")
            {
                return Bogus.DataSets.Name.Gender.Male;
            }
            else
            {
                return Bogus.DataSets.Name.Gender.Female;
            }
        }

        private static string ReturnUrlPhoto(Random rand, string genderString)
        {
            string urlman1 = "https://consequenceofsound.net/wp-content/uploads/2016/09/screen-shot-2016-09-08-at-12-08-28-am.png";
            string urlman2 = "https://i.pinimg.com/originals/22/06/60/220660a5ebf66d4a3e9647a40b46d0c2.jpg";
            string urlman3 = "https://www.thewrap.com/wp-content/uploads/2018/01/Screen-Shot-2018-01-19-at-11.44.51-AM.png";
            string urlwoman1 = "https://www.hindustantimes.com/rf/image_size_960x540/HT/p2/2019/09/20/Pictures/_92830774-db6b-11e9-89b8-e15e15df329c.PNG";
            string urlwoman2 = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-871092988-1552921582.jpg?crop=1.00xw:0.668xh;0,0.0341xh&resize=480:*";
            string urlwoman3 = "https://i.pinimg.com/originals/c6/9e/a3/c69ea3ea8fff9d8325bc1fc70fad9a0b.jpg";
            
            if (genderString == "Male")
            {
                switch (rand.Next(3))
                {
                    case 0:
                        return urlman1;
                    case 1:
                        return urlman2;
                    case 2:
                        return urlman3;
                    default:
                        return urlman1;
                }
            }
            else
            {
                switch (rand.Next(3))
                {
                    case 0:
                        return urlwoman1;
                    case 1:
                        return urlwoman2;
                    case 2:
                        return urlwoman3;
                    default:
                        return urlwoman1;
                }
            }

        }
    }
}
