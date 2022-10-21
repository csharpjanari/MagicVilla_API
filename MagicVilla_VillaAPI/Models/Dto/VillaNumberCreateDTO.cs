

using System.Diagnostics.CodeAnalysis;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }
    }
}
