using System.Collections.Generic;

namespace TGPro.Data.Entities
{
    public class Condition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
