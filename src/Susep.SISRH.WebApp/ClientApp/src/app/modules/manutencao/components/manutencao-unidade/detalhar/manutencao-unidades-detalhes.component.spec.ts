import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoUnidadesDetalhesComponent } from './manutencao-unidades-detalhes.component';

describe('ManutencaoUnidadesDetalhesComponent', () => {
  let component: ManutencaoUnidadesDetalhesComponent;
  let fixture: ComponentFixture<ManutencaoUnidadesDetalhesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoUnidadesDetalhesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoUnidadesDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
