import { ComponentFixture, TestBed } from '@angular/core/testing';
import { InventoryFiltersComponent } from './inventory-filters-component';

describe('InventoryFiltersComponent', () => {
  let component: InventoryFiltersComponent;
  let fixture: ComponentFixture<InventoryFiltersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InventoryFiltersComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(InventoryFiltersComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
