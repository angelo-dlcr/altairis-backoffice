import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RoomTypeFormPage } from './room-type-form-page';

describe('RoomTypeFormPage', () => {
  let component: RoomTypeFormPage;
  let fixture: ComponentFixture<RoomTypeFormPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoomTypeFormPage],
    }).compileComponents();

    fixture = TestBed.createComponent(RoomTypeFormPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
