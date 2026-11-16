import { Component, ElementRef, inject, input, Input, output, signal, viewChild } from '@angular/core';

import { Modal } from 'bootstrap';

@Component({
  imports: [],
  selector: 'app-modal-component',
  styleUrl: './modal-component.scss',
  templateUrl: './modal-component.html',
})
export class ModalComponent {

  title = input.required<string>();
  size = input<'md' | 'lg' | 'xl'>('lg');

  closed = output<void>();

  modalElement = viewChild.required<ElementRef<HTMLDivElement>>('modal');

  private modalInstance?: Modal;

  open(): void {
    this.modalInstance = Modal.getOrCreateInstance(
      this.modalElement().nativeElement
    );

    this.modalInstance.show();
  }

  close(): void {
    this.modalInstance?.hide();
  }

  onHidden(): void {
    this.closed.emit();
  }
}
