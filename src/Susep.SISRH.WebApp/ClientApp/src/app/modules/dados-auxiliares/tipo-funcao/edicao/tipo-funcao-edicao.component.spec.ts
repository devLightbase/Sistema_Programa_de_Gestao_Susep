import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoFuncaoEdicaoComponent } from './tipo-funcao-edicao.component';

describe('TipoFuncaoEdicaoComponent', () => {
  let component: TipoFuncaoEdicaoComponent;
  let fixture: ComponentFixture<TipoFuncaoEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoFuncaoEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoFuncaoEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
