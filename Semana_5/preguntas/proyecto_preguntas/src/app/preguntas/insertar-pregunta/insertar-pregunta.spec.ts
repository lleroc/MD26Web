import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsertarPregunta } from './insertar-pregunta';

describe('InsertarPregunta', () => {
  let component: InsertarPregunta;
  let fixture: ComponentFixture<InsertarPregunta>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InsertarPregunta],
    }).compileComponents();

    fixture = TestBed.createComponent(InsertarPregunta);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
