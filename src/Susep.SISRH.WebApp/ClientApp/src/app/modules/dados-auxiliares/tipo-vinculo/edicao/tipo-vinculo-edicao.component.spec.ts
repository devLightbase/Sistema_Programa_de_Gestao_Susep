import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoVinculoEdicaoComponent } from './tipo-vinculo-edicao.component';

describe('TipoVinculoEdicaoComponent', () => {
  let component: TipoVinculoEdicaoComponent;
  let fixture: ComponentFixture<TipoVinculoEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoVinculoEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoVinculoEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
