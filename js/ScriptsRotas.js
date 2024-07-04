


function moveltodos() {

    var sel = document.getElementById("cmbUsuarios");
    var txt = document.getElementById("txtItemsRelacionados");
    var selu = document.getElementById("cmbRelacionados");

    txt.value = '';

    for (i = 0; i <= sel.options.length - 1; i++) {

        var opt = document.createElement('option');
        opt.text = sel.options[i].text;
        opt.value = sel.options[i].value;
        opt.selected = true;
        opt.innerHTML = sel.options[i].text;
        selu.appendChild(opt);


    }

    document.getElementById("cmbUsuarios").innerHTML = ""

    let element1 = document.getElementById("cmbRelacionados");
    element1.dispatchEvent(new Event("change"));

    let element2 = document.getElementById("cmbUsuarios");
    element2.dispatchEvent(new Event("change"));

    VarreItemsUsuarios();
    VarreItemsRelacionados();
    return false;
}

function moveltodos2() {

    var sel = document.getElementById("cmbRelacionados");
    var txt = document.getElementById("txtItemsUsuarios");
    var selu = document.getElementById("cmbUsuarios");

    txt.value = '';

    for (i = 0; i <= sel.options.length - 1; i++) {


        var opt = document.createElement('option');
        opt.text = sel.options[i].text
        opt.value = sel.options[i].value;
        opt.selected = true;
        opt.innerHTML = sel.options[i].text;
        selu.appendChild(opt);


    }

    document.getElementById("cmbRelacionados").innerHTML = ""

    let element1 = document.getElementById("cmbRelacionados");
    element1.dispatchEvent(new Event("change"));

    let element2 = document.getElementById("cmbUsuarios");
    element2.dispatchEvent(new Event("change"));

    VarreItemsUsuarios();
    VarreItemsRelacionados();
    return false;

}

function move1() {

    var sel = document.getElementById("cmbUsuarios");
    var txt = document.getElementById("txtItemsRelacionados");
    var selu = document.getElementById("cmbRelacionados");
    var ind = [];

    txt.value = '';

    for (i = 0; i <= sel.options.length - 1; i++) {

        if (sel.options[i].selected == true) {

            ind.push(sel.options[i].value);
            var opt = document.createElement('option');
            opt.value = sel.options[i].value;
            opt.text = sel.options[i].text;
            opt.selected = true;
            selu.appendChild(opt);
        }
    }


    for (x = 0; x <= ind.length - 1; x++) {


        for (i = 0; i <= sel.options.length - 1; i++) {

            if (sel.options[i].value == ind[x]) {
                document.getElementById("cmbUsuarios").remove(i);
            }
        }
    }


    let element1 = document.getElementById("cmbRelacionados");
    element1.dispatchEvent(new Event("change"));

    let element2 = document.getElementById("cmbUsuarios");
    element2.dispatchEvent(new Event("change"));

    VarreItemsUsuarios();
    VarreItemsRelacionados();
    return false;
}

function move2() {

    var sel = document.getElementById("cmbRelacionados");
    var txt = document.getElementById("txtItemsUsuarios");
    var selu = document.getElementById("cmbUsuarios");
    var ind = [];

    txt.value = '';

    for (i = 0; i <= sel.options.length - 1; i++) {

        if (sel.options[i].selected == true) {

            ind.push(sel.options[i].value);
            var opt = document.createElement('option');
            opt.value = sel.options[i].value;
            opt.text = sel.options[i].text;
            opt.selected = true;
            selu.appendChild(opt);

        }
    }

    for (x = 0; x <= ind.length - 1; x++) {
        for (i = 0; i <= sel.options.length - 1; i++) {
            if (sel.options[i].value == ind[x]) {
                document.getElementById("cmbRelacionados").remove(i);
            }
        }
    }

    let element1 = document.getElementById("cmbRelacionados");
    element1.dispatchEvent(new Event("change"));

    let element2 = document.getElementById("cmbUsuarios");
    element2.dispatchEvent(new Event("change"));

    VarreItemsUsuarios();
    VarreItemsRelacionados();
    return false;
}

function VarreItemsRelacionados() {
    var txt = document.getElementById("txtItemsRelacionados");
    var sel = document.getElementById("cmbRelacionados");
    txt.value = '';
    for (i = 0; i <= sel.options.length - 1; i++) {
        txt.value = txt.value + sel.options[i].value + ';';
    }
    return false;
}

function VarreItemsUsuarios() {
    var txt = document.getElementById("txtItemsUsuarios");
    var sel = document.getElementById("cmbUsuarios");
    txt.value = '';
    for (i = 0; i <= sel.options.length - 1; i++) {
        txt.value = txt.value + sel.options[i].value + ';';
    }
    return false;
}