using System;
using System.Collections.Generic;
using Xunit;
using Microsoft.EntityFrameworkCore;
using finaltodo.Controllers;
using Todolist.Models;
using finaltodo.Models;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace XUnitTesttodo
{
    public class UnitTest1
    {
        finaltodoContext _context;
        TodoesController _controller;
        public TodoesController GetController()
          {
            var optionsBuilder = new DbContextOptionsBuilder<finaltodoContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            

            _context = new finaltodoContext(optionsBuilder.Options);
            _controller = new TodoesController(_context);
            createdata(optionsBuilder.Options);
            return new TodoesController(_context);
        }     
     
       public void createdata(DbContextOptions<finaltodoContext> _context)
        {
            using (var g_keep_context = new finaltodoContext(_context))
            {
                var notes = new List<Todo>()
            {
                new Todo()
                {
                    id=1,
                    pinned=true,
                    heading="header1",
                    text = "write text here",
                    label=new List<labels>
                    {
                        new labels
                        {
                                labelname="label1"
                        }
                    },
                    checklist=new List<checklist>
                    {
                        new checklist
                        {
                            checkname="checklist1"
                        }
                    }
                },
                new Todo()
                {
                   id=2,
                    pinned=true,
                    heading="header2",
                    text = "write text here",
                    label=new List<labels>
                    {
                        new labels
                        {
                                labelname="label2"
                        }
                    },
                    checklist=new List<checklist>
                    {
                        new checklist
                        {
                            checkname="checklist1"
                        }
                    }
                }
            };
                g_keep_context.Todo.AddRange(notes);
                var CountOfEntitiesBeingTracked = g_keep_context.ChangeTracker.Entries().Count();
                g_keep_context.SaveChanges();
         }
        }
        [Fact]
        public async void Gettest()
        {
            var _controller = GetController(); 
            var result = await _controller.GetNotes(null ,null, true);

            var OkObj = result as OkObjectResult;

            var value = OkObj.Value as List<Todo>;
            foreach (var a in value)
            {
                Console.WriteLine(a.id);
            }
            Assert.Equal(2, value.Count);




            //var okResult = await _controller.GetNotes(null, null, true) as OkObjectResult;
            //var result = okResult.Value as List<Todo>;
            //Console.WriteLine(result);
            //Assert.Equal(2, result.Count);
        }
        //public async void Deletetest()
        //{
        //    var result = await _controller.DeleteNotes(null, null, true);
        //    var OkObj = result as ;
        //    Assert.Equal(1, OkObj.Count);
        //}
        [Fact]
        public async void Posttest()
        {
            var _controller = GetController();
            var notes2 = new Todo()
            {
               id=3,
                pinned = true,
                heading = "header3",
                text = "write text here",
                label = new List<labels>
                    {
                        new labels
                        {
                                labelname="label3"
                        }
                    },
                checklist = new List<checklist>
                    {
                        new checklist
                        {
                            checkname="checklist3"
                        }
                    }
            };
            var result =  _controller.PostTodo(notes2).Result as CreatedAtActionResult;
            

            var value = result.Value as Todo;
            Console.WriteLine(value.id);

            Assert.Equal("header3", value.heading);
        }
        //[Fact]
        //public async void Puttest()
        //{

        //    var notes1 = new Todo
        //    {
        //        id = 2,
        //        pinned = true,
        //        heading = "headerchanged",
        //        text = "write text here",
        //        label = new List<labels>
        //            {
        //                new labels
        //                {
        //                        labelname="label2"
        //                }
        //            },
        //        checklist = new List<checklist>
        //            {
        //                new checklist
        //                {
        //                    checkname="checklist1"
        //                }
        //            }
        //    };

        // var result = await _controller.PutTodo(2, notes1);
        //    var OkObj = result as OkObjectResult;
        //    //var okResponse = await notesController.PutNote(3, notePut) as OkObjectResult;
        //    //var result = okResponse.Value as Note;

        //    var value = OkObj.Value as Todo;
        //    //var OkObj = result.Value as Todo;
        //    // var value = OkObj.Value as Todo;

        //    // Console.WriteLine(value.id);

        //    Assert.Equal("headerchanged", value.heading);



        //    //var result = await _controller.GetNotes(null, null, true);
        //    //var OkObj = result as OkObjectResult;
        //    //var value = OkObj.Value as List<Todo>;
        //    //Assert.Equal(2, value.Count);
        //}
        [Fact]
        public async void Test_Delete()
        {
            var _controller = GetController();
            var result = await _controller.DeleteNotes("header1", null, null) as OkResult;
           // var okResponse = await _controller.PutNote(1, notePut) as OkObjectResult;
            var value = result.StatusCode;
            Assert.Equal(200,value);
        }

        [Fact]
        public async void Test_Delete1()
        {
            var _controller = GetController();
            var result = await _controller.DeleteNotes("header1", null, null) as OkResult;
            // var okResponse = await _controller.PutNote(1, notePut) as OkObjectResult;
            var value = result.StatusCode;
            Assert.Equal(200, value);
        }

        [Fact]
        public async void Test_Put()
        {
            var _controller = GetController();
            var notePut = new Todo
            {
                id=1,
                pinned = true,
                heading = "header1changed",
                text = "write text here",
                label = new List<labels>
                    {
                        new labels
                        {
                                labelname="label1"
                        }
                    },
                checklist = new List<checklist>
                    {
                        new checklist
                        {
                            checkname="checklist1"
                        }
                    }

            };
            var okResponse = await _controller.PutNote(1,notePut) as OkObjectResult;
            var result = okResponse.Value as Todo;
            Assert.Equal("header1changed", result.heading);

        }
        

    }
}
