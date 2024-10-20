import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageAddressBookComponent } from './manage-address-book.component';

describe('ManageAddressBookComponent', () => {
  let component: ManageAddressBookComponent;
  let fixture: ComponentFixture<ManageAddressBookComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManageAddressBookComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ManageAddressBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
