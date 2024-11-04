import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SituacaoPessoaComponent } from './situacao-pessoa.component';

describe('SituacaoPessoaComponent', () => {
  let component: SituacaoPessoaComponent;
  let fixture: ComponentFixture<SituacaoPessoaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SituacaoPessoaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SituacaoPessoaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
