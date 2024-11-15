import { NgClass, NgFor } from '@angular/common';
import { Component } from '@angular/core';
import { PokemonDataService } from '../shared/pokemon-data.service';

@Component({
  selector: 'app-shared-table',
  standalone: true,
  imports: [NgFor, NgClass],
  templateUrl: './shared-table.component.html',
  styleUrl: './shared-table.component.css',
})
export class TableComponent {
  constructor(public service: PokemonDataService) {}
}
