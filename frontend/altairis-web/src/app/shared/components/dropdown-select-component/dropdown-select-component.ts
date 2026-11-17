import { Component, input } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  imports: [],
  selector: 'app-dropdown-select-component',
  styleUrl: './dropdown-select-component.scss',
  templateUrl: './dropdown-select-component.html',
})
export class DropdownSelectComponent {
    control = input.required<FormControl>();
    options = input.required<readonly string[]>();
    requiredValidation = input.required<boolean>();

    viewing = input<boolean>(false);
    placeholder = input<string>('Seleccione una opción');

    selectOption(option: string): void {
      if (this.viewing()) return;

      this.control().setValue(option);
      this.control().markAsDirty();
      this.control().markAsTouched();
    }

    getOptionLabel(option: string): string {
      return this.options().find(op => op.startsWith(option)) ?? '';
    }
}
