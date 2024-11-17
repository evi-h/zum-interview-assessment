import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PokemonData } from './pokemon-data.model';

@Injectable({
  providedIn: 'root',
})
export class PokemonDataService {
  url: string = 'http://localhost:5065/pokemon/tournament/statistics';
  pokemonList: PokemonData[] = [];

  constructor(private http: HttpClient) {}

  getPokemonData(sortBy: string, sortDirection: string = ''): void {
    this.http
      .get<PokemonData[]>(
        this.url + `?sortBy=${sortBy}&sortDirection=${sortDirection}`
      )
      .subscribe({
        next: (data) => {
          this.pokemonList = data; // Update the pokemonList with the fetched data
        },
        error: (err) => {
          console.error('Error in request', err);
        },
      });
  }
}
