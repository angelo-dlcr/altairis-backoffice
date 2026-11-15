import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RoomTypeListPage } from './room-type-list-page';

describe('RoomTypeListPage', () => {
  let component: RoomTypeListPage;
  let fixture: ComponentFixture<RoomTypeListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoomTypeListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(RoomTypeListPage);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
