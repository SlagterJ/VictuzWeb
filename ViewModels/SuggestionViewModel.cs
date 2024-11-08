using VictuzWeb.Models;

namespace VictuzWeb.ViewModels
{
    public class SuggestionViewModel
    {
        public uint Identifier { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }

        public int likeCount { get; set; }

        public bool Haslike { get; set; }

    }
}
