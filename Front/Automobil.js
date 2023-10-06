export class Automobil {
    constructor(id, cena, datumPro, kolicina, slika, marka, model, boja) {

        this.id = id;
        this.cena = cena;
        this.datumPro = datumPro;
        this.kolicina = kolicina;
        this.slika = slika;
        this.marka = marka;
        this.model = model;
        this.boja = boja;

        this.container = null;

    }

    crtajAuto(host) {
        this.container = host;
        var kartica = document.createElement("div");
        kartica.className = "kartica";
        host.appendChild(kartica);

        var redK1 = document.createElement("div");
        redK1.className = "redK1";
        kartica.appendChild(redK1);

        var markaK = document.createElement("label");
        markaK.className = "markaK";
        markaK.innerHTML = "Marka: " + this.marka.nazivMarke;
        redK1.appendChild(markaK);


        var redK2 = document.createElement("div");
        redK2.className = "redK2";
        kartica.appendChild(redK2);

        var modelK = document.createElement("label");
        modelK.className = "modelK";
        modelK.innerHTML = "Model: " + this.model.nazivModela;
        redK2.appendChild(modelK);


        var redK3 = document.createElement("div");
        redK3.className = "redK3";
        kartica.appendChild(redK3);

        var slika = document.createElement("img");
        slika.className = "slika";
        slika.src = `www.root/${this.slika}`
        slika.alt = `www.root/${this.slika}`
        redK3.appendChild(slika);


        var redK4 = document.createElement("div");
        redK4.className = "redK4";
        kartica.appendChild(redK4);

        var kolicina = document.createElement("label");
        kolicina.className = "kolicina";
        kolicina.innerHTML = "Kolicina: " + this.kolicina;
        redK3.appendChild(kolicina);


        var redK5 = document.createElement("div");
        redK5.className = "redK5";
        kartica.appendChild(redK5);

        var datumProdaje = document.createElement("label");
        datumProdaje.className = "poslednjaProdaja";
        datumProdaje.innerHTML = "Poslednja prodaja: " + new Date(this.datumPro).toLocaleDateString();
        redK5.appendChild(datumProdaje);


        var redK6 = document.createElement("div");
        redK6.className = "redK6";
        kartica.appendChild(redK6);

        var cena = document.createElement("label");
        cena.className = "cena";
        cena.innerHTML = "Cena: " + this.cena + "din";
        redK6.appendChild(cena);


        var redK7 = document.createElement("div");
        redK7.className = "redK7";
        kartica.appendChild(redK7);

        var findButton = document.createElement("button");
        findButton.className = "findButton";
        findButton.innerHTML = "Naruci";
        redK7.appendChild(findButton);

        findButton.onclick = async e => {

            var response = await fetch(`http://localhost:5206/Prodavnica/NaruciAuto/${this.id}`, {
                method: "PUT",
            });

            if (response.ok) {
                var data = await response.json();

                var kol = kartica.querySelector(".kolicina");
                kol.innerHTML = "";
                kol.innerHTML = "Kolicina: " + data.kolicina;

                var dat = kartica.querySelector(".poslednjaProdaja");
                dat.innerHTML = "";
                dat.innerHTML = "Poslednja prodaja: " + new Date(data.poslednjaProdaja).toLocaleDateString();
            } else {
                alert("Auto nije na stanju");
            }

        };

    }
}