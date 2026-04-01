import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarPeliCine } from './asignar-peli-cine';

describe('AsignarPeliCine', () => {
  let component: AsignarPeliCine;
  let fixture: ComponentFixture<AsignarPeliCine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsignarPeliCine],
    }).compileComponents();

    fixture = TestBed.createComponent(AsignarPeliCine);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
