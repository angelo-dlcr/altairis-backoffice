import { Component, inject } from '@angular/core';
import { ToastService } from './service/toast-service';
import { CommonModule } from '@angular/common';

@Component({
  imports: [CommonModule],
  selector: 'app-toast-component',
  styleUrl: './toast-component.scss',
  templateUrl: './toast-component.html',
})
export class ToastComponent {
  toastService = inject(ToastService);
}
