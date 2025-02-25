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
                    InteractibleID = ti.InteractibleID,
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
                    }
                }).ToListAsync();


            var shops = new shops
            {
                
                trades = trades,
                buys = buys,
                tradeInteractibles = tradesInteractible
            };

            return Ok(shops);
        }

        public class shops
        {
            public List<ViewTrades> trades { get; set; }
            public List<ViewBuy> buys { get; set; }
            public List<ViewTradeInteractible> tradeInteractibles { get; set; }
        }


    }
}
