import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CatalogoDominioComponent } from './catalogo-dominio.component';

describe('CatalogoDominioComponent', () => {
  let component: CatalogoDominioComponent;
  let fixture: ComponentFixture<CatalogoDominioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CatalogoDominioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CatalogoDominioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
