using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todolist.Models;

namespace finaltodo.services
{
    public interface Ifinaltodo
    {
        IEnumerable<Todo> GetNotes();
        void DeleteNotes(string title, string label, bool pinned);
        IEnumerable<Todo> PutTodo(int id, Todo todo);
        IEnumerable<Todo> PostTodo(Todo todo);
        IEnumerable<Todo> DeleteTodo(int id);
    }
    public class finaltodoservices : Ifinaltodo
    {
        private readonly List<Todo> _todolist;
        public finaltodoservices()
        {
            _todolist = new List<Todo>()
            {
            new Todo() { id = 1, pinned = false, heading = "abc", text = "write text here" ,label=new List<labels>(){ new labels { id = 1, labelname = "label1" } },checklist=new List<checklist>(){new checklist {id=1,checkname="checklist1" } } }
             };
        }
        public void DeleteNotes(string title, string label, bool pinned)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> DeleteTodo(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> GetNotes()
        {
            return _todolist;
        }

        public IEnumerable<Todo> PostTodo(Todo todo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Todo> PutTodo(int id, Todo todo)
        {
            throw new NotImplementedException();
        }
    }
}
