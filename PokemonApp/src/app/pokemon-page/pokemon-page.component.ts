import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { TableComponent } from '../table/shared-table.component';
import { PokemonDataService } from '../shared/pokemon-data.service';
import { SharedSelectComponent } from '../shared-select/shared-select.component';

export interface SelectOptions {
  value: string;
  label: string;
}

@Component({
  selector: 'app-pokemon-page',
  standalone: true,
  imports: [FormsModule, TableComponent, SharedSelectComponent],
  templateUrl: './pokemon-page.component.html',
  styleUrl: './pokemon-page.component.css',
})
export class PokemonPageComponent implements OnInit {
  sortDirection: string;
  sortType: string;
  sortTypeOptions: SelectOptions[] = [
    { value: 'id', label: 'ID' },
    { value: 'name', label: 'Name' },
    { value: 'wins', label: 'Wins' },
    { value: 'losses', label: 'Losses' },
    { value: 'ties', label: 'Ties' },
  ];

  sortDirectionOptions: SelectOptions[] = [
    { value: 'desc', label: 'Descending' },
    { value: 'asc', label: 'Ascending' },
  ];

  constructor(public service: PokemonDataService) {
    this.sortType = 'id';
    this.sortDirection = 'desc';
  }

  ngOnInit(): void {
    this.service.getPokemonData(this.sortType, this.sortDirection);
  }

  sort() {
    this.service.getPokemonData(this.sortType, this.sortDirection);
  }
}
