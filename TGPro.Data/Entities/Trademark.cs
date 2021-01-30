using System.Collections.Generic;
using TGPro.Data.Enums;

namespace TGPro.Data.Entities
{
    public class Trademark
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string PublicId { get; set; }
    }
}
