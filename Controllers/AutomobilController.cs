using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [Route("[controller]")]
    public class AutomobilController : ControllerBase
    {
        public Context _context;

        public AutomobilController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveAutomobile")]
        [HttpGet]
        public async Task<ActionResult> VratiSveAutomobile()
        {
            try
            {
                var auto = await _context.Automobili.ToListAsync();
                return Ok(auto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviAuto")]
        [HttpPost]
        public async Task<ActionResult> NapraviAuto(Automobil auto)
        {
            try
            {
                _context.Automobili.Add(auto);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljen automobil");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajFilterAutu")]
        [HttpPut]
        public async Task<ActionResult> DodajFilterAutu(int idAuto, int idMarke, int idModela, int idBoje)
        {
            try
            {
                var auto = await _context.Automobili.FindAsync(idAuto);
                if(auto == null)
                    return BadRequest("Automobil sa ovim Id-jem ne postoji");

                var marka = await _context.Marke.Include(a => a.Automobili).FirstOrDefaultAsync(m => m.ID == idMarke);
                if(marka == null)
                    return BadRequest("Marka sa ovim Id-jem ne postoji");
                
                var model = await _context.Modeli.Include(a => a.Automobili).FirstOrDefaultAsync(m2 => m2.ID == idModela);
                if(model == null)
                    return BadRequest("Model sa ovim Id-jem ne postoji");
                
                var boja = await _context.Boje.Include(a => a.Automobili).FirstOrDefaultAsync(b => b.ID == idBoje);
                if(boja == null)
                    return BadRequest("Model sa ovim Id-jem ne postoji");

                marka.Automobili.Add(auto);
                model.Automobili.Add(auto);
                boja.Automobili.Add(auto);

                await _context.SaveChangesAsync();
                return Ok("Uspesno je automobil dodat filterima");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviMarku")]
        [HttpPost]
        public async Task<ActionResult> NapraviMarku(Marka marka)
        {
            try
            {
                _context.Marke.Add(marka);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena marka");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviModel")]
        [HttpPost]
        public async Task<ActionResult> NapraviModel(Model model)
        {
            try
            {
                _context.Modeli.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljen model");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviBoju")]
        [HttpPost]
        public async Task<ActionResult> NapraviBoju(Boja boja)
        {
            try
            {
                _context.Boje.Add(boja);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena boja");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}