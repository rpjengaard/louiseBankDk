using System;

namespace louiseBank.Models
{
    public class Case
    {
        public int Id { get; set; }

        public string Headline { get; set; }

        public string Customer { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public int SortOrder { get; set; }
    }
}