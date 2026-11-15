import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HotelFormPage } from './hotel-form-page';

describe('HotelFormPage', () => {
  let component: HotelFormPage;
  let fixture: ComponentFixture<HotelFormPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HotelFormPage],
    }).compileComponents();

    fixture = TestBed.createComponent(HotelFormPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
