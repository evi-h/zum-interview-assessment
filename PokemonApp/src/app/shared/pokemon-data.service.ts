import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PokemonData } from './pokemon-data.model';

@Injectable({
  providedIn: 'root',
})
export class PokemonDataService {
  constructor(private http: HttpClient) {}

  url: string = 'http://localhost:5065/pokemon/tournament/statistics';
  pokemonList: PokemonData[] = [];

  getPokemonData(sortBy: string, sortDirection: string = '') {
    this.http
      .get(this.url + `?sortBy=${sortBy}&sortDirection=${sortDirection}`)
      .subscribe({
        next: (res) => {
          this.pokemonList = res as PokemonData[];
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
