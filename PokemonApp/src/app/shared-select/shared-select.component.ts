import { NgFor } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { SelectOptions } from '../pokemon-page/pokemon-page.component';

@Component({
  selector: 'app-shared-select',
  standalone: true,
  imports: [NgFor, FormsModule],
  templateUrl: './shared-select.component.html',
  styleUrl: './shared-select.component.css',
})
export class SharedSelectComponent {
  // Input to receive the list of options
  @Input() options: SelectOptions[] = [];

  // Input to bind the selected option
  @Input() selectedOption: string = '';

  // Output to emit the selected option to the parent
  @Output() selectedOptionChange = new EventEmitter<string>();

  // Method to handle selection change
  onSelectionChange(value: string) {
    this.selectedOptionChange.emit(value); // Emit the change
  }
}
