import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoPessoaEdicaoComponent } from './situacao-pessoa-edicao.component';

describe('SituacaoPessoaEdicaoComponent', () => {
  let component: SituacaoPessoaEdicaoComponent;
  let fixture: ComponentFixture<SituacaoPessoaEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoPessoaEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoPessoaEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
