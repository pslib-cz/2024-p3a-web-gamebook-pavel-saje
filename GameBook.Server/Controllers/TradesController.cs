using Microsoft.AspNetCore.Mvc;
using GameBook.Server.Data;
using GameBook.Server.Models;
using Microsoft.EntityFrameworkCore;
using GameBook.Server.Migrations;

namespace GameBook.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private AppDbContext _context;
        public TradesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<ViewTrades>> Get()
        {
            var trades = await _context.Trades
                .Include(ts => ts.trade)
                .ThenInclude(t => t.Item1)
                .Include(ts => ts.trade)
                .ThenInclude(t => t.Item2)
                .Include(ts => ts.interactible)
                .Select(ts => new ViewTrades
                {
                    TradesID = ts.trade.TradeID,
                    interactible = new ViewInteractible
                    {
                        InteractibleID = ts.interactibleID,
                        Name = ts.interactible.Name,
                    },
                    trade = new ViewTrade
                    {
                        TradeID = ts.trade.TradeID,
                        Item1 = new ViewItem
                        {
                            ItemID = ts.trade.Item1.ItemID,
                            Name = ts.trade.Item1.Name,
                            TradeValue = ts.trade.Item1.TradeValue,
                        },
                        Item2 = new ViewItem
                        {
                            ItemID = ts.trade.Item2.ItemID,
                            Name = ts.trade.Item2.Name,
                            TradeValue = ts.trade.Item2.TradeValue,
                        }
                    }
                }).ToListAsync();
            return Ok(trades);
        }

        [HttpGet("ByInteractibleId/{id}")]
        public async Task<ActionResult<ViewTrades>> Get(int id)
        {
            var trades = await _context.Trades
                .Include(ts => ts.trade)
                .ThenInclude(t => t.Item1)
                .Include(ts => ts.trade)
                .ThenInclude(t => t.Item2)
                .Include(ts => ts.interactible)
                .Where(ts => ts.interactibleID == id)
                .Select(ts => new ViewTrades
                {
                    TradesID = ts.trade.TradeID,
                    interactible = new ViewInteractible
                    {
                        InteractibleID = ts.interactibleID,
                        Name = ts.interactible.Name,
                        ImagePath = ts.interactible.ImagePath
                    },
                    trade = new ViewTrade
                    {
                        TradeID = ts.trade.TradeID,
                        Item1 = new ViewItem
                        {
                            ItemID = ts.trade.Item1.ItemID,
                            Name = ts.trade.Item1.Name,
                            TradeValue = ts.trade.Item1.TradeValue,
                        },
                        Item2 = new ViewItem
                        {
                            ItemID = ts.trade.Item2.ItemID,
                            Name = ts.trade.Item2.Name,
                            TradeValue = ts.trade.Item2.TradeValue,
                        }
                    }
                }).ToListAsync();
            var buys = await _context.Buy
                .Include(b => b.Item)
                .Include(b => b.Interactible)
                .Where(b => b.InteractibleID == id)
                .Select(b => new ViewBuy
                {
                    BuyID = b.BuyID,
                    interactibleID = b.InteractibleID,
                    itemID = b.ItemID,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = b.InteractibleID,
                        Name = b.Interactible.Name,
                        ImagePath = b.Interactible.ImagePath
                        //ImageBase64 = b.Interactible.ImageBase64,
                    },
                    Item = new ViewItem
                    {
                        ItemID = b.Item.ItemID,
                        Name = b.Item.Name,
                        TradeValue = b.Item.TradeValue,
                    }
                }).ToListAsync();

            var tradesInteractible = await _context.TradesInteractibles
                .Include(ti => ti.TradeInteractible)
                .Include(ti => ti.Interactible)
                .Where(ti => ti.InteractibleID == id)
                .Select(ti => new ViewTradeInteractible
                {
                    TradeInteractibleID = ti.TradeInteractibleID,
                    InteractibleID = ti.TradeInteractible.InteractibleID,
                   
                    Text = ti.TradeInteractible.Text,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = ti.Interactible.InteractibleID,
                        Name = ti.Interactible.Name,
                        ImagePath = ti.Interactible.ImagePath
                    },
                    ItemID = ti.TradeInteractible.ItemID,
                    Item = new ViewItem
                    {
                        ItemID = ti.TradeInteractible.Item.ItemID,
                        Name = ti.TradeInteractible.Item.Name,
                        TradeValue = ti.TradeInteractible.Item.TradeValue
                    },
                    
                }).ToListAsync();

            var sells = await _context.Sell
                .Include(s => s.Item)
                .Include(s => s.Interactible)
                .Where(s => s.interactibleID == id)
                .Select(s => new ViewSell
                {
                    SellID = s.SellID,
                    interactibleID = s.interactibleID,
                    itemID = s.itemID,
                    Interactible = new ViewInteractible
                    {
                        InteractibleID = s.interactibleID,
                        Name = s.Interactible.Name,
                        ImagePath = s.Interactible.ImagePath
                    },
                    Item = new ViewItem
                    {
                        ItemID = s.Item.ItemID,
                        Name = s.Item.Name,
                        TradeValue = s.Item.TradeValue,
                    }
                }).ToListAsync();

            var shops = new shops
            {
                
                trades = trades,
                buys = buys,
                tradeInteractibles = tradesInteractible,
                sells = sells
            };

            return Ok(shops);
        }

        [NonAction][HttpPost("Trade")]
        public async Task<ActionResult<ViewTrade>> Post(InputTrade inputTrade)
        {
            var dataTrade = new DataTrade
            {
                Item1ID = inputTrade.item1ID,
                Item2ID = inputTrade.item2ID,
            };
            _context.Trade.Add(dataTrade);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = dataTrade.TradeID }, inputTrade);
        }

        [NonAction][HttpPost("Trades")]
        public async Task<ActionResult<ViewTrades>> Post(InputTrades inputTrades)
        {
            var dataTrades = new DataTrades
            {
                tradeID = inputTrades.tradeID,
                interactibleID = inputTrades.interactibleID,
            };
            _context.Trades.Add(dataTrades);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = dataTrades.TradesID }, inputTrades);
        }

        [NonAction][HttpPost("TradeInteractible")]
        public async Task<ActionResult<ViewTradeInteractible>> Post(InputTradeInteractible inputTradeInteractible)
        {
            var dataTradeInteractible = new DataTradeInteractible
            {
                InteractibleID = inputTradeInteractible.interactibleID,
                ItemID = inputTradeInteractible.itemID,
                TradeValue = inputTradeInteractible.tradeValue,
                Text = inputTradeInteractible.text
            };
            _context.TradeInteractibles.Add(dataTradeInteractible);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = dataTradeInteractible.TradeInteractibleID }, inputTradeInteractible);
        }

        [NonAction][HttpPost("TradesInteractible")]
        public async Task<ActionResult<ViewTradeInteractible>> Post(InputTradesInteractible inputTradesInteractibles)
        {
            var dataTradesInteractibles = new DataTradesInteractible
            {
                TradeInteractibleID = inputTradesInteractibles.tradeInteractibleID,
                InteractibleID = inputTradesInteractibles.interactibleID,
            };
            _context.TradesInteractibles.Add(dataTradesInteractibles);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = dataTradesInteractibles.TradesInteractibleID }, inputTradesInteractibles);
        }

        [NonAction][HttpDelete("Trade/{id}")]
        public async Task<ActionResult<ViewTrade>> DeleteTrade(int id)
        {
            var trade = await _context.Trade.FindAsync(id);
            if (trade == null)
            {
                return NotFound();
            }
            _context.Trade.Remove(trade);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [NonAction][HttpDelete("Trades/{id}")]
        public async Task<ActionResult<ViewTrades>> DeleteTrades(int id)
        {
            var trades = await _context.Trades.FindAsync(id);
            if (trades == null)
            {
                return NotFound();
            }
            _context.Trades.Remove(trades);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [NonAction][HttpDelete("TradeInteractible/{id}")]
        public async Task<ActionResult<ViewTradeInteractible>> DeleteTradeInteractible(int id)
        {
            var tradeInteractible = await _context.TradeInteractibles.FindAsync(id);
            if (tradeInteractible == null)
            {
                return NotFound();
            }
            _context.TradeInteractibles.Remove(tradeInteractible);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [NonAction][HttpDelete("TradesInteractible/{id}")]
        public async Task<ActionResult<ViewTradeInteractible>> DeleteTradesInteractible(int id)
        {
            var tradesInteractible = await _context.TradesInteractibles.FindAsync(id);
            if (tradesInteractible == null)
            {
                return NotFound();
            }
            _context.TradesInteractibles.Remove(tradesInteractible);
            await _context.SaveChangesAsync();
            return Ok();
        }


        public class shops
        {
            public List<ViewTrades> trades { get; set; }
            public List<ViewBuy> buys { get; set; }
            public List<ViewTradeInteractible> tradeInteractibles { get; set; }
            public List<ViewSell> sells { get; set; }
        }


    }
}
