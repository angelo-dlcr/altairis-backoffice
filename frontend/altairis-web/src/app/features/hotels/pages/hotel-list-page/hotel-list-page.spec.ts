import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HotelListPage } from './hotel-list-page';

describe('HotelListPage', () => {
  let component: HotelListPage;
  let fixture: ComponentFixture<HotelListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HotelListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(HotelListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
