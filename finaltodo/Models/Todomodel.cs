using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Todolist.Models
{
    public class labels
    {
        public int id { get; set; }
        public string labelname { get; set; }
    }
    public class checklist
    {
        public int id { get; set; }
        public string checkname { get; set; }
    }
    public class Todo
    {
        

        public int id { get; set; }
        public bool pinned { get; set; }
        public string heading { get; set; }
        public string text { get; set; }
        public List<labels> label { get; set; }
        public List<checklist> checklist { get; set; }



    }
    
}
