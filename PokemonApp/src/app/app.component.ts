import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { PokemonPageComponent } from './pokemon-page/pokemon-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PokemonPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'PokemonApp';
}
