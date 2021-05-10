using System;
using System.Collections.Generic;

namespace TestAutomation.Framework.Common
{
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public int Rank { get; set; }
        public int Votes { get; set; }
        public string Engine { get; set; }
        public List<Comment> Comments { get; set; }
    }

    public class Comment
    {
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string Context { get; set; }
    }
}
