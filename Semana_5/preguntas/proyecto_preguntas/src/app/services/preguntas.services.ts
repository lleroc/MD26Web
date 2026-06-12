import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { IPregunta } from '../interfaces/ipregunta';

@Service()
export class PreguntasServices {
    private readonly http = inject(HttpClient);

    ruta:string = "https://localhost:44304/api/preguntasapi";

    todos():Observable<IPregunta[]>{
        return this.http.get<IPregunta[]>(this.ruta)
    }

}
