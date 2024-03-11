
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarvedRock.Admin.Controllers
{
    public class QueController : Controller
    {
        public IActionResult List()
        {
            ViewData["Title"] = "Page Title!";
            ViewBag.Msg = "Select rows to post";
            return View(GetQueList());
        }

        [Route("Que/SubmitDetails")]
        [HttpPost]
        public void Details([FromBody] List<QueItem> items)
        {
            if(items?.Count > 0)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(items);
                Debug.WriteLine("Details:");
                Debug.WriteLine(json);
                Debug.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            }
            else
            {
                ViewBag.Msg = "VB Que msg! No items!";
            }
        }

        public List<QueItem> GetQueList()
        {
            return new List<QueItem>
            {
                new QueItem
                {
                    Id = 1,
                    Name = "Name1",
                    Desc = "Desc1"
                },
                new QueItem
                {
                    Id = 2,
                    Name = "Name2",
                    Desc = "Desc2"
                },
                new QueItem
                {
                    Id = 3,
                    Name = "Name3",
                    Desc = "Desc3"
                },
            };
        }
    }

    public class QueItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
