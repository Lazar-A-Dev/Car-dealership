using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Models
{
    [Route("[controller]")]
    public class ProdavnicaController : ControllerBase
    {
        public Context _context;

        public ProdavnicaController(Context context)
        {
            _context = context;
        }

        [Route("VratiSveProdavnice")]
        [HttpGet]
        public async Task<ActionResult> VratiSveProdavnice()
        {
            try
            {
                var prodavnica = await _context.Prodavnice.ToListAsync();
                return Ok(prodavnica);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NadjiAutomobileFront")]
        [HttpPost]
        public async Task<ActionResult> NadjiAutomobileFront([FromBody] FilterDTO filter)
        {
            try
            {
                var marka = await _context.Marke!.FindAsync(filter.marka);

                if (marka == null)
                {
                    return BadRequest("Morate selektovati marku");
                }

                var model = await _context.Modeli!.FindAsync(filter.model);
                var boja = await _context.Boje!.FindAsync(filter.boja);

                if (marka != null && model == null && boja == null)
                {
                    var filteredAutos = await _context.Automobili!.Include(a => a.Model).Include(a => a.Boja).Include(a => a.Marka).Where(a => a.Marka == marka).ToListAsync();
                    return Ok(filteredAutos);
                }


                if (marka != null && model != null && boja != null)
                {
                    var filteredAutos = await _context.Automobili!.Include(a => a.Model).Include(a => a.Boja).Include(a => a.Marka).Where(a => a.Marka == marka && a.Boja == boja && a.Model == a.Model).ToListAsync();
                    return Ok(filteredAutos);
                }


                if (marka != null && model != null && boja == null)
                {
                    var filteredAutos = await _context.Automobili!.Include(a => a.Model).Include(a => a.Boja).Include(a => a.Marka).Where(a => a.Marka == marka && a.Model == model).ToListAsync();
                    return Ok(filteredAutos);
                }


                if (marka != null && model == null && boja != null)
                {
                    var filteredAutos = await _context.Automobili!.Include(a => a.Model).Include(a => a.Boja).Include(a => a.Marka).Where(a => a.Marka == marka && a.Boja == boja).ToListAsync();
                    return Ok(filteredAutos);
                }

                return BadRequest("Auto nije pronadjen");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NaruciAuto/{idAuto}")]
        [HttpPut]
        public async Task<ActionResult> NaruciAuto(int idAuto)
        {
            try
            {
                var auto = await _context.Automobili.FindAsync(idAuto);
                if(auto == null)
                return BadRequest("Auto sa ovim Id-jem ne postoji");

                if(auto.Kolicina == 0)
                    return BadRequest("Auto nije na stanju");

                auto.Kolicina-=1;
                auto.PoslednjaProdaja = DateTime.Now;
                await _context.SaveChangesAsync();
                return Ok(auto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("NapraviProdavnicu")]
        [HttpPost]
        public async Task<ActionResult> NapraviProdavnicu(Prodavnica pro)
        {
            try
            {
                _context.Prodavnice.Add(pro);
                await _context.SaveChangesAsync();
                return Ok("Uspesno napravljena prodavnica");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajAutoProdavnici")]
        [HttpPut]
        public async Task<ActionResult> DodajAutoProdavnici(int idPro, int idAuto)
        {
            try
            {
                var prodavnica = await _context.Prodavnice.Include(a => a.Automobili).FirstOrDefaultAsync(p => p.ID == idPro);
                if (prodavnica == null)
                    return BadRequest("Prodavnica sa ovim Id-jem ne postoji");

                var auto = await _context.Automobili.FindAsync(idAuto);
                if (auto == null)
                    return BadRequest("Automobil sa ovim Id-jem ne postoji");

                prodavnica.Automobili.Add(auto);
                await _context.SaveChangesAsync();
                return Ok("Uspesno dodatk auto prodavnici");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}