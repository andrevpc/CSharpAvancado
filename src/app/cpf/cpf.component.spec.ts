import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CPFComponent } from './cpf.component';

describe('CPFComponent', () => {
  let component: CPFComponent;
  let fixture: ComponentFixture<CPFComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CPFComponent]
    });
    fixture = TestBed.createComponent(CPFComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
