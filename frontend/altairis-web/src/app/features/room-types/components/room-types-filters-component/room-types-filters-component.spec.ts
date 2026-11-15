import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RoomTypesFiltersComponent } from './room-types-filters-component';

describe('RoomTypesFiltersComponent', () => {
  let component: RoomTypesFiltersComponent;
  let fixture: ComponentFixture<RoomTypesFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoomTypesFiltersComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RoomTypesFiltersComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
