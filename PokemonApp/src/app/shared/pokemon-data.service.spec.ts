import { TestBed } from '@angular/core/testing';
import { PokemonDataService } from './pokemon-data.service';
import {
  HttpTestingController,
  provideHttpClientTesting,
} from '@angular/common/http/testing';
import { PokemonData } from './pokemon-data.model';
import { provideHttpClient } from '@angular/common/http';

describe('PokemonDataService', () => {
  let service: PokemonDataService;
  let httpMock: HttpTestingController;

  const mockPokemonData: PokemonData[] = [
    {
      id: 1,
      name: 'Pikachu',
      type: 'Electric',
      baseExperience: 112,
      wins: 50,
      losses: 10,
      ties: 5,
    },
    {
      id: 2,
      name: 'Bulbasaur',
      type: 'Grass',
      baseExperience: 64,
      wins: 30,
      losses: 15,
      ties: 10,
    },
  ];

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [],
      providers: [
        PokemonDataService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    });
    service = TestBed.inject(PokemonDataService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch pokemon data successfully', () => {
    service.getPokemonData('id', 'asc');

    const req = httpMock.expectOne(
      'http://localhost:5065/pokemon/tournament/statistics?sortBy=id&sortDirection=asc'
    );
    expect(req.request.method).toBe('GET');
    req.flush(mockPokemonData);

    expect(service.pokemonList.length).toBe(2);
    expect(service.pokemonList).toEqual(mockPokemonData);
  });
});
