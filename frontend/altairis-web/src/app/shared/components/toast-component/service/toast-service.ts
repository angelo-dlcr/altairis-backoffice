import { Injectable, signal } from '@angular/core';

export interface ToastMessage {
  id: number;
  message: string;
  title?: string;
  type: 'success' | 'danger' | 'warning' | 'info' | 'primary' | 'secondary';
  delay?: number;
}

@Injectable({
    providedIn: 'root'
})
export class ToastService {
  toasts = signal<ToastMessage[]>([]);
  private counter = 0;

  show(message: string, type: ToastMessage['type'] = 'info', title?: string, delay = 6000) {
    const id = ++this.counter;
    const newToast: ToastMessage = { id, message, type, title, delay };
    
    this.toasts.update(current => [...current, newToast]);

    setTimeout(() => {
      this.remove(id);
    }, delay);
  }

  remove(id: number) {
    this.toasts.update(current => current.filter(t => t.id !== id));
  }
}
