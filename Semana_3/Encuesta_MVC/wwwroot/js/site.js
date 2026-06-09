// $.ready(
//     () => {
//         cargaPreguntas()
//     }
// )


// var cargaPreguntas = () => {
//     var clasePreguntas = new Preguntas_Clase("")
//     clasePreguntas.lista_preguntas()
// }
let listaPreguntas = [];
document.addEventListener("DOMContentLoaded", function () {
    cargaPreguntas()

    document.getElementById("btnGuardar").addEventListener("click", function () {
        guardarRespuestas();
    })
});
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
            "<strong>" + (index + 1) + ". " + enunciado+ "</strong>" +
            "</label>" +
        "<input type='hidden' class='pregunta_id' value=" + preguntaId + " />"+
            (descripcion
                ? "<p class='text-muted'>" + descripcion + "</p>"
                : "") +
            "<input type='text' " +
            "class='form-control respuesta' " +
            "data-id='" + preguntaId + "' " +
            "data-enunciado='" + (enunciado) + "' " +
            "data-descripcion='" + (descripcion ?? "") + "' " +
            "placeholder='Escriba su respuesta' />";

        console.log(div);

        contenedorPreguntas.appendChild(div);
    });
}
async function guardarRespuestas() {
    const token = document.querySelector("input[name='__RequestVerificationToken']").value;

    const filas = document.querySelectorAll("#contenedorPreguntas .mb-4")

    const listaPreguntas = []

    filas.forEach(fila => {
        const prreguntasId = fila.querySelector(".pregunta_id").value;
        const respuesta = fila.querySelector(".respuesta").value;
        listaPreguntas.push({
            PrreguntasId: parseInt(prreguntasId),
            respuesta: respuesta
        });
    });

    const datos = {
        listaPreguntas: listaPreguntas
    }
    try {
        const respuesta = await fetch("/Respuestas/Guadar", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                //'RequestVerificationToken':token    
            },
            body: JSON.stringify(datos)
        })

        const resultado = respuesta.json()
        if (resultado.ok) {
            const mensaje = document.getElementById("mensaje")
            mensaje.className = "alert alert-success"
            mensaje.textContent = resultado.mensaje
            mensaje.classList.remove("d-none")
        } else {
            const mensaje = document.getElementById("mensaje")
            mensaje.className = "alert alert-danger"
            mensaje.textContent = resultado.mensaje
            mensaje.classList.remove("d-none")
        }

    } catch (e) {

    }


}
