import { Component, input } from '@angular/core';

type StatisticStyle = | 'primary' | 'secondary' | 'success' | 'danger' | 'info' | 'warning';

export interface StatisticsTextData {
  value: any,
  label: string,
  style: StatisticStyle
}

@Component({
  imports: [],
  selector: 'app-statistic-text-component',
  styleUrl: './statistic-text-component.scss',
  templateUrl: './statistic-text-component.html',
})
export class StatisticTextComponent {
  readonly data = input.required<StatisticsTextData[]>();
}
