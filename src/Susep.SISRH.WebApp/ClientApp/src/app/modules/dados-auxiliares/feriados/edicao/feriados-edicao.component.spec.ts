import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FeriadosEdicaoComponent } from './feriados-edicao.component';

describe('FeriadosEdicaoComponent', () => {
  let component: FeriadosEdicaoComponent;
  let fixture: ComponentFixture<FeriadosEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FeriadosEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FeriadosEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
