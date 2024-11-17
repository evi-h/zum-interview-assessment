import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableComponent } from './shared-table.component';
import { NgClass, NgFor } from '@angular/common';
import { PokemonDataService } from '../shared/pokemon-data.service';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideHttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
import { PokemonData } from '../shared/pokemon-data.model';
import { DebugElement } from '@angular/core';

describe('TableComponent', () => {
  let component: TableComponent;
  let fixture: ComponentFixture<TableComponent>;
  let mockPokemonDataService: jasmine.SpyObj<PokemonDataService>;

  // Mock data for testing
  const mockPokemonList: PokemonData[] = [
    {
      id: 1,
      name: 'Pikachu',
      type: 'Electric',
      baseExperience: 112,
      wins: 5,
      losses: 1,
      ties: 0,
    },
    {
      id: 2,
      name: 'Charmander',
      type: 'Fire',
      baseExperience: 62,
      wins: 3,
      losses: 2,
      ties: 1,
    },
  ];

  beforeEach(async () => {
    // Create the mock PokemonDataService
    mockPokemonDataService = jasmine.createSpyObj('PokemonDataService', [
      'getPokemonData',
    ]);

    await TestBed.configureTestingModule({
      imports: [TableComponent, NgFor, NgClass],
      providers: [
        PokemonDataService,
        provideHttpClient(),
        provideHttpClientTesting(),
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(TableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create table', () => {
    expect(component).toBeTruthy();
  });

  it('should display a list of pokemons in the table', () => {
    // Trigger ngOnInit to load the data
    component.service.pokemonList = mockPokemonList;
    fixture.detectChanges();

    // Check if the table rows are rendered
    const rows: DebugElement[] = fixture.debugElement.queryAll(By.css('tr'));
    expect(rows.length).toBe(mockPokemonList.length + 1); // +1 for header row

    // Check if the first row has the correct data
    const firstRowCells = rows[1].nativeElement.querySelectorAll('td');
    expect(firstRowCells[0].textContent).toBe('1'); // ID
    expect(firstRowCells[1].textContent).toBe('Pikachu'); // Name
    expect(firstRowCells[2].textContent).toBe('Electric'); // Type
    expect(firstRowCells[3].textContent).toBe('112'); // Base Experience
    expect(firstRowCells[4].textContent).toBe('5'); // Wins
    expect(firstRowCells[5].textContent).toBe('1'); // Losses
    expect(firstRowCells[6].textContent).toBe('0'); // Ties
  });
});
