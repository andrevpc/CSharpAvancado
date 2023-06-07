import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyCpfComponent } from './verify-cpf.component';

describe('VerifyCpfComponent', () => {
  let component: VerifyCpfComponent;
  let fixture: ComponentFixture<VerifyCpfComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [VerifyCpfComponent]
    });
    fixture = TestBed.createComponent(VerifyCpfComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
