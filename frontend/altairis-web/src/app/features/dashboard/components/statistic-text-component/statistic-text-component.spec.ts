import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StatisticTextComponent } from './statistic-text-component';

describe('StatisticTextComponent', () => {
  let component: StatisticTextComponent;
  let fixture: ComponentFixture<StatisticTextComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StatisticTextComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StatisticTextComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
