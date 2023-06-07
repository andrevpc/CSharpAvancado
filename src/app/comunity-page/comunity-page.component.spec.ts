import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComunityPageComponent } from './comunity-page.component';

describe('ComunityPageComponent', () => {
  let component: ComunityPageComponent;
  let fixture: ComponentFixture<ComunityPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComunityPageComponent]
    });
    fixture = TestBed.createComponent(ComunityPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
