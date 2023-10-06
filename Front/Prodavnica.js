import { Automobil } from "./Automobil.js";

export class Prodavnica {
    constructor(id) {
        this.id = id;

        this.listaAutomobila = [];
        this.listaMarke = [];
        this.listaModela = [];
        this.listaBoja = [];
        this.container = null;
    }

    dodajAuto(auto) {
        this.listaAutomobila.push(auto);
    }

    dodajBoju(boja) {
        this.listaBoja.push(boja);
    }

    dodajMarku(marka) {
        this.listaMarke.push(marka);
    }

    dodajModel(model) {
        this.listaModela.push(model);
    }

    crtaj(host) {

        this.container = document.createElement("div");
        this.container.className = "glavni";
        host.appendChild(this.container);

        var levi = document.createElement("div");
        levi.className = "levi";
        this.container.appendChild(levi);

        var red1 = document.createElement("div");
        red1.className = "red1";
        levi.appendChild(red1);

        var marka = document.createElement("label");
        marka.className = "marka";
        marka.innerHTML = "Marka:"
        red1.appendChild(marka);

        var mDiv = document.createElement("div");
        mDiv.className = "mDiv";
        red1.appendChild(mDiv);

        var selM = document.createElement("select");
        selM.className = "selM";
        selM.id = this.id;
        mDiv.appendChild(selM);

        this.listaMarke.forEach((marka, index) => {

            console.log(marka);
            const opcija = document.createElement("option");
            opcija.innerHTML = marka.nazivM;
            opcija.value = marka.id;
            if (index != null) {

                opcija.selected = true;

            }

            selM.appendChild(opcija);

        })

        var red2 = document.createElement("div");
        red2.className = "red2";
        levi.appendChild(red2);

        var model = document.createElement("label");
        model.className = "model";
        model.innerHTML = "Model:"
        red2.appendChild(model);

        var moDiv = document.createElement("div");
        moDiv.className = "moDiv";
        red2.appendChild(moDiv);

        var selMo = document.createElement("select");
        selMo.className = "selMo";
        selMo.id = this.id;
        moDiv.appendChild(selMo);

        this.listaModela.forEach((model, index) => {
            console.log(model);
            const opcija = document.createElement("option");
            opcija.innerHTML = model.nazivMo;
            opcija.value = model.id;

            //if (index != null) {

            //opcija.selected = true;

            //}
            selMo.appendChild(opcija);
        })

        //Dodavanje praznog elementa
        var emptyOption = document.createElement("option");
        emptyOption.value = "";
        emptyOption.selected = true;
        emptyOption.text = "Izaberite model";
        selMo.appendChild(emptyOption);

        var red3 = document.createElement("div");
        red3.className = "red3";
        levi.appendChild(red3);

        var boja = document.createElement("label");
        boja.className = "boja";
        boja.innerHTML = "Boja:"
        red3.appendChild(boja);

        var bDiv = document.createElement("div");
        bDiv.className = "bDiv";
        red3.appendChild(bDiv);

        var selB = document.createElement("select")
        selB.className = "selB";
        selB.id = this.id;
        bDiv.appendChild(selB);

        this.listaBoja.forEach((boja, index) => {
            console.log(boja);

            const opcija = document.createElement("option");

            opcija.innerHTML = boja.nazivB;

            opcija.value = boja.id;

            //if (index != null) {

            //opcija.selected = true;
            //}
            selB.appendChild(opcija);
        })

        //Dodavanje praznog elementa
        var emptyOption = document.createElement("option");
        emptyOption.value = "";
        emptyOption.selected = true;
        emptyOption.text = "Izaberite boju";
        selB.appendChild(emptyOption);

        var searchButton = document.createElement("button");
        searchButton.className = "searchButton";
        searchButton.innerHTML = "Pronadji";
        levi.appendChild(searchButton);

        var desni = document.createElement("div");
        desni.className = "desni";
        this.container.appendChild(desni);

        searchButton.onclick = async e => {
            var queryM = parseInt(this.container.querySelector(".selM").value);
            var queryMo = parseInt(this.container.querySelector(".selMo").value);
            var queryB = parseInt(this.container.querySelector(".selB").value);


            var queryParams;

            if (isNaN(queryB) && isNaN(queryMo)) {
                queryParams = {
                    marka: queryM
                };

            }
            else if (isNaN(queryMo)) {
                queryParams = {
                    marka: queryM,
                    boja: queryB
                };
            }
            else if (isNaN(queryB)) {
                queryParams = {
                    marka: queryM,
                    model: queryMo
                };
            }
            else {
                queryParams = {
                    marka: queryM,
                    model: queryMo,
                    boja: queryB
                }
            }


            var response = await fetch("http://localhost:5206/Prodavnica/NadjiAutomobileFront", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(queryParams)
            });

            var data = await response.json();

            while (desni.firstChild) {
                desni.removeChild(desni.lastChild);
            }

            data.forEach(p => {
                var autoId = p["id"];
                var existingAuto = this.listaAutomobila.find(auto => auto.id === autoId);
                console.log(p);
                if (existingAuto == null) {
                    var auto = new Automobil(p["id"], p["cena"], p["poslednjaProdaja"], p["kolicina"], p["slika"], p["marka"], p["model"], p["boja"]);
                    this.listaAutomobila.push(auto);
                    this.listaAutomobila.forEach(auto => {
                        auto.crtajAuto(desni);

                    })
                    this.listaAutomobila.splice(0);
                }

            });
        };




    }
}

