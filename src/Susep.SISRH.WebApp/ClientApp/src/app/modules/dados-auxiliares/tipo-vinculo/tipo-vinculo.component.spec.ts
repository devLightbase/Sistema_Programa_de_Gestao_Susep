import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoVinculoComponent } from './tipo-vinculo.component';

describe('TipoVinculoComponent', () => {
  let component: TipoVinculoComponent;
  let fixture: ComponentFixture<TipoVinculoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoVinculoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoVinculoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
