using VictuzWeb.Models;

namespace VictuzWeb.ViewModels
{
    public class GatheringsViewModel
    {
        public uint Identifier { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IngescrevenUsers { get; set; }

        public uint MaxUsers { get; set; }

        public DateOnly DeadlineDate { get; set; }

        public DateTime BeginDateTime { get; set; }

        public DateTime EndDateTime { get; set; }
    }
}
