using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [Route("[controller]")]
    public class FilterController : ControllerBase
    {
        public Context _context;

        public FilterController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveMarke")]
        [HttpGet]
        public async Task<ActionResult> VratiSveMarke()
        {
            try
            {
                var marka = await _context.Marke.ToListAsync();
                return Ok(marka);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSveModele")]
        [HttpGet]
        public async Task<ActionResult> VratiSveModele()
        {
            try
            {
                var model = await _context.Modeli.ToListAsync();
                return Ok(model);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiSveBoje")]
        [HttpGet]
        public async Task<ActionResult> VratiSveBoje()
        {
            try
            {
                var boja = await _context.Boje.ToListAsync();
                return Ok(boja);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}