import { Component, inject, OnInit } from '@angular/core';
import { PreguntasServices } from '../services/preguntas.services';
import { IPregunta } from '../interfaces/ipregunta';

@Component({
  selector: 'app-preguntas',
  imports: [],
  templateUrl: './preguntas.html',
  styleUrl: './preguntas.css',
})
export class Preguntas implements OnInit {
  lista:IPregunta[] = []
  private readonly preguntasServicio = inject(PreguntasServices)

  ngOnInit(): void {
    this.preguntasServicio.todos().subscribe(
      {
        next: (preguntas)=>{
          this.lista = preguntas
          console.log(this.lista)
        },
        error: (er)=>{
          console.log("No se pudo cargar las preguntas", er)
        }
      }
    )
  }
}
