using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CopyChallenge.Models;

namespace CopyChallenge.Controllers
{
    [Route("api/[controller]/[action]")]
    public class PlayersController : Controller
    {
        private MyDbContext _context;

        public PlayersController(MyDbContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var players = _context.Players.Select(i => new {
                i.ID,
                i.FirstName,
                i.LastName,
                i.Week1,
                i.Week2,
                i.Week3,
                i.Week4,
                i.Week5
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "ID" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(players, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Player();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Players.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.ID });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Players.FirstOrDefaultAsync(item => item.ID == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Players.FirstOrDefaultAsync(item => item.ID == key);

            _context.Players.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Player model, IDictionary values) {
            string ID = nameof(Player.ID);
            string FIRST_NAME = nameof(Player.FirstName);
            string LAST_NAME = nameof(Player.LastName);
            string WEEK1 = nameof(Player.Week1);
            string WEEK2 = nameof(Player.Week2);
            string WEEK3 = nameof(Player.Week3);
            string WEEK4 = nameof(Player.Week4);
            string WEEK5 = nameof(Player.Week5);

            if(values.Contains(ID)) {
                model.ID = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(FIRST_NAME)) {
                model.FirstName = Convert.ToString(values[FIRST_NAME]);
            }

            if(values.Contains(LAST_NAME)) {
                model.LastName = Convert.ToString(values[LAST_NAME]);
            }

            if(values.Contains(WEEK1)) {
                model.Week1 = Convert.ToDecimal(values[WEEK1], CultureInfo.InvariantCulture);
            }

            if(values.Contains(WEEK2)) {
                model.Week2 = Convert.ToDecimal(values[WEEK2], CultureInfo.InvariantCulture);
            }

            if(values.Contains(WEEK3)) {
                model.Week3 = Convert.ToDecimal(values[WEEK3], CultureInfo.InvariantCulture);
            }

            if(values.Contains(WEEK4)) {
                model.Week4 = Convert.ToDecimal(values[WEEK4], CultureInfo.InvariantCulture);
            }

            if(values.Contains(WEEK5)) {
                model.Week5 = Convert.ToDecimal(values[WEEK5], CultureInfo.InvariantCulture);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}