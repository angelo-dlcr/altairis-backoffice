import { Component, input } from '@angular/core';
import { StatisticsTextData, StatisticTextComponent } from '../statistic-text-component/statistic-text-component';


type StatisticStyle = | 'primary' | 'secondary' | 'success' | 'danger' | 'info' | 'warning';


@Component({
  imports: [StatisticTextComponent],
  selector: 'app-statistic-card-component',
  styleUrl: './statistic-card-component.scss',
  templateUrl: './statistic-card-component.html',
})

export class StatisticCardComponent {
  title = input.required<string>();
  icon = input.required<string>();
  value = input.required<number>();
  style = input<StatisticStyle>('secondary')
  
  statistics = input<StatisticsTextData[]>([]);
}
