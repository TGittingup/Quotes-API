using Microsoft.AspNetCore.Mvc;
using QuotesAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuotesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {
        private static List<Quote> Quotes = new List<Quote>
        {
            new Quote { Id = 1, Author = "Albert Einstein", Text = "Life is like riding a bicycle. To keep your balance, you must keep moving." },
            new Quote { Id = 2, Author = "Oscar Wilde", Text = "Be yourself; everyone else is already taken." }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Quote>> GetAllQuotes()
        {
            return Ok(Quotes);
        }

        [HttpPost]
        public ActionResult<Quote> AddQuote([FromBody] Quote newQuote)
        {
            newQuote.Id = Quotes.Max(q => q.Id) + 1;
            Quotes.Add(newQuote);
            return CreatedAtAction(nameof(GetAllQuotes), new { id = newQuote.Id }, newQuote);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var quote = Quotes.FirstOrDefault(q => q.Id == id);
            if (quote == null)
                return NotFound();

            Quotes.Remove(quote);
            return NoContent();
        }
    }
}
