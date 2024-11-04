import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PgUnidadeComponent } from './pg-unidade.component';

describe('PgUnidadeComponent', () => {
  let component: PgUnidadeComponent;
  let fixture: ComponentFixture<PgUnidadeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PgUnidadeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PgUnidadeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
