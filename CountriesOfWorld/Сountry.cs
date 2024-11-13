using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CountriesOfWorld
{
    [Index(nameof(Alpha2Code), IsUnique = true)]
    public class Сountry
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Alpha2Code { get; set; }
        public Сountry() {}
    }
}
