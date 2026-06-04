let listaPreguntas = [];

async function cargaPreguntas() {
    
    try {
        const respuesta = await fetch("/Respuestas/Lista_preguntas", {
            method: "GET"
        });
        const resultado = await respuesta.json();
        if (!resultado.ok) {
            alert("Ocurrio un error al guardar")
            return;
        }
        listaPreguntas = resultado.data.listaPreguntas;
        console.log(listaPreguntas)
        mostrarPreguntas(listaPreguntas);

    
    } catch (error) {

    }
}

function mostrarPreguntas(listaPreguntas) {

    const contenedorPreguntas = document.getElementById("contenedorPreguntas");

    contenedorPreguntas.innerHTML = "";

    if (!listaPreguntas || listaPreguntas.length === 0) {
        contenedorPreguntas.innerHTML = "<p>No existen preguntas registradas</p>";
        return;
    }

    console.log(listaPreguntas.length);

    listaPreguntas.forEach((pregunta, index) => {

        console.log(pregunta);

        const preguntaId = pregunta.prreguntasId;
        const enunciado = pregunta.enunciado;
        const descripcion = pregunta.descripcion;
        const div = document.createElement("div");

        div.className = "mb-4";

        div.innerHTML =
            "<label class='form-label'>" +
            "<strong>" + (index + 1) + ". " + escapeHtml(enunciado) + "</strong>" +
            "</label>" +

            (descripcion
                ? "<p class='text-muted'>" + escapeHtml(descripcion) + "</p>"
                : "") +

            "<input type='text' " +
            "class='form-control respuesta' " +
            "data-id='" + preguntaId + "' " +
            "data-enunciado='" + escapeHtml(enunciado) + "' " +
            "data-descripcion='" + escapeHtml(descripcion ?? "") + "' " +
            "placeholder='Escriba su respuesta' />";

        console.log(div);

        contenedorPreguntas.appendChild(div);
    });
}