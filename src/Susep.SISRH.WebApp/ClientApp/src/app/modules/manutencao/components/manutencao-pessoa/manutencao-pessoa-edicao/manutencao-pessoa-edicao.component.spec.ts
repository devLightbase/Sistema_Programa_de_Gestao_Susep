import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoPessoaEdicaoComponent } from './manutencao-pessoa-edicao.component';

describe('ManutencaoPessoaEdicaoComponent', () => {
  let component: ManutencaoPessoaEdicaoComponent;
  let fixture: ComponentFixture<ManutencaoPessoaEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoPessoaEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoPessoaEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
