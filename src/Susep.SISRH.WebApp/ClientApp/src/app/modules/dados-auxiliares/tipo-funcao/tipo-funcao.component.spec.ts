import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoFuncaoComponent } from './tipo-funcao.component';

describe('TipoFuncaoComponent', () => {
  let component: TipoFuncaoComponent;
  let fixture: ComponentFixture<TipoFuncaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoFuncaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoFuncaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
