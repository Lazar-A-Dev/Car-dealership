import { Automobil } from "./Automobil.js";
import { Boja } from "./Boja.js";
import { Marka } from "./Marka.js";
import { Model } from "./Model.js";
import { Prodavnica } from "./Prodavnica.js";

 
var response = await fetch("http://localhost:5206/Prodavnica/VratiSveProdavnice");
var data = await response.json();

data.forEach(async p => {

    var prodavnica = new Prodavnica(p["id"]);
    var response = await fetch("http://localhost:5206/Filter/VratiSveBoje");

        var data = await response.json();
        data.forEach(b =>{

           console.log(b);
            var boja =new Boja(b["id"],b["nazivBoje"]);
            prodavnica.dodajBoju(boja);
        })

 
        var response = await fetch("http://localhost:5206/Filter/VratiSveModele");
        var data = await response.json();
        data.forEach(mo => {

            var model =new Model(mo["id"],mo["nazivModela"]);
            prodavnica.dodajModel(model);

        })

        var response = await fetch("http://localhost:5206/Filter/VratiSveMarke");
        var data = await response.json();
        data.forEach(m => {
            var marka =new Marka(m["id"],m["nazivMarke"]);
            prodavnica.dodajMarku(marka);
        })
    prodavnica.crtaj(document.body);

});
