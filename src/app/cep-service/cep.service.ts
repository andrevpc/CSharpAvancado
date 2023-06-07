import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CepData } from '../cep-data'
@Injectable({
  providedIn: 'root'
})
export class CepService {
  constructor(private http: HttpClient) { }
  getStreet(cep: string) {
    return this.http.get<CepData>("https://viacep.com.br/ws/" + cep + "/json/");
  }
}