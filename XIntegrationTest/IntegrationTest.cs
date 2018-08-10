using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net;
using finaltodo.Controllers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using finaltodo;
//using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Xunit;
using System.Threading.Tasks;
using Todolist.Models;
using finaltodo.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore.InMemory;
using FluentAssertions;

namespace XUnitTesttodo
{
    public class IntegrationTest
    {
        private HttpClient client;
        private finaltodoContext _context;
        public IntegrationTest()
        {
            //var host = new TestServer(new WebHostBuilder()
            //    .UseEnvironment("Testing")
            //    .UseStartup<Startup>());
            var host = new TestServer(new WebHostBuilder().UseEnvironment("Testing")
                .UseStartup<Startup>());
            _context = host.Host.Services.GetService(typeof(finaltodoContext)) as finaltodoContext;
            client = host.CreateClient();
            _context.Todo.Add(TestNote1);
            _context.Todo.Add(TestNote2);
           
            _context.SaveChanges();
        }
        Todo TestNote1 = new Todo()
        {
            heading = "header1",
            text = "text1",
            checklist = new List<checklist>()
                        {
                            new checklist(){checkname = "checklist1"},

                        },
            label = new List<labels>()
                        {
                            new labels(){labelname = "Label11"},
                            new labels(){labelname = "Label12"}
                        },
            pinned = true
        };
        Todo TestNote2 = new Todo()
        {
            heading = "header2",
            text = "text2",
            checklist = new List<checklist>()
                        {
                            new checklist(){checkname = "checklist2"},

                        },
            label = new List<labels>()
                        {
                            new labels(){labelname = "Label21"},
                            new labels(){labelname = "Label22"}
                        },
            pinned = true
        };
        [Fact]
        public async void GetAllIntegrationTest()
        {
            var response = await client.GetAsync("api/todoes");
            var responseresult = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Todo>>(responseresult);
               Assert.Equal(2, responsenote.Count);
            //Assert.Equal(response., "NotFound");
            //response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task Get()
        {
            // Act
            var response = await client.GetAsync("api/Todoes/2");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<Todo>(responseString);

            notes.id.Should().Be(2);
        }

        [Fact]
        public async void GetHeaderIntegrationTest()
        {
            var response = await client.GetAsync("api/todoes?title=header1");
            var responseresult = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<List<Todo>>(responseresult);
            foreach (var a in responsenote)
            {
                Assert.Equal("header1", a.heading);
            }
            //Assert.Equal(response., "NotFound");
            //response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async void DeleteIntegrationTest()
        {
            var response = await client.DeleteAsync("api/todoes/delete?title=header2");
            response.EnsureSuccessStatusCode();
            var responset = await client.GetAsync("api/Todoes/2");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, responset.StatusCode);
        }
        [Fact]
        public async void PutIntegrationTest()
        {
            var note = new Todo()
            {
                id = 1,
                heading = "header changed",
                pinned = true,
                label = new List<labels>()
                {
                    new labels()
                    {
                        labelname="label1changed"
                    }
                },
                checklist = new List<checklist>()
                {
                     new checklist()
                     {
                        checkname = "checklist1changed"
                     }
                }
            };
            var json = JsonConvert.SerializeObject(note);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PutAsync("api/Todoes/1", stringContent);
            var responseresult = await response.Content.ReadAsStringAsync();
            var responsenote = JsonConvert.DeserializeObject<Todo>(responseresult);
            Assert.Equal(responsenote.heading, "header changed");
            response.EnsureSuccessStatusCode();



            var tresponse = await client.GetAsync("api/todoes?id=1");
            var tresponseresult = await response.Content.ReadAsStringAsync();
            var tresponsenote = JsonConvert.DeserializeObject<Todo>(responseresult);
            Assert.Equal(tresponsenote.heading, "header changed");


        }
        [Fact]
        public async void PostIntegrationTest()
        {
            var note = new Todo()
            {

                heading = "header3",
                pinned = true,
                label = new List<labels>()
                {
                    new labels()
                    {
                        labelname="label3"
                    }
                },
                checklist = new List<checklist>()
                {
                     new checklist()
                     {
                        checkname = "checklist3"
                     }
                }
            };
            var json = JsonConvert.SerializeObject(note);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Todoes", stringContent);
            response.EnsureSuccessStatusCode();
            var responset = await client.GetAsync("api/todoes?label=label3");
            responset.EnsureSuccessStatusCode();

        }
    }

}
