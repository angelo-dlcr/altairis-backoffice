import { Component, effect, inject, signal } from '@angular/core';
import { StatisticsApiService } from '../../services/statistics-service';
import { Statistics } from '../../models/statistics';
import { ToastService } from '../../../../shared/components/toast-component/service/toast-service';
import { StatisticTextComponent } from '../../components/statistic-text-component/statistic-text-component';
import { StatisticCardComponent } from '../../components/statistic-card-component/statistic-card-component';

@Component({
  imports: [StatisticTextComponent, StatisticCardComponent],
  selector: 'app-dashboard-page',
  styleUrl: './dashboard-page.scss',
  templateUrl: './dashboard-page.html',
})
export class DashboardPageComponent {
  private service = inject(StatisticsApiService);
  private toastService = inject(ToastService);

  statistics = signal<Statistics | null>(null);

  constructor() {
    effect(() => {
      this.fetchStatistics();
    });
  }
  
  fetchStatistics() {
    this.service.getStatistics().subscribe({
      next: (response) => {
        this.statistics.set(response.data ?? null);
      },
      error: (err) => {
        this.toastService.show(
          err.error.message ?? 'No se puedo obtener la información del servidor.',
          'danger'
        )
      }
    });
  }
}
