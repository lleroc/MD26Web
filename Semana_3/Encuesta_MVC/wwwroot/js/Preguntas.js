class Preguntas_Clase {
    constructor(url) {
        this.contenedorPreguntas = document.getElementById("contenedorPreguntas")
        this.botonBoton = document.getElementById("btnGuardar")
        this.url = url
    }

    async lista_preguntas() {
        try {
            const respuesta = await fetch("/Respuestas/Lista_preguntas")
            const lista_pre = respuesta.json()

            if (lista_pre.ok) {
                //llamarr a otro metodo para mostrar
            } else {
                this.contenedorPreguntas.innerHTML = "No se pudieron mostrar las preguntas"
            }
        } catch (e) {
            console.log("Error al listar las prguntas", e)
            this.contenedorPreguntas.innerHTML = "Error al mostrar las preguntas" + e
        }
    }

    mastrarPrguntas(preguntas) {
        preguntas.forEach((pregunta, index) => {

            console.log(pregunta);

            const preguntaId = pregunta.prreguntasId;
            const enunciado = pregunta.enunciado;
            const descripcion = pregunta.descripcion;

            const div = document.createElement("div");

            div.className = "mb-4";

            div.innerHTML =
                "<label class='form-label'>" +
                "<strong>" + (index + 1) + ". " + enunciado + "</strong>" +
                "</label>" +

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

            this.contenedorPreguntas.appendChild(div);
        });
    }
}