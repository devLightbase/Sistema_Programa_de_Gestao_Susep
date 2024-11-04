import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoUnidadeEdicaoComponent } from './manutencao-unidade-edicao.component';

describe('ManutencaoUnidadeEdicaoComponent', () => {
  let component: ManutencaoUnidadeEdicaoComponent;
  let fixture: ComponentFixture<ManutencaoUnidadeEdicaoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoUnidadeEdicaoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoUnidadeEdicaoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
