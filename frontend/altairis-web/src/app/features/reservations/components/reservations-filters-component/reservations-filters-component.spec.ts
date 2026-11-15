import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReservationsFiltersComponent } from './reservations-filters-component';

describe('ReservationsFiltersComponent', () => {
  let component: ReservationsFiltersComponent;
  let fixture: ComponentFixture<ReservationsFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReservationsFiltersComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationsFiltersComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
