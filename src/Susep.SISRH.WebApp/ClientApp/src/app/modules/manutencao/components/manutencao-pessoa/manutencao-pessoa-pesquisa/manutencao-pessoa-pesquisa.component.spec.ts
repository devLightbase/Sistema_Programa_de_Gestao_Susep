import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoPessoaPesquisaComponent } from './manutencao-pessoa-pesquisa.component';

describe('ManutencaoPessoaPesquisaComponent', () => {
  let component: ManutencaoPessoaPesquisaComponent;
  let fixture: ComponentFixture<ManutencaoPessoaPesquisaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoPessoaPesquisaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoPessoaPesquisaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
