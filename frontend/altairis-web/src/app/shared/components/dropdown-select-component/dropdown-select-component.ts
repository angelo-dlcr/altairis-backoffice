import { Component, effect, input, output } from '@angular/core';
import { FormControl } from '@angular/forms';

export interface DropdownSelectOption<T = any> {
  value: T,
  label: string
}

@Component({
  imports: [],
  selector: 'app-dropdown-select-component',
  styleUrl: './dropdown-select-component.scss',
  templateUrl: './dropdown-select-component.html',
})
export class DropdownSelectComponent {
  control = input<FormControl | undefined>(undefined);
  
  value = input<any>(null);
  valueChange = output<any>();
  
  options = input.required<readonly DropdownSelectOption[]>();
  requiredValidation = input<boolean>(false);
  
  viewing = input<boolean>(false);
  placeholder = input<string>('Seleccione una opción');
  
  modalOpen = input<boolean>(false);

  constructor() {
    effect(() => {
      const isOpen = this.modalOpen();
    });
  }

  get selectedValue(): any {
    return this.control() !== undefined
      ? this.control()?.value
      : this.value() ?? null;
  }

  selectOption(option: DropdownSelectOption): void {
      if (this.viewing()) return;

      if (this.control()) {
        this.control()?.setValue(option.value);
        this.control()?.markAsDirty();
        this.control()?.markAsTouched();
      } 
      else {
        this.valueChange.emit(option.value);
      }
  }

  getOptionLabel(): string {
    return this.options().find(
      option => option.value === this.selectedValue
    )?.label ?? '';
  }

  markAsTouched(): void {
    if (this.requiredValidation()) {
      this.control()?.markAsTouched();
    }
  }

  checkValid(): boolean {
    return this.requiredValidation() && this.control()
      ? this.requiredValidation()! && this.control()?.valid! && this.control()?.touched!
      : false;
  }

  checkInvalid(): boolean {
    return this.requiredValidation() && this.control()
      ? this.requiredValidation() && this.control()?.invalid! && this.control()?.touched!
      : false;
  }

  applyTransparency(): string {
    return this.requiredValidation() && this.control()?.touched
      ? 'transparent'
      : ''
  }
}
