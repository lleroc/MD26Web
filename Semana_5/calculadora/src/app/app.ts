import { ThisReceiver } from '@angular/compiler';
import { Component, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { RouterOutlet } from '@angular/router';
import { reportUnhandledError } from 'rxjs/internal/util/reportUnhandledError';
import { flattenDiagnosticMessageText } from 'typescript';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatButtonModule,MatCardModule,
    MatIconModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('calculadora');
  pantalla = "0";
  private valorAnterior:any = null;
  private operador: string | null = null;

  private auxiliarEsperaNuevoNuevo = false;



  limpiar(){
    this.pantalla = "0"
    this.valorAnterior = null
    this.operador = null
    this.auxiliarEsperaNuevoNuevo = true
  }
  borar_ultimo(){
    if(this.pantalla.length === 1 || this.pantalla === 'Error'){
      this.pantalla = "0"
    }else{
      this.pantalla = this.pantalla.slice(0,-1)
    }
  }
  presionaNumero(numero:any){
    if(this.auxiliarEsperaNuevoNuevo){
      this.pantalla = numero
      this.auxiliarEsperaNuevoNuevo = false
      return;
    }
    this.pantalla = this.pantalla === '0' ? numero : this.pantalla + numero
  }
  presionaOperador(operador:any){
    const valorActual = parseFloat(this.pantalla)
    if(this.valorAnterior === null){
      this.valorAnterior = valorActual
    }else if(this.operador){
      const resultado = this.calcular(this.valorAnterior, valorActual,this.operador)
      this.pantalla  = String(resultado)
      this.valorAnterior = resultado
    }
  }

  presionaDecimal(){
    if(this.auxiliarEsperaNuevoNuevo){
      this.pantalla = "0."
      this.auxiliarEsperaNuevoNuevo = false
      return;
    }
    if(!this.pantalla.includes('.')) {
      this.pantalla += '.' 
    }
  }

  obtenerResultado(){
    if(this.valorAnterior === null || this.operador === null) return;

    const valor = parseFloat(this.pantalla)
    const resultado = this.calcular(this.valorAnterior, valor,this.operador)
    this.pantalla = String(resultado)
    this.valorAnterior = null
    this.operador = null
    this.auxiliarEsperaNuevoNuevo = true
    }
  
    private calcular(a: number, b:number,operador:string):number|string{
      switch(operador){
        case '+':
          return a+b
        case '-':
          return a-b
        case '*':
          return a*b
        case '/':
          return b === 0 ? "Error" : a/b
        default:
          return b
      }
      
    }
}
