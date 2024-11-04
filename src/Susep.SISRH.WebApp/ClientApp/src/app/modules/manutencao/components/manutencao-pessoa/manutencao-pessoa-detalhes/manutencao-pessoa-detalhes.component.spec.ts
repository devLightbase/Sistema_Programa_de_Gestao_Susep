import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoPessoaDetalhesComponent } from './manutencao-pessoa-detalhes.component';

describe('ManutencaoPessoaDetalhesComponent', () => {
  let component: ManutencaoPessoaDetalhesComponent;
  let fixture: ComponentFixture<ManutencaoPessoaDetalhesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoPessoaDetalhesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoPessoaDetalhesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
