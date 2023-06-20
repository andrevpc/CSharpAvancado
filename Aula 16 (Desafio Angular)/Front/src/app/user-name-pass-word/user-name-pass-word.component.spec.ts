import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserNamePassWordComponent } from './user-name-pass-word.component';

describe('UserNamePassWordComponent', () => {
  let component: UserNamePassWordComponent;
  let fixture: ComponentFixture<UserNamePassWordComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UserNamePassWordComponent]
    });
    fixture = TestBed.createComponent(UserNamePassWordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
