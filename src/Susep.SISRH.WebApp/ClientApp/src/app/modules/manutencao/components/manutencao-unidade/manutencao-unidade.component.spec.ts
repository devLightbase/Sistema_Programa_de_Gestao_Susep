import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ManutencaoUnidadeComponent } from './manutencao-unidade.component';

describe('ManutencaoUnidadeComponent', () => {
  let component: ManutencaoUnidadeComponent;
  let fixture: ComponentFixture<ManutencaoUnidadeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ManutencaoUnidadeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManutencaoUnidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
