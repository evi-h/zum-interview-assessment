import { Component, OnInit } from '@angular/core';
import { TableComponent } from '../table/shared-table.component';
import { PokemonDataService } from '../shared/pokemon-data.service';
import { NgClass, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-pokemon-page',
  standalone: true,
  imports: [FormsModule, TableComponent],
  templateUrl: './pokemon-page.component.html',
  styleUrl: './pokemon-page.component.css',
})
export class PokemonPageComponent implements OnInit {
  sortDirection: string;
  sortType: string;

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
