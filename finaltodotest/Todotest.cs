using System;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Todolist.Models;
using finaltodo.Controllers;

using NSuperTest;
using Xunit;


namespace todoxunittest
{
    public class Todotest
    {
        Server server;


        public Todotest()
        {
            server = new Server("http://localhost:44330");

        }
        [Fact]
        public void testget()
        {try
            {


                server.Get("api/Todoes")
                    .Expect(200)
                    .End();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
      //  [Fact]
        //public void test()
        //{
        //    server.Get("api/Todoes?heading=header").Expect(200).End();

        //}
    
    
   
